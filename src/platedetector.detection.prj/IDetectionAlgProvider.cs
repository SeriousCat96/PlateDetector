namespace Platedetector.Detection
{
	/// <summary> Интерфейс, обеспечивающий создание <see cref="IDetectionAlg"/>.</summary>
	public interface IDetectionAlgProvider
	{
		/// <summary> Производит объект <see cref="IDetectionAlg"/> с установленными параметрами. </summary>
		/// <returns> Возвращает созданный объект <see cref="IDetectionAlg"/> . </returns>
		IDetectionAlg CreateDetectionAlgorithm();
	}
}
