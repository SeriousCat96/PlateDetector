namespace PlateDetector.Algorithms
{
	public class HaarCascadeFactory : IDetectionAlgorithmFactory
	{
		public IDetectionAlgorithm CreateDetectionAlgorithm()
		{
			return new HaarCascade(
				scaleFactor: 1.25,
				minNeighbours: 5,
				minSize: new OpenCvSharp.Size(80, 20),
				maxSize: new OpenCvSharp.Size(1000, 300));
		}
	}
}
