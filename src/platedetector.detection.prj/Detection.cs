using Newtonsoft.Json;

using OpenCvSharp;

using System.Runtime.Serialization;
using System.Text;

namespace Platedetector.Detection
{
	/// <summary> Обнаруженный объект. </summary>
    [DataContract]
	public sealed class Detection
	{
        #region .ctor

        public Detection(Rect region)
		{
			Region		= region;
			Probability = 0.0f;
			Country		= Country.None;
		}

		public Detection(Rect region, float probability, Country country) : this(region)
		{
			Probability = probability;
			Country		= country;
		}
        
        #endregion

        #region Properties
        /// <summary> Прямоугольная область обнаруженного объекта. </summary>
        public Rect Region { get; }

        [DataMember]
        [JsonProperty(PropertyName = "x")]
        public int X
        {
            get
            {
                return Region.X;
            }
        }

        [DataMember]
        [JsonProperty(PropertyName = "y")]
        public int Y
        {
            get
            {
                return Region.Y;
            }
        }

        [DataMember]
        [JsonProperty(PropertyName = "width")]
        public int Width
        {
            get
            {
                return Region.Width;
            }
        }

        [DataMember]
        [JsonProperty(PropertyName = "height")]
        public int Height
        {
            get
            {
                return Region.Height;
            }
        }

        /// <summary> Предсказанная страна номера. </summary>
        [DataMember]
        [JsonProperty(PropertyName = "country")]
        public Country Country { get; }

        /// <summary> Уверенность модели в результате. </summary>
        [DataMember]
        [JsonProperty(PropertyName = "probability")]
        public float Probability { get; }
		#endregion

		#region Methods

		public override string ToString()
		{
			var sb = new StringBuilder();

			sb.Append($"X = {Region.X}, Y = {Region.Y}, W = {Region.Width}, H = {Region.Height}");
			if(Country != Country.None)
			{
				sb.Append($", Страна: {Country}");
			}
			if(Probability != 0)
			{
				sb.Append($", Вероятность: {Probability}");
			}
			
			return sb.ToString();
		}
		#endregion
	}

	public enum Country : int
	{
		None,
		Abkhazia,
		Austria,
		Belarus,
		Belgium,
		Germany,
		Lithuania,
		Russia,
		Uzbekistan,
		Ukraine,
		Kazakhstan,
		Latvia,
		Finland,
	}
}
