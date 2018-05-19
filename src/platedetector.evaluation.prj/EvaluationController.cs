using OpenCvSharp;

using Platedetector.Detection;
using Platedetector.Markup;
using Platedetector.Utils.Logging;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Platedetector.Evaluation
{
    /// <summary> Выполняет оценивание алгоритма заданными метриками. </summary>
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

        /// <summary> Предоставляет данные о файлах и каталоге изображений. </summary>
        public ImageFilesDataProvider DataProvider { get; }

        /// <summary> Детектор номеров. </summary>
        public Detector Detector { get; }

        /// <summary> Каталог с тестовой выборкой изображений. </summary>
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

        /// <summary> Лог. </summary>
        public Log Log { get; }

        /// <summary> Загрузчик размеченных областей. </summary>
        public MarkupImporter MarkupImporter { get; }

        /// <summary> Список метрик. </summary>
        public IEnumerable<IMetric> Metrics { get; }

        #endregion

        #region Methods
        /// <summary> Выполняет оценку алгоритма на заданной выборке в отдельном потоке. </summary>
        /// <param name="progress"> Прогресс. </param>
        /// <param name="token"> Токен отмены. </param>
        /// <returns></returns>
        public Task EvaluateAsync(IProgress<ProgressReport> progress, CancellationToken token)
        {
            return Task.Run(() =>
            {
                if (Folder == null)
                {
                    Log.Error("Не выбран каталог.");
                    return;
                }

                var files = DataProvider.GetFiles();
                var detectionTimeList = new List<double>();

                int filesProceed = 0;

                foreach (var file in files)
                {
                    try
                    {
                        token.ThrowIfCancellationRequested();

                        var groundTruth = MarkupImporter
                            .ImportRegions(file)
                            .ToList();
                        var detections = Detector
                            .Detect(new Mat(file));

                        detectionTimeList.Add(detections.ElapsedTime.TotalSeconds);

                        var predicted = detections
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
                            ItemsCount = files.Count,
                            File = file,
                        });

                        filesProceed++;

                        token.ThrowIfCancellationRequested();
                    }
                    catch (OperationCanceledException exc)
                    {
                        Log.Info(exc.Message);

                        return;
                    }
                    catch (FileNotFoundException exc)
                    {
                        Log.Warning(exc.Message);
                    }
                    catch (InvalidOperationException exc)
                    {
                        Log.Warning(exc.Message);
                    }
                    catch (Exception exc)
                    {
                        Log.Error(exc.Message);
                    }
                }

                var minTimeInterval = detectionTimeList.Min();
                var meanTimeInterval = detectionTimeList.Average();
                var maxTimeInterval = detectionTimeList.Max();

                Log.Info($"Обработано изображений: {filesProceed}");
                Log.Info($"Минимальное время обработки: {minTimeInterval} сек");
                Log.Info($"Среднее время обработки: {meanTimeInterval} сек");
                Log.Info($"Максимальное время обработки: {maxTimeInterval} сек");

                foreach (var metric in Metrics)
                {
                    var value = metric.Compute();

                    Log.Info($"{metric}: {value}");
                }
            });
        }

        /// <summary> Выполняет отчет о прогрессе выполнения оценки. </summary>
        /// <param name="progress"> Прогресс. </param>
        /// <param name="report"> Отчет. </param>
        private void ReportProgress(IProgress<ProgressReport> progress, ProgressReport report)
        { 
            progress?.Report(report);
        }

        #endregion
    }
}
