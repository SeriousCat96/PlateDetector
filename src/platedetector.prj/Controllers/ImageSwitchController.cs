using OpenCvSharp;
using OpenCvSharp.UserInterface;

using PlateDetector.Imaging;

using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateDetector.Controllers
{
	public sealed class ImageSwitchController
	{
		#region Data
		/// <summary> Список путей к файлам с изображениями. </summary>
		private IList<string> _items;

		private int _curPosition;

		#endregion

		#region .ctor

		public ImageSwitchController(PictureBoxIpl picBox)
		{
			PicBox		 = picBox;
			DataProvider = new ImageFilesDataProvider();
			DataProvider.FileChanged += OnFileChanged;
		}

		~ImageSwitchController()
		{
			if(DataProvider != null)
			{
				DataProvider.FileChanged -= OnFileChanged;
			}
		}
		#endregion

		#region Properties

		/// <summary> Контрол <see cref="PictureBoxIpl"/> для отображения изображения. </summary>
		public PictureBoxIpl PicBox { get; }

		public ImageFilesDataProvider DataProvider { get; set; }

		#endregion

		#region Methods

		public void MoveNext()
		{
			MoveTo(_curPosition + 1);
		}

		public void MoveBack()
		{
			MoveTo(_curPosition - 1);
		}

		private void MoveTo(int position)
		{
			if(position < 0)
			{
				position = position + _items.Count;
			}

			_curPosition = position % _items.Count;
		
			DataProvider.File = _items[_curPosition];
		}

		#endregion

		#region EventHandlers

		private void OnFileChanged(object sender, FileChangedEventArgs e)
		{
			if (e.Folder != null)
			{
				_items = DataProvider.GetFiles();
			}

			_curPosition = _items.IndexOf(e.File);

			PicBox.RefreshIplImage(new Mat(e.File));

			GC.Collect();
		}

		#endregion
	}
}
