namespace PlateDetector.Algorithms
{
	/// <summary> Интерфейс, обеспечивающий создание <see cref="IDetectionAlgorithm"/>.</summary>
	public interface IDetectionAlgorithmFactory
	{
		/// <summary> Производит объект <see cref="IDetectionAlgorithm"/> с установленными параметрами. </summary>
		/// <returns> Возвращает созданный объект <see cref="IDetectionAlgorithm"/> . </returns>
		IDetectionAlgorithm CreateDetectionAlgorithm();
	}
}
