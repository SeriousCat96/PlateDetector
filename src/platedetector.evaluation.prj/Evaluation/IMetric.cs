using OpenCvSharp;
using System.Collections.Generic;

namespace PlateDetector.Evaluation
{
    /// <summary> Интерфейс метрики качества. </summary>
    public interface IMetric
	{
        /// <summary> Количество правильно обнаруженных объектов. </summary>
        int NumTruePositive { get; set; }

        /// <summary> Вычислить метрику. </summary>
        /// <returns> Возвращает значение метрики. </returns>
        double Compute();

        /// <summary> Сбор данных для вычисления метрики. </summary>
        /// <param name="groundTruth"> Истинные координаты объектов. </param>
        /// <param name="predicted"> Предсказанные координаты объектов.</param>
        void Stash(IReadOnlyList<Rect> groundTruth, IReadOnlyList<Rect> predicted);
	}
}
