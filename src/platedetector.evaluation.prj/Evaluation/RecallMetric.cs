using OpenCvSharp;

using System.Collections.Generic;

namespace PlateDetector.Evaluation
{
    /// <summary> Оценивает насколько точно обнаружены все искомые объекты. </summary>
    public sealed class RecallMetric : IMetric
    {
        public RecallMetric()
        {
            NumTruePositive = 0;
            NumGtBoxes      = 0;
        }

        /// <summary> Количество истинных областей. </summary>
        public int NumGtBoxes { get; set; }

        /// <summary> Количество правильно обнаруженных объектов. </summary>
        public int NumTruePositive { get; set; }

        /// <summary> Вычислить метрику. </summary>
        /// <returns> Возвращает значение метрики. </returns>
        public double Compute()
        {
            var r = (double)NumTruePositive / NumGtBoxes;

            return r;
        }

        /// <summary> Сбор данных для вычисления метрики. </summary>
        /// <param name="groundTruth"> Истинные координаты объектов. </param>
        /// <param name="predicted"> Предсказанные координаты объектов.</param>
        public void Stash(IReadOnlyList<Rect> groundTruth, IReadOnlyList<Rect> predicted)
        {
            var numTruePositive = Overlap.NumTruePositives(predicted, groundTruth);

            NumTruePositive += numTruePositive;
            NumGtBoxes      += groundTruth.Count;
        }

        public override string ToString()
        {
            return "Recall";
        }
    }
}
