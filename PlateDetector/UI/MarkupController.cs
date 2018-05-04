using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.UserInterface;
using PlateDetector.Detection;
using PlateDetector.Logging;
using PlateDetector.Markup;

namespace PlateDetector.UI
{
	public sealed class MarkupController
	{
		#region .ctor
		public MarkupController(PictureBoxIpl picBox, Log log)
		{
			PicBox = picBox;
			Image = picBox.ImageIpl;

			MarkupImporter = new MarkupImporter();
			SelectionController = new RegionSelectionController(picBox.ImageIpl);
			Log = log;
		}

		#endregion

		#region Properties

		public PictureBoxIpl PicBox { get; }

		public Mat OriginalImage { get; set; }

		public Mat Image
		{
			get
			{
				if(PicBox == null)
				{
					throw new InvalidOperationException("Picture box is not initialized");
				}
				return PicBox.ImageIpl;
			}
			set
			{
				if(PicBox == null)
				{
					throw new InvalidOperationException("Picture box is not initialized");
				}
				PicBox.ImageIpl = value;
			}
		}

		public MarkupImporter MarkupImporter { get; }

		public RegionSelectionController SelectionController { get; }

		public bool IsMarkupOn { get; set; } = true;

		public Log Log { get; private set; }
		#endregion

		#region Methods

		public void Reload(string uri)
		{
			OriginalImage = new Mat(uri);
			Draw(uri);
		}

		public void Draw(string uri)
		{
			if(IsMarkupOn)
			{
				SelectionController.Image = OriginalImage.Clone();

				var gtBoxes = MarkupImporter.ImportRegions(uri, SelectionController);
				if(gtBoxes != null)
				{
					SelectionController.SelectRegions(gtBoxes);
					PicBox.RefreshIplImage(SelectionController.Image);
				}
			}
		}

		#endregion
	}
}
