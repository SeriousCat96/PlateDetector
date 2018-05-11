namespace PlateDetector.Detection
{
	/// <summary> Класс, обеспечивающий создание <see cref="HaarCascadeFactory"/>.</summary>
	public class HaarCascadeProvider : IDetectionAlgProvider
	{
		/// <summary> Производит объект <see cref="HaarCascadeModel"/> с установленными параметрами. </summary>
		/// <returns> Возвращает созданный объект <see cref="IDetectionAlg"/> . </returns>
		public IDetectionAlg CreateDetectionAlgorithm()
		{
			return new HaarCascadeModel(
				scaleFactor: 1.25,
				minNeighbours: 5,
				minSize: new OpenCvSharp.Size(80, 20),
				maxSize: new OpenCvSharp.Size(1000, 300));
		}
	}
}
