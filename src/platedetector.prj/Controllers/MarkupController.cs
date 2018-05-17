using OpenCvSharp;
using OpenCvSharp.UserInterface;

using PlateDetector.Logging;
using PlateDetector.Markup;

using System;
using System.Collections;
using System.Collections.Generic;

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
            SelectionController = new RegionSelectionController(picBox);
            Log = log;
        }

        #endregion

        #region Properties

        public IEnumerable<Rect> GtBoxes { get; private set; }

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

            if(IsMarkupOn)
            {
                try
                { 
                    GtBoxes = MarkupImporter.ImportRegions(uri, SelectionController);
                    if (GtBoxes != null)
                    {
                        SelectionController.SelectRegions(GtBoxes);
                    }
                }
                catch(InvalidOperationException)
                {
                    GtBoxes = null;
                    PicBox.RefreshIplImage(SelectionController.Image);
                    throw;
                }
            }

            PicBox.RefreshIplImage(SelectionController.Image);
        }

        #endregion
    }
}