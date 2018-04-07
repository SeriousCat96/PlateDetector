using System;
using System.Collections.Generic;
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

		/// <summary> TF сессия, обеспечивающая запус графа вычислений нейронной сети. </summary>
		private TFSession _session;

		/// <summary> Размер входного изображения. </summary>
		private Size _imageSize;
		#endregion

		#region .ctor

		/// <summary> Создает <see cref="ConvNeuralNet"/>. </summary>
		/// <param name="model"> Граф вычислений модели сверточной нейронной сети. </param>
		/// <param name="imageSize"> Размер входного изображения. </param>
		public ConvNeuralNet(TFGraph model, Size imageSize)
		{
			_imageSize = imageSize;
			_graph = model;

			Load(@"E:\Study\Mallenom\vgg16_rpn\fasterrcnn_frozen.pb");
		}
		#endregion

		#region Methods

		/// <summary> Освобождает ресурсы объекта. </summary>
		public void Dispose()
		{
			_graph?.Dispose();

			_session?.CloseSession();
			_session?.Dispose();
		}

		/// <summary> Загрузка параметров алгоритма из файла. </summary>
		/// <param name="filename"> Путь к файлу. </param>
		public void Load(string filename)
		{
			var buffer = new TFBuffer(File.ReadAllBytes(filename));
			_graph.Import(buffer, "");

			_session = new TFSession(_graph);

			//InitVariables();
		}

		/// <summary> Предсказывает местоположения объектов на изображении. </summary>
		/// <param name="image"> Анализируемое изображение. </param>
		/// <returns> Список ограничивающих прямоугольников <see cref="Rectangle"/>. </returns>
		public List<Rect> Predict(Mat image)
		{
			var runner = _session.GetRunner();

			var imgArr = Preprocess(image);
			var tensor = new TFTensor(imgArr);
			var is_train = new TFTensor(false);

			runner.AddInput(
				_graph["inputs"][0],
				tensor);
			runner.Fetch(
				_graph["BoundingBoxTransform/clip_bboxes_1/concat"][0],
				_graph["nms/gather_nms_proposals_scores"][0]);

			var output = runner.Run();
			var rects = PostProcess(output, image.Size());
			
			return rects;
		}

		private void InitVariables()
		{
			var runner = _session.GetRunner();
			var global = _graph["init"];
			var local = _graph["init_1"];
			runner.AddTarget(global, local);
			runner.Run();
		}

		/// <summary> Предварительная обработка изображения и конвертация пикселей в массив <see cref="float"/>. </summary>
		/// <param name="bitmap"> Исходное изображение. </param>
		/// <returns> Массив float пикселей изображения. </returns>
		private float[,,,] Preprocess(Mat image)
		{
			image = image.Resize(_imageSize, 1, 1, InterpolationFlags.Linear);

			return image.ToFloatArray();
		}

		/// <summary> Обработка результатов работы алгоритма. </summary>
		/// <param name="output"> Исходное изображение. </param>
		/// <returns> Результаты локализации. </returns>
		private List<Rect> PostProcess(TFTensor[] output, Size originalSize)
		{
			TFTensor boundBoxesTensor = output[0];
			TFTensor probsTensor = output[1];

			var boundBoxes = (float[][])boundBoxesTensor.GetValue(jagged: true);
			var probs	   = (float[])probsTensor.GetValue();

			var result = new List<Rect>();

			for(int i = 0; i < boundBoxes.Length; i++)
			{
				if(probs[i] >= 0.5)
				{
					int newXmin = (int)(boundBoxes[i][0] / _imageSize.Width * originalSize.Width);
					int newYmin = (int)(boundBoxes[i][1] / _imageSize.Height * originalSize.Height);
					int newXmax = (int)(boundBoxes[i][2] / _imageSize.Width * originalSize.Width);
					int newYmax = (int)(boundBoxes[i][3] / _imageSize.Height * originalSize.Height);

					result.Add(
						new Rect(
							newXmin,
							newYmin,
							newXmax - newXmin,
							newYmax - newYmin));
				}
				else break;
			}

			return result;
		}
		#endregion
	}
}
