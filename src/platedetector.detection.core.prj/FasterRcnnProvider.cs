﻿using OpenCvSharp;

using TensorFlow;

namespace Platedetector.Detection.Core
{
    /// <summary> Класс, обеспечивающий создание <see cref="FasterRcnn"/>.</summary>
	public sealed class FasterRcnnProvider : IDetectionAlgProvider
    {
        /// <summary> Производит объект <see cref="FasterRcnn"/> с установленными параметрами. </summary>
        /// <returns> Возвращает созданный объект <see cref="IDetectionAlg"/> . </returns>
        public IDetectionAlg CreateDetectionAlgorithm()
        {
            return new FasterRcnn(
                computationalGraph: new TFGraph(),
                inputImageSize: new Size(224, 224));
        }
    }
}
