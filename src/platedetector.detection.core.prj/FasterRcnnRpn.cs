﻿using OpenCvSharp;

using Platedetector.Detection.Utils;

using System;
using System.Collections.Generic;
using System.IO;

using TensorFlow;

namespace Platedetector.Detection.Core
{
	/// <summary> Реализует сверточную нейронную сеть. </summary>
	public sealed class FasterRcnnRpn : IDetectionAlg, IDisposable
	{
		#region Const
		public readonly string ModelFile = Path.Combine(Directory.GetCurrentDirectory(), "fasterrcnn.pb");

		#endregion

		#region Data
		/// <summary> Граф вычислений модели сверточной нейронной сети. </summary>
		private TFGraph _graph;

		/// <summary> TF сессия, обеспечивающая запус графа вычислений нейронной сети. </summary>
		private TFSession _session;

		/// <summary> Размер входного изображения. </summary>
		private Size _inputImageSize;
        #endregion

        #region .ctor

        /// <summary> Создает <see cref="FasterRcnnRpn"/>. </summary>
        /// <param name="computationalGraph"> Граф вычислений модели сверточной нейронной сети. </param>
        /// <param name="inputImageSize"> Размер входного изображения. </param>
        public FasterRcnnRpn(TFGraph computationalGraph, Size inputImageSize, DetectionResultPattern pattern = DetectionResultPattern.RegionWithProbability)
        {
			_inputImageSize = inputImageSize;
			_graph     = computationalGraph;
            Pattern    = pattern;

			Load(ModelFile);
		}
        #endregion

        #region Properties
    
        /// <summary> Шаблон результата локализации. </summary>
        public DetectionResultPattern Pattern { get; }

        #endregion

        #region Methods

        /// <summary> Освобождает ресурсы объекта. </summary>
        public void Dispose()
		{
            _graph?.Dispose();
            _graph = null;

            _session?.CloseSession();
            _session?.Dispose();
            _session = null;
        }

		/// <summary> Загрузка параметров алгоритма из файла. </summary>
		/// <param name="filename"> Путь к файлу. </param>
		public void Load(string filename)
		{
			var buffer = new TFBuffer(File.ReadAllBytes(filename));
			_graph.Import(buffer, "");

			_session = new TFSession(_graph);
		}

		/// <summary> Предсказывает местоположения объектов на изображении. </summary>
		/// <param name="image"> Анализируемое изображение. </param>
		/// <returns> Список ограничивающих прямоугольников <see cref="Rectangle"/>. </returns>
		public IReadOnlyList<Detection> Predict(Mat image)
		{
			var runner = _session.GetRunner();
			
			var imgArr = Preprocess(image);
			var tensor = new TFTensor(imgArr.Value);
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

		/// <summary> Предварительная обработка изображения и конвертация пикселей в массив <see cref="float"/>. </summary>
		/// <param name="image"> Исходное изображение. </param>
		/// <returns> Массив float пикселей изображения. </returns>
		private NdArray<float> Preprocess(Mat image)
		{
			image = image.Resize(_inputImageSize, 1, 1, InterpolationFlags.Linear);

			return image.ToFloatArray();
		}

		/// <summary> Обработка результатов работы алгоритма. </summary>
		/// <param name="output"> Результаты в виде тензоров. </param>
		/// <param name="originalSize"> Размер оригинала изображения. </param>
		/// <returns> Результаты локализации. </returns>
		private IReadOnlyList<Detection> PostProcess(TFTensor[] output, Size originalSize)
		{
			TFTensor boundBoxesTensor = output[0];
			TFTensor probsTensor = output[1];

			var boundBoxes = (float[][])boundBoxesTensor.GetValue(jagged: true);
			var probs	   = (float[])probsTensor.GetValue();

			var detections = new List<Detection>();

			for(int i = 0; i < boundBoxes.Length; i++)
			{
				if(probs[i] >= 0.5)
				{
					int newXmin = (int)(boundBoxes[i][0] / _inputImageSize.Width * originalSize.Width);
					int newYmin = (int)(boundBoxes[i][1] / _inputImageSize.Height * originalSize.Height);
					int newXmax = (int)(boundBoxes[i][2] / _inputImageSize.Width * originalSize.Width);
					int newYmax = (int)(boundBoxes[i][3] / _inputImageSize.Height * originalSize.Height);

					var region = new Rect(
						newXmin,
						newYmin,
						newXmax - newXmin,
						newYmax - newYmin);

					detections.Add(new Detection(region, probs[i], Country.None));
				}
				else break;
			}

			return detections;
		}

		public override string ToString()
		{
			return "Faster R-CNN";
		}
		#endregion
	}
}
