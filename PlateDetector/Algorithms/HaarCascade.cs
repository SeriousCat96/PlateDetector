

using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace PlateDetector.Algorithms
{
	public class HaarCascade : IDetectionAlgorithm
	{
		#region Data
		/// <summary>
		/// Классификатор каскадов для обнаружения объекта.
		/// </summary>
		private CascadeClassifier _classifier;

		private double _scaleFactor;

		private int _minNeighbours;

		private OpenCvSharp.Size _minSize;

		private OpenCvSharp.Size _maxSize;

		#endregion

		#region .ctor
		public HaarCascade(double scaleFactor, int minNeighbours, OpenCvSharp.Size minSize, OpenCvSharp.Size maxSize)
		{
			_scaleFactor = scaleFactor;
			_minNeighbours = minNeighbours;
			_minSize = minSize;
			_maxSize = maxSize;
		}

		#endregion

		#region Methods

		public void Load(string filename)
		{
			_classifier = new CascadeClassifier(filename);
		} 

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
