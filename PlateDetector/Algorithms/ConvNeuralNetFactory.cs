namespace PlateDetector.Algorithms
{
	public class ConvNeuralNetFactory : IDetectionAlgorithmFactory
	{
		public IDetectionAlgorithm CreateDetectionAlgorithm()
		{
			return new ConvNeuralNet();
		}
	}
}
