using System;
using System.Drawing;

namespace PlateDetector.Algorithms
{
	public class Detector
	{
		#region Data

		private AlgorithmManager _manager;
		#endregion

		#region .ctor

		public Detector(AlgorithmManager manager)
		{
			_manager = manager;
		}
		#endregion

		#region Methods

		public DetectionResult Detect(Bitmap image)
		{
			var algorithm = _manager.SelectedAlgorithm;

			if(algorithm != null)
			{
				var boundBoxes = algorithm.Predict(image);
				var result = new DetectionResult(boundBoxes);

				return result;
			}

			else return null;
		}

		public void ImportModel(string filename)
		{
			var algorithm = _manager.SelectedAlgorithm;

			if(algorithm != null)
			{
				algorithm.Load(filename);
			}
		}

		public void ChangeAlgorithm(Type type)
		{
			_manager.Select(type);
		} 
		#endregion
	}
}
