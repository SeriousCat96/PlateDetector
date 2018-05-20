using OpenCvSharp;

using TensorFlow;

namespace Platedetector.Detection.Core
{
	/// <summary> Класс, обеспечивающий создание <see cref="FasterRcnnRpn"/>.</summary>
	public sealed class FasterRcnnRpnProvider : IDetectionAlgProvider
	{
        /// <summary> Производит объект <see cref="FasterRcnnRpn"/> с установленными параметрами. </summary>
        /// <returns> Возвращает созданный объект <see cref="IDetectionAlg"/> . </returns>
        public IDetectionAlg CreateDetectionAlgorithm()
		{
			return new FasterRcnnRpn(
                computationalGraph: new TFGraph(),
                inputImageSize: new Size(224, 224));
		}
	}
}
