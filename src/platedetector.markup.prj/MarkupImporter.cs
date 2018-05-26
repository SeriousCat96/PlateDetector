using OpenCvSharp;

using System;
using System.Collections.Generic;
using System.IO;

namespace Platedetector.Markup
{
	public class MarkupImporter : IDisposable
	{
		#region Properties

		/// <summary> Разметка. </summary>
		public XmlMarkup Markup { get; private set; }

        #endregion

        #region Methods

        public void Dispose()
        {
            Markup?.Dispose();

            Markup = null;
        }

        /// <summary> Выполнить импорт и выделение размеченных областей. </summary>
        /// <param name="uri"> Путь к файлу с изображением. </param>
        /// <param name="controller"> Контроллер выделения областей.</param>
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

        /// <summary> Загрузить разметку если существует ли файл с разметкой. </summary>
        /// <param name="uri"> Путь к файлу с изображением. </param>
        /// <exception cref="FileNotFoundException">.</exception>
        private void CreateIfMarkupFileExists(string uri)
		{
			var fileInfo = new FileInfo(uri);

			var xmlPath = fileInfo
				.FullName
				.Replace(fileInfo.Extension, ".xml");

			if(File.Exists(xmlPath))
			{
                Markup?.Dispose();
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
