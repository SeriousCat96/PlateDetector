using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TensorFlow;
using System.Drawing.Imaging;

namespace PlateDetector.Algorithms
{
	public class ConvNeuralNet : IDetectionAlgorithm
	{
		private TFGraph _model;

		public ConvNeuralNet()
		{
			_model = new TFGraph();
		}

		public void Load(string filename)
		{
			_model.Import(File.ReadAllBytes(filename));
		}

		public List<Rectangle> Predict(Bitmap image)
		{
			var session = new TFSession(_model);
			var runner = session.GetRunner();

			//var tensor = ImageUtil.CreateTensorFromImageFile(tuple.input, TFDataType.UInt8);
			//runner.AddInput(_model[""], tensor);

			return null;
		}

		public unsafe static double[,,] BitmapToByteRgb(Bitmap bmp)
		{
			int width = bmp.Width,
				height = bmp.Height;
			double[,,] res = new double[3, height, width];
			BitmapData bd = bmp.LockBits(
				new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly,PixelFormat.Format24bppRgb);
			try
			{
				byte* curpos;
				fixed (double* _res = res)
				{
					double* _r = _res, _g = _res + width * height, _b = _res + 2 * width * height;
					for(int h = 0; h < height; h++)
					{
						curpos = ((byte*)bd.Scan0) + h * bd.Stride;
						for(int w = 0; w < width; w++)
						{
							*_b = *(curpos++) / 255d; ++_b;
							*_g = *(curpos++) / 255d; ++_g;
							*_r = *(curpos++) / 255d; ++_r;
						}
					}
				}
			}
			finally
			{
				bmp.UnlockBits(bd);
			}
			return res;
		}
	}
}
