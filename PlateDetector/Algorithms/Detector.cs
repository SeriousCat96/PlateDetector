using System;
using System.Drawing;

namespace PlateDetector.Algorithms
{
	/// <summary> Реализует детектор номеров. </summary>
	public class Detector
	{
		#region Data

		/// <summary> Менеджер алгоритмов локализации. </summary>
		private AlgorithmManager _manager;
		#endregion

		#region .ctor

		/// <summary> Создаёт <see cref="Detector"/>. </summary>
		/// <param name="manager">Менеджер алгоритмов локализации. </param>
		public Detector(AlgorithmManager manager)
		{
			_manager = manager;
		}
		#endregion

		#region Properties

		/// <summary> Обрабатываемое изображение. </summary>
		public Bitmap CurrentBitmap { get; set; }
		#endregion

		#region Methods

		/// <summary> Изменяет алгоритм локализации в детекторе. </summary>
		/// <param name="type"></param>
		public void ChangeAlgorithm(Type type)
		{
			_manager.Select(type);
		}
	
		/// <summary> Выполняет локализацию на текущем изображении. </summary>
		/// <returns> Возвращает результат локализации <seealso cref="DetectionResult"/>. </returns>
		public DetectionResult Detect()
		{
			var algorithm = _manager.SelectedAlgorithm;

			if(algorithm != null && CurrentBitmap != null)
			{
				var boundBoxes = algorithm.Predict(CurrentBitmap);
				var result = new DetectionResult(boundBoxes);

				return result;
			}

			else throw new NullReferenceException();
		}

		#endregion
	}
}
