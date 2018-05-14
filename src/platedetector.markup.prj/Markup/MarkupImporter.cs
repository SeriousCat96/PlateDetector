using OpenCvSharp;

using System;
using System.Collections.Generic;
using System.IO;

namespace PlateDetector.Markup
{
	public class MarkupImporter
	{
		#region Properties

		/// <summary> Разметка. </summary>
		public XmlMarkup Markup { get; private set; }

		#endregion

		#region Methods

		/// <summary> </summary>
		/// <param name="uri"></param>
		/// <param name="controller"></param>
		public IEnumerable<Rect> ImportRegions(string uri, RegionSelectionController controller = null)
		{
			CreateIfMarkupFileExists(uri);

			if(Markup.HumanChecked)
			{
				var regions = Markup.GetRegions();
                if(controller != null)
                { 
                    controller.SelectRegions(regions);
                }

				return regions;
			}

			else throw new InvalidOperationException("Изображение не размечено вручную.");
		}

		/// <summary> Существует ли файл с разметкой. </summary>
		/// <param name="uri"> Путь к файлу с изображением. </param>
		private void CreateIfMarkupFileExists(string uri)
		{
			var fileInfo = new FileInfo(uri);

			var xmlPath = fileInfo
				.FullName
				.Replace(fileInfo.Extension, ".xml");

			if(File.Exists(xmlPath))
			{
				Markup = new XmlMarkup(xmlPath);
			}
			else
			{
				throw new FileNotFoundException("Файл с разметкой не найден.", nameof(xmlPath));
			}
		} 

		#endregion
	}
}
