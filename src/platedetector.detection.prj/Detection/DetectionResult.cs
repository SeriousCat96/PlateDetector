using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PlateDetector.Detection
{
    /// <summary> Представляет результат локализации в виде списка объектов <see cref="Detection"/>. </summary>
    [DataContract]
    public class DetectionResult
	{
		#region Data
        [DataMember]
        [JsonProperty(PropertyName = "detections")]
        private readonly IReadOnlyList<Detection> _detections;

		#endregion

		#region .ctor
		/// <summary> Создает <see cref="DetectionResult"/>. </summary>
		/// <param name="detections"> Список обнаруженных объектов <see cref="Detection"/>.</param>
		/// /// <param name="elapsedTime"> Время работы алгоритма.</param>
		public DetectionResult(IReadOnlyList<Detection> detections, TimeSpan elapsedTime, DetectionResultPattern pattern)
		{
			_detections	= detections;
			ElapsedTime	= elapsedTime;
            Pattern     = pattern;
		}
        #endregion

        #region Properties
        /// <summary> Время работы алгоритма локализации. </summary>
        [DataMember]
        [JsonProperty(PropertyName = "elapsedTime")]
        public TimeSpan ElapsedTime { get; }

        /// <summary> Шаблон результата локализации. </summary>
        [DataMember]
        [JsonProperty(PropertyName = "pattern")]
        public DetectionResultPattern Pattern { get; }
        #endregion

        #region Methods
        /// <summary> Возвращает список обнаруженных объектов <see cref="Detection"/>.</summary>
        /// <returns> Возвращает список обнаруженных объектов <see cref="Detection"/>.</returns>
        public IReadOnlyList<Detection> GetDetectionsList() => _detections;
		#endregion
	}

    public enum DetectionResultPattern : int
    {
        RegionOnly,
        RegionWithProbability,
        RegionWithProbabilityAndCountry,
    }
}
