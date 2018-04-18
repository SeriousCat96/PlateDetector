using OpenCvSharp;

using TensorFlow;

namespace PlateDetector.Detection
{
	/// <summary> Класс, обеспечивающий создание <see cref="FasterRCNNModel"/>.</summary>
	public class FasterRCNNFactory : IDetectionAlgFactory
	{
		/// <summary> Производит объект <see cref="FasterRCNNModel"/> с установленными параметрами. </summary>
		/// <returns> Возвращает созданный объект <see cref="IDetectionAlg"/> . </returns>
		public IDetectionAlg CreateDetectionAlgorithm()
		{
			return new FasterRCNNModel(new TFGraph(), new Size(224, 224));
		}
	}
}
