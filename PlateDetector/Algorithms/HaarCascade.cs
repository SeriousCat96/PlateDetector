using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace PlateDetector.Algorithms
{
	/// <summary> Реализует каскадный классификатор Хаара. </summary>
	public class HaarCascade : IDetectionAlgorithm
	{
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
		public HaarCascade(double scaleFactor, int minNeighbours, OpenCvSharp.Size minSize, OpenCvSharp.Size maxSize)
		{
			_scaleFactor = scaleFactor;
			_minNeighbours = minNeighbours;
			_minSize = minSize;
			_maxSize = maxSize;
		}

		#endregion

		#region Methods

		/// <summary> Загрузка параметров алгоритма из файла. </summary>
		/// <param name="filename"> Путь к файлу. </param>
		public void Load(string filename)
		{
			_classifier = new CascadeClassifier(filename);
		}

		/// <summary> Предсказывает местоположения объектов на изображении. </summary>
		/// <param name="image"> Анализируемое изображение. </param>
		/// <returns> Список ограничивающих прямоугольников <see cref="Rectangle"/>. </returns>
		public List<Rectangle> Predict(Bitmap image)
		{
			var mat = BitmapConverter.ToMat(image);

			var rects = _classifier.DetectMultiScale(
				image: mat,
				scaleFactor: _scaleFactor,
				minNeighbors: _minNeighbours,
				flags: HaarDetectionType.ScaleImage,
				minSize: _minSize,
				maxSize: _maxSize);


			var rectList = rects
				.Select(e => new Rectangle(e.X, e.Y, e.Width, e.Height))
				.ToList();

			return rectList;
		}

		#endregion
	}
}
