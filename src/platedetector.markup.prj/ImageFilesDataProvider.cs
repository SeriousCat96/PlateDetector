using System;
using System.Collections.Generic;
using System.IO;

namespace Platedetector.Markup
{
    /// <summary> Предоставляет данные о файлах и каталоге изображений. </summary>
    public class ImageFilesDataProvider
	{
		#region Data

		private string _file;

		#endregion

		#region Properties

        /// <summary> Текущий файл. </summary>
		public string File
		{
			get
			{
				return _file;
			}
			set
			{
				var fileInfo = new FileInfo(value);

				if(!fileInfo.Exists)
				{
					throw new FileNotFoundException("Файл не найден", value);
				}

				_file = value;

				if(fileInfo.DirectoryName != Folder)
				{
					Folder = fileInfo.DirectoryName;
					OnFileChanged(new FileChangedEventArgs(value, fileInfo.DirectoryName));
				}
				else
				{
					OnFileChanged(new FileChangedEventArgs(value, null));
				}
				
			}
		}

        /// <summary> Текущий каталог. </summary>
		public string Folder { get; set; }

		#endregion

		#region Events
		/// <summary> Возникает при изменении файла в текущем каталоге. </summary>
		public event EventHandler<FileChangedEventArgs> FileChanged;

		private void OnFileChanged(FileChangedEventArgs e)
		{
			FileChanged?.Invoke(this, e);
		}

		#endregion

		#region Methods

		/// <summary> Получает список всех файлов с изображениями в текущем каталоге. </summary>
		/// <returns> Список всех файлов с изображениями в текущем каталоге. </returns>
		public IList<string> GetFiles()
		{
			var files = Directory.EnumerateFiles(Folder);

			var sorted = new List<string>();
			foreach(var file in files)
			{
				if(file.EndsWith(".jpg", StringComparison.CurrentCultureIgnoreCase)
					|| file.EndsWith(".jpeg", StringComparison.CurrentCultureIgnoreCase)
					|| file.EndsWith(".png", StringComparison.CurrentCultureIgnoreCase)
					|| file.EndsWith(".bmp", StringComparison.CurrentCultureIgnoreCase))
				{
					sorted.Add(file);
				}
			}

			return sorted;
		}

		#endregion
	}

	public class FileChangedEventArgs : EventArgs
	{
		public FileChangedEventArgs(string file, string folder)
		{
			Folder = folder;
			File = file;
		}

		public string File { get; }

		public string Folder { get; }
	}
}
