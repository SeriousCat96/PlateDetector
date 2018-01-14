using OpenCvSharp;

using TensorFlow;

namespace PlateDetector.Algorithms
{
	/// <summary> Класс, обеспечивающий создание <see cref="ConvNeuralNet"/>.</summary>
	public class ConvNeuralNetFactory : IDetectionAlgorithmFactory
	{
		/// <summary> Производит объект <see cref="ConvNeuralNet"/> с установленными параметрами. </summary>
		/// <returns> Возвращает созданный объект <see cref="IDetectionAlgorithm"/> . </returns>
		public IDetectionAlgorithm CreateDetectionAlgorithm()
		{
			return new ConvNeuralNet(new TFGraph(), new Size(128, 96));
		}
	}
}
