using OpenCvSharp;
using OpenCvSharp.Extensions;
using System;
using System.Diagnostics;
using System.Drawing;

namespace Platedetector.Detection
{
	/// <summary> Реализует детектор номеров. </summary>
	public class Detector : IDetector
	{
		#region Data

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
			Manager = manager;
			_timer	 = new Stopwatch();

		}
		#endregion

		#region Properties
		/// <summary> Менеджер алгоритмов локализации. </summary>
		public AlgManager Manager { get; }

		/// <summary> Текущий алгоритм локализации </summary>
		public IDetectionAlg SelectedAlgorithm
		{
			get
			{
				return Manager.SelectedAlgorithm;
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
			Manager.Select(type);
		}

        /// <summary> Выполняет локализацию на текущем изображении. </summary>
        /// <param name="image"> Обрабатываемое изображение. </param>
		/// <returns> Возвращает результат локализации <seealso cref="DetectionResult"/>. </returns>
		public DetectionResult Detect(Bitmap image)
        {
            return Detect(BitmapConverter.ToMat(image));
        }

        /// <summary> Выполняет локализацию на текущем изображении. </summary>
        /// <param name="image"> Обрабатываемое изображение. </param>
        /// <returns> Возвращает результат локализации <seealso cref="DetectionResult"/>. </returns>
        public DetectionResult Detect(Mat image)
		{
			var algorithm = Manager.SelectedAlgorithm;

			if(algorithm != null && image != null)
			{
				_timer.Restart();
				var detections = algorithm.Predict(image);
				_timer.Stop();

				var elapsedTime = _timer.Elapsed;

				var result = new DetectionResult(detections, elapsedTime, algorithm.Pattern);

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
