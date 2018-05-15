using OpenCvSharp;
using OpenCvSharp.UserInterface;

using PlateDetector.Detection;
using PlateDetector.Evaluation;
using PlateDetector.Imaging;
using PlateDetector.Logging;
using PlateDetector.Markup;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace PlateDetector.Controllers
{
    public class EvaluationController
    {       
        #region .ctor

        public EvaluationController(IEnumerable<IMetric> metrics, Detector detector, Log log)
        {
            Metrics          = metrics;
            MarkupImporter   = new MarkupImporter();
            DataProvider     = new ImageFilesDataProvider();
            Detector         = detector;
            Log              = log;
        }

        #endregion

        #region Properties

        public ImageFilesDataProvider DataProvider { get; }

        public Detector Detector { get; }

        public string Folder
        {
            get
            {
                return DataProvider.Folder;
            }
            set
            {
                DataProvider.Folder = value;
            }
        }

        public Log Log { get; }

        public MarkupImporter MarkupImporter { get; }

        public IEnumerable<IMetric> Metrics { get; }

        public PictureBoxIpl PicBox { get; }


        #endregion

        #region Methods

        public void Evaluate(IProgress<ProgressReport> progress, CancellationToken token)
        {
            if (Folder == null)
            {
                Log.Error("Не выбран каталог.");
                return;
            }

            var files = DataProvider.GetFiles();

            foreach (var file in files)
            { 
                try
                {
                    token.ThrowIfCancellationRequested();

                    var groundTruth = MarkupImporter
                        .ImportRegions(file)
                        .ToList();
                    var predicted = Detector
                        .Detect(new Mat(file))
                        .GetDetectionsList()
                        .Select(e => e.Region)
                        .ToList();

                    foreach (var metric in Metrics)
                    {
                        metric.Stash(groundTruth, predicted);
                    }

                    ReportProgress(progress, new ProgressReport()
                    {
                        CurPosition = files.IndexOf(file),
                        ItemsCount  = files.Count,
                        File        = file,
                    });

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

        private void ReportProgress(IProgress<ProgressReport> progress, ProgressReport report)
        { 
            progress?.Report(report);
        }

        #endregion
    }
}
