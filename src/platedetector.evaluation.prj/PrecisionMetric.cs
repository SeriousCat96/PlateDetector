using OpenCvSharp;
using System;
using System.Collections.Generic;

namespace Platedetector.Evaluation
{
    /// <summary> Оценивает насколько точно обнаруженные объекты не являются ложными срабатываниями. </summary>
    public sealed class PrecisionMetric : IMetric
    {
        public PrecisionMetric()
        {
            NumTruePositive = 0;
            NumDetections   = 0;
        }
        
        /// <summary> Количество обнаружений. </summary>
        public int NumDetections { get; set; }

        /// <summary> Количество правильно обнаруженных объектов. </summary>
        public int NumTruePositive { get; set; }

        /// <summary> Вычислить метрику. </summary>
        /// <returns> Возвращает значение метрики. </returns>
        public double Compute()
        {
            double p = 0.0;

            if (NumDetections > 0)
            {
                p = (double)NumTruePositive / NumDetections;
            }

            return p;
        }

        /// <summary> Сбор данных для вычисления метрики. </summary>
        /// <param name="groundTruth"> Истинные координаты объектов. </param>
        /// <param name="predicted"> Предсказанные координаты объектов.</param>
        public void Stash(IReadOnlyList<Rect> groundTruth, IReadOnlyList<Rect> predicted)
        {
            var numTruePositive = Overlap.NumTruePositives(predicted, groundTruth);

            NumTruePositive += numTruePositive;
            NumDetections   += predicted.Count;
        }

        public override string ToString()
        {
            return "Precision";
        }
    }
}
