using System.Collections.Generic;
using System.Drawing;

namespace PlateDetector.Algorithms
{
	/// <summary> Интерфейс алгоритма локализации. </summary>
	public interface IDetectionAlgorithm
	{
		/// <summary> Загрузка параметров алгоритма из файла. </summary>
		/// <param name="filename"> Путь к файлу. </param>
		void Load(string filename);

		/// <summary> Предсказывает местоположения объектов на изображении. </summary>
		/// <param name="image"> Анализируемое изображение. </param>
		/// <returns> Список ограничивающих прямоугольников <see cref="Rectangle"/>. </returns>
		List<Rectangle> Predict(Bitmap image);
	}
}
