using OpenCvSharp;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Platedetector.Detection
{
	/// <summary> Реализует каскадный классификатор Хаара. </summary>
	public sealed class HaarCascade : IDetectionAlg, IDisposable
	{
		#region Const
		private readonly string ModelFile = Path.Combine(Directory.GetCurrentDirectory(), "haarcascade_russian_plate_number.xml");

		#endregion

		#region Data
		/// <summary> Классификатор каскадов <seealso cref="OpenCvSharp.CascadeClassifier"/> для обнаружения объектов. </summary>
		private CascadeClassifier _classifier;

		/// <summary> Параметр, определяющий, насколько размер сканирующего окна увеличивается. </summary>
		private double _scaleFactor;

		/// <summary> Параметр, определяющий минимальное количество обнаружений в соседних областях, чтобы считать обнаружение достоверным. </summary>
		private int _minNeighbours;

		/// <summary> Минимальный размер объекта на изображении. Объекты меньше будут проигнорированы. </summary>
		private OpenCvSharp.Size _minSize;

		/// <summary> Максимальный размер объекта на изображении. Объекты больше будут проигнорированы. </summary>
		private OpenCvSharp.Size _maxSize;

		#endregion

		#region .ctor
		/// <summary> Создает <see cref="HaarCascade"/>. </summary>
		/// <param name="scaleFactor"> Параметр, определяющий, насколько размер сканирующего окна увеличивается. </param>
		/// <param name="minNeighbours"> Параметр, определяющий минимальное количество обнаружений в соседних областях, чтобы считать обнаружение достоверным. </param>
		/// <param name="minSize"> Минимальный размер объекта на изображении. Объекты меньше будут проигнорированы. </param>
		/// <param name="maxSize"> Максимальный размер объекта на изображении. Объекты больше будут проигнорированы. </param>
		public HaarCascade(double scaleFactor, int minNeighbours, OpenCvSharp.Size minSize, OpenCvSharp.Size maxSize, DetectionResultPattern pattern = DetectionResultPattern.RegionOnly)
		{
			_scaleFactor = scaleFactor;
			_minNeighbours = minNeighbours;
			_minSize = minSize;
			_maxSize = maxSize;
            Pattern  = pattern;

			_classifier = new CascadeClassifier(ModelFile);
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
			if(_classifier != null)
			{
				_classifier.Dispose();
			}
		}

		/// <summary> Загрузка параметров алгоритма из файла. </summary>
		/// <param name="filename"> Путь к файлу. </param>
		public void Load(string filename)
		{
			var result = _classifier.Load(filename);
		}

		/// <summary> Предсказывает местоположения объектов на изображении. </summary>
		/// <param name="image"> Анализируемое изображение. </param>
		/// <returns> Список ограничивающих прямоугольников <see cref="OpenCvSharp.Rect"/>. </returns>
		public IReadOnlyList<Detection> Predict(Mat image)
		{
			var rects = _classifier.DetectMultiScale(
				image: image,
				scaleFactor: _scaleFactor,
				minNeighbors: _minNeighbours,
				flags: HaarDetectionType.ScaleImage,
				minSize: _minSize,
				maxSize: _maxSize);

			var detections = rects.Select(e => new Detection(e)).ToList();
			return detections;
		}

		public override string ToString()
		{
			return "Метод Виолы-Джонса";
		}

		#endregion
	}
}
