using OpenCvSharp;

using TensorFlow;

namespace PlateDetector.Detection
{
	/// <summary> Класс, обеспечивающий создание <see cref="FasterRcnnModel"/>.</summary>
	public class FasterRcnnProvider : IDetectionAlgProvider
	{
		/// <summary> Производит объект <see cref="FasterRCNNModel"/> с установленными параметрами. </summary>
		/// <returns> Возвращает созданный объект <see cref="IDetectionAlg"/> . </returns>
		public IDetectionAlg CreateDetectionAlgorithm()
		{
			return new FasterRcnnModel(new TFGraph(), new Size(224, 224));
		}
	}
}
