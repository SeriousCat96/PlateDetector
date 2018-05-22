using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Xml.XPath;
using OpenCvSharp;

namespace Platedetector.Markup
{
    /// <summary> Реализует чтение XML-файлов с разметкой. </summary>
    public class XmlMarkup
	{
		#region .ctor
		/// <summary> Создает <see cref="XmlMarkup"/>.</summary>
		/// <param name="uri"> Путь к файлу с разметкой. </param>
		public XmlMarkup(string uri)
		{
			Load(uri);
		}

        public XmlMarkup()
        {
            
        }
        #endregion

        #region Properties
        /// <summary> Путь к файлу с разметкой. </summary>
        public string Uri { get; private set; }

		/// <summary> XML файл. </summary>
		public XDocument XmlFile { get; private set; }

		/// <summary> Размечен ли файл вручную. </summary>
		public bool HumanChecked
		{
			get
			{
				if(XmlFile != null)
				{
					var humanChecked = bool.Parse(
						XmlFile
							.XPathSelectElement("/Image/HumanChecked")
							.Attribute(XName.Get("Value"))
							.Value
					);
					return humanChecked;
				}
				else throw new NullReferenceException();
			}
		}
		#endregion

		#region Methods

        /// <summary> Получить список размеченных областей. </summary>
        /// <returns></returns>
		public IEnumerable<Rect> GetRegions()
		{
            XmlFile = XmlFile ?? throw new InvalidOperationException($"Свойство {nameof(XmlFile)} неопределено.");

            var plates = XmlFile
                .XPathSelectElement("/Image/Plates")
                .Elements();
			var regions = new List<Rect>();

			foreach(var plate in plates)
			{
				var points = plate
					.Element(XName.Get("Region"))
					.Elements();

				var xList = new List<int>();
				var yList = new List<int>();
				
				foreach(var point in points)
				{
					var x = int.Parse(
						point
							.Attribute(XName.Get("X"))
							.Value);

					var y = int.Parse(
						point
							.Attribute(XName.Get("Y"))
							.Value);

					xList.Add(x);
					yList.Add(y);
				}

				var xmin = xList.Min();
				var ymin = yList.Min();
				var xmax = xList.Max();
				var ymax = yList.Max();

				var rect = new Rect(
					x: xmin,
					y: ymin,
					width: xmax - xmin,
					height: ymax - ymin);

				regions.Add(rect);
			}

			return regions;
		}

        /// <summary> Загрузить файл с разметкой. </summary>
        /// <param name="uri"></param>
		public void Load(string uri)
		{
			Uri	= uri ?? throw new ArgumentNullException(nameof(uri));
			XmlFile = XDocument.Load(Uri);
		}
		#endregion
	}
}
