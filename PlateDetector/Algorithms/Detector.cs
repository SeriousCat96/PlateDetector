using System;
using System.Diagnostics;
using System.Drawing;
using OpenCvSharp;

namespace PlateDetector.Algorithms
{
	/// <summary> Реализует детектор номеров. </summary>
	public class Detector
	{
		#region Data

		/// <summary> Менеджер алгоритмов локализации. </summary>
		private AlgorithmManager _manager;

		private Stopwatch _timer;
		#endregion


		#region Events
		public event EventHandler<DetectionEventArgs> Detected;

		#endregion

		#region .ctor

		/// <summary> Создаёт <see cref="Detector"/>. </summary>
		/// <param name="manager">Менеджер алгоритмов локализации. </param>
		public Detector(AlgorithmManager manager)
		{
			_manager = manager;
			_timer	 = new Stopwatch();

		}
		#endregion

		#region Properties

		/// <summary> Обрабатываемое изображение. </summary>
		public Mat Image { get; set; }

		public IDetectionAlgorithm SelectedAlgorithm
		{
			get
			{
				return _manager.SelectedAlgorithm;
			}
		}
		#endregion

		#region Handlers

		private void OnDetected(DetectionEventArgs e)
		{
			Detected?.Invoke(this, e);
		}
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

			if(algorithm != null && Image != null)
			{
				_timer.Restart();
				var boundBoxes = algorithm.Predict(Image);
				_timer.Stop();

				var result = new DetectionResult(boundBoxes);

				if(boundBoxes.Count > 0)
				{
					OnDetected(new DetectionEventArgs(result, _timer.Elapsed));
				}

				return result;
			}

			else throw new NullReferenceException();
		}

		#endregion
	}

	public sealed class DetectionEventArgs : EventArgs
	{
		public DetectionEventArgs(DetectionResult result, TimeSpan time)
		{
			Result = result;
			Time   = time;
		}

		public DetectionResult Result { get; }
		public TimeSpan Time { get; }
	}
}
