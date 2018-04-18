using System;
using System.Diagnostics;
using System.Drawing;
using OpenCvSharp;

namespace PlateDetector.Detection
{
	/// <summary> Реализует детектор номеров. </summary>
	public class Detector
	{
		#region Data

		/// <summary> Менеджер алгоритмов локализации. </summary>
		private AlgManager _manager;

		/// <summary> Таймер для измерения времени работы алгоритма. </summary>
		private Stopwatch _timer;
		#endregion

		#region Events
		public event EventHandler<DetectionEventArgs> Detected;

		#endregion

		#region .ctor

		/// <summary> Создаёт <see cref="Detector"/>. </summary>
		/// <param name="manager">Менеджер алгоритмов локализации. </param>
		public Detector(AlgManager manager)
		{
			_manager = manager;
			_timer	 = new Stopwatch();

		}
		#endregion

		#region Properties

		/// <summary> Обрабатываемое изображение. </summary>
		public Mat Image { get; set; }

		/// <summary> Текущий алгоритм локализации </summary>
		public IDetectionAlg SelectedAlgorithm
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
				var detections = algorithm.Predict(Image);
				_timer.Stop();

				var elapsedTime = _timer.Elapsed;

				var result = new DetectionResult(detections, elapsedTime);

				if(detections.Count > 0)
				{
					OnDetected(new DetectionEventArgs(result));
				}

				return result;
			}

			else throw new NullReferenceException();
		}

		#endregion
	}

	public sealed class DetectionEventArgs : EventArgs
	{
		public DetectionEventArgs(DetectionResult detections)
		{
			Detections = detections;
		}

		public DetectionResult Detections { get; }
	}
}
