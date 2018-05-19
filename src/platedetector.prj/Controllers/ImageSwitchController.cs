using OpenCvSharp;
using OpenCvSharp.UserInterface;

using Platedetector.Markup;

using System;
using System.Collections.Generic;

namespace Platedetector.Controllers
{
    public sealed class ImageSwitchController
	{
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

        /// <summary> Список путей к файлам с изображениями. </summary>
		public IList<string> Items { get; set; }

        public int CurPosition { get; private set; }

        #endregion

        #region Methods

        public void MoveNext()
		{
			MoveTo(CurPosition + 1);
		}

		public void MoveBack()
		{
			MoveTo(CurPosition - 1);
		}

		private void MoveTo(int position)
		{
            if (position < 0)
            {
                position = position + Items.Count;
            }

            CurPosition = position % Items.Count;

            DataProvider.File = Items[CurPosition];
        }

        #endregion

        #region EventHandlers

        private void OnFileChanged(object sender, FileChangedEventArgs e)
		{
			if (e.Folder != null)
			{
				Items = DataProvider.GetFiles();
			}

			CurPosition = Items.IndexOf(e.File);

			PicBox.RefreshIplImage(new Mat(e.File));

			GC.Collect();
		}

		#endregion
	}
}
