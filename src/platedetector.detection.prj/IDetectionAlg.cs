using OpenCvSharp;

using System.Collections.Generic;

namespace Platedetector.Detection
{
	/// <summary> Интерфейс алгоритма локализации. </summary>
	public interface IDetectionAlg
	{
        /// <summary> Шаблон результата локализации. </summary>
        DetectionResultPattern Pattern { get; }

		/// <summary> Загрузка параметров алгоритма из файла. </summary>
		/// <param name="filename"> Путь к файлу. </param>
		void Load(string filename);

		/// <summary> Предсказывает местоположения объектов на изображении. </summary>
		/// <param name="image"> Анализируемое изображение. </param>
		/// <returns> Список ограничивающих прямоугольников <see cref="OpenCvSharp.Rect"/>. </returns>
		IReadOnlyList<Detection> Predict(Mat image);
	}
}
