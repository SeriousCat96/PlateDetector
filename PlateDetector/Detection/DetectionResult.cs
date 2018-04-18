using System.Collections.Generic;
using System;

namespace PlateDetector.Detection
{
	/// <summary> Представляет результат локализации в виде списка объектов <see cref="Detection"/>. </summary>
	public class DetectionResult
	{
		#region Data
		private List<Detection> _detections;

		#endregion
		#region .ctor
		/// <summary> Создает <see cref="DetectionResult"/>. </summary>
		/// <param name="detections"> Список обнаруженных объектов <see cref="Detection"/>.</param>
		/// /// <param name="elapsedTime"> Время работы алгоритма.</param>
		public DetectionResult(List<Detection> detections, TimeSpan elapsedTime)
		{
			_detections	= detections;
			ElapsedTime	= elapsedTime;
		}
		#endregion

		#region Properties
		/// <summary> Время работы алгоритма локализации. </summary>
		public TimeSpan ElapsedTime { get; }
		#endregion

		#region Methods
		/// <summary> Возвращает список обнаруженных объектов <see cref="Detection"/>.</summary>
		/// <returns> Возвращает список обнаруженных объектов <see cref="Detection"/>.</returns>
		public List<Detection> GetDetectionsList() => _detections;
		#endregion
	}
}
