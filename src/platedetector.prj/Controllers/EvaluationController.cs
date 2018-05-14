using OpenCvSharp;
using OpenCvSharp.UserInterface;

using PlateDetector.Detection;
using PlateDetector.Evaluation;
using PlateDetector.GUI;
using PlateDetector.Logging;
using PlateDetector.Markup;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlateDetector.Controllers
{
    public class EvaluationController
    {
        #region .ctor

        public EvaluationController(IEnumerable<IMetric> metrics, Detector detector, PictureBoxIpl picBox, Log log)
        {
            Metrics          = metrics;
            MarkupImporter   = new MarkupImporter();
            PicBox           = picBox;
            SwitchController = new ImageSwitchController(PicBox);
            Detector         = detector;
            Log              = log;
        }

        #endregion

        #region Properties

        public string Folder
        {
            get
            {
                return SwitchController
                    .DataProvider
                    .Folder;
            }
            set
            {
                SwitchController
                    .DataProvider
                    .Folder = value;

                try
                { 
                    var files = SwitchController
                        .DataProvider
                        .GetFiles();

                    SwitchController
                        .Items = files;

                    SwitchController
                        .DataProvider
                        .File = files[0];
                }
                catch(IndexOutOfRangeException)
                {
                    Log.Error("Отсутствуют изображения в папке");
                }
                catch(Exception exc)
                {
                    Log.Error(exc.Message);
                }
            }
        }

        public Detector Detector { get; }

        public Log Log { get; }

        public MarkupImporter MarkupImporter { get; }

        public IEnumerable<IMetric> Metrics { get; }

        public PictureBoxIpl PicBox { get; }

        public ImageSwitchController SwitchController { get; }

        #endregion

        #region Methods

        public void Evaluate(IProgress<ProgressReport> progress, CancellationToken token)
        {
            if (Folder == null)
            {
                Log.Error("Не выбран каталог.");
                return;
            }

            foreach (var file in SwitchController.Items)
            { 
                try
                {
                    token.ThrowIfCancellationRequested();

                    var groundTruth = MarkupImporter
                        .ImportRegions(file)
                        .ToList();
                    var predicted = Detector
                        .Detect(PicBox.ImageIpl)
                        .GetDetectionsList()
                        .Select(e => e.Region)
                        .ToList();

                    foreach (var metric in Metrics)
                    {
                        metric.Stash(groundTruth, predicted);
                    }

                    ReportProgress(progress);
                    token.ThrowIfCancellationRequested();
                }
                catch(OperationCanceledException exc)
                {
                    Log.Info(exc.Message);

                    return;
                }
                catch(FileNotFoundException exc)
                {
                    Log.Warning(exc.Message);
                }
                catch(InvalidOperationException exc)
                {
                    Log.Warning(exc.Message);
                }
                catch(Exception exc)
                {
                    Log.Error(exc.Message);
                }
            }

            foreach (var metric in Metrics)
            {
                var value = metric.Compute();

                Log.Info($"{metric}: {value}");
            }
        }

        private void ReportProgress(IProgress<ProgressReport> progress)
        {
            var curPosition = SwitchController.CurPosition;
            var itemsCount  = SwitchController.Items.Count;
            var percent     = (int)((float)curPosition / itemsCount * 100);

            progress?.Report(new ProgressReport()
            {
                CurPosition = SwitchController.CurPosition,
                ItemsCount  = SwitchController.Items.Count,
                File        = SwitchController.DataProvider.File,
            });
        }

        #endregion
    }
}
