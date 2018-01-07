using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

using OpenCvSharp;
using OpenCvSharp.Extensions;

using TensorFlow;

namespace PlateDetector.Algorithms
{
	public class ConvNeuralNet : IDetectionAlgorithm, IDisposable
	{
		private TFGraph _model;

		public ConvNeuralNet()
		{
			_model = new TFGraph();
		}

		public void Load(string filename)
		{
			var buffer = new TFBuffer(File.ReadAllBytes(filename));
			_model.Import(buffer, "");
		}

		public List<Rectangle> Predict(Bitmap image)
		{
			using(var session = new TFSession(_model))
			{
				var runner = session.GetRunner();
				
				var imgArr = Preprocess(image);
				var tensor = new TFTensor(imgArr);
				runner.AddInput(_model["inputs"][0], tensor);
				runner.AddInput(_model["fc2_c_dropout"][0], new TFTensor(1.0f));
				runner.Fetch(_model["outputs"][0]);

				var output = runner.Run();
				TFTensor result = output[0];
				var res = (float[][]) result.GetValue(jagged: true);

				var rects = new List<Rectangle>();
				var rect = ImageProcessor.GetRectangle(res[0], image.Width, image.Height);

				rects.Add(rect);
				return rects;
			}
		}

		private static float[,,,] Preprocess(Bitmap bitmap)
		{
			var mat = BitmapConverter.ToMat(bitmap);

			mat = ImageProcessor.Resize(mat, new OpenCvSharp.Size(128, 96));
			var gray = mat.CvtColor(ColorConversionCodes.RGBA2GRAY);

			bitmap = BitmapConverter.ToBitmap(gray);

			return ImageProcessor.BitmapToFloatGrayScale(bitmap);
		}

		public void Dispose()
		{
			if(_model != null)
			{
				_model.Dispose();
			}
		}
	}
}
