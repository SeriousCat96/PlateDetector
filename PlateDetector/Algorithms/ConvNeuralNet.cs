using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

using OpenCvSharp;
using OpenCvSharp.Extensions;

using TensorFlow;

namespace PlateDetector.Algorithms
{
	/// <summary> Реализует сверточную нейронную сеть. </summary>
	public class ConvNeuralNet : IDetectionAlg, IDisposable
	{
		#region Data
		/// <summary> Граф вычислений модели сверточной нейронной сети. </summary>
		private TFGraph _graph;

		/// <summary> Размер входного изображения. </summary>
		private OpenCvSharp.Size _imageSize;
		#endregion

		#region .ctor

		/// <summary> Создает <see cref="ConvNeuralNet"/>. </summary>
		/// <param name="model"> Граф вычислений модели сверточной нейронной сети. </param>
		/// <param name="imageSize"> Размер входного изображения. </param>
		public ConvNeuralNet(TFGraph model, OpenCvSharp.Size imageSize)
		{
			_imageSize = imageSize;
			_graph = model;
		}
		#endregion

		#region Methods

		/// <summary> Освобождает ресурсы объекта. </summary>
		public void Dispose()
		{
			if(_graph != null)
			{
				_graph.Dispose();
			}
		}

		/// <summary> Загрузка параметров алгоритма из файла. </summary>
		/// <param name="filename"> Путь к файлу. </param>
		public void Load(string filename)
		{
			var buffer = new TFBuffer(File.ReadAllBytes(filename));
			_graph.Import(buffer, "");
		}

		/// <summary> Предсказывает местоположения объектов на изображении. </summary>
		/// <param name="image"> Анализируемое изображение. </param>
		/// <returns> Список ограничивающих прямоугольников <see cref="Rectangle"/>. </returns>
		public List<Rect> Predict(Mat image)
		{
			using(var session = new TFSession(_graph))
			{
				var runner = session.GetRunner();
				var global = _graph["init"];
				var local = _graph["init_1"];

				runner.AddTarget(global, local);
				runner.Run();

				var imgArr = Preprocess(image);
				var tensor = new TFTensor(imgArr);
				var is_train = new TFTensor(false);

				runner.AddInput(_graph["inputs"][0], tensor);
				runner.AddInput(_graph["is_train"][0], is_train);
				runner.Fetch(_graph["BoundingBoxTransform/clip_bboxes_1/concat"][0], _graph["nms/gather_nms_proposals_scores"][0]);

				var output = runner.Run();
				TFTensor result = output[0];
				var res = (float[][])result.GetValue(jagged: true);

				var rects = new List<Rect>();
				//var rect = MatExtensions.GetRectangle(res[0], image.Width, image.Height);

				//rects.Add(rect);
				return rects;
			}
		}

		/// <summary> Предварительная обработка изображения и конвертация пикселей в массив <see cref="float"/>. </summary>
		/// <param name="bitmap"> Исходное изображение. </param>
		/// <returns> Массив float пикселей изображения. </returns>
		private float[,,,] Preprocess(Mat image)
		{
			image = image.Resize(_imageSize);

			return image.ToFloatArray();
		}
		 
		#endregion
	}
}
