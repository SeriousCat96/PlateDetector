using OpenCvSharp;
using OpenCvSharp.UserInterface;

using PlateDetector.Logging;
using PlateDetector.Markup;

using System;

namespace PlateDetector.Controllers
{
    public sealed class MarkupController
    {
        #region Data

        private bool _isMarkupOn;
        #endregion

        #region Events

        /// <summary> Возникает при изменении режима разметки. </summary>
        public event EventHandler MarkupModeChanged;

        private void OnMarkupModeChanged(EventArgs e)
        {
            MarkupModeChanged?.Invoke(this, e);
        }

        #endregion

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
                if (PicBox == null)
                {
                    throw new InvalidOperationException("Picture box is not initialized");
                }
                return PicBox.ImageIpl;
            }
            set
            {
                if (PicBox == null)
                {
                    throw new InvalidOperationException("Picture box is not initialized");
                }
                PicBox.ImageIpl = value;
            }
        }

        public MarkupImporter MarkupImporter { get; }

        public RegionSelectionController SelectionController { get; }

        public bool IsMarkupOn
        {
            get
            {
                return _isMarkupOn;
            }
            set
            {
                _isMarkupOn = value;
                OnMarkupModeChanged(new EventArgs());
            }
        }

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
            SelectionController.Image = OriginalImage.Clone();

            if (IsMarkupOn)
            {
                var gtBoxes = MarkupImporter.ImportRegions(uri, SelectionController);
                if (gtBoxes != null)
                {
                    SelectionController.SelectRegions(gtBoxes);
                }
            }

            PicBox.RefreshIplImage(SelectionController.Image);
        }

        #endregion
    }
}