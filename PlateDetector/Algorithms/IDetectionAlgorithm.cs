using System.Collections.Generic;
using System.Drawing;

namespace PlateDetector.Algorithms
{
	public interface IDetectionAlgorithm
	{
		List<Rectangle> Predict(Bitmap image);

		void Load(string filename);
	}
}
