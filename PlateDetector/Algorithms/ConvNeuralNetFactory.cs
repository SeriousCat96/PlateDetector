using OpenCvSharp;

using TensorFlow;

namespace PlateDetector.Algorithms
{
	/// <summary> Класс, обеспечивающий создание <see cref="ConvNeuralNet"/>.</summary>
	public class ConvNeuralNetFactory : IDetectionAlgFactory
	{
		/// <summary> Производит объект <see cref="ConvNeuralNet"/> с установленными параметрами. </summary>
		/// <returns> Возвращает созданный объект <see cref="IDetectionAlg"/> . </returns>
		public IDetectionAlg CreateDetectionAlgorithm()
		{
			return new ConvNeuralNet(new TFGraph(), new Size(224, 224));
		}
	}
}
