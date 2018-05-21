using System;
using System.Collections.Generic;
using System.Linq;

namespace Platedetector.Detection
{
	/// <summary> Реализует менеджер алгоритмов, способный переключаться между алгоритмами локализации. </summary>
	public class AlgManager
	{
		#region Events

		/// <summary> Возникает при изменении алгоритма. </summary>
		public event EventHandler<AlgChangedEventArgs> AlgorithmChanged;

		/// <summary> Вызов события <see cref="AlgChanged"/>.</summary>
		/// <param name="e"> Аргументы события.</param>
		private void OnAlgorithmChanged(AlgChangedEventArgs e)
		{
			AlgorithmChanged?.Invoke(this, e);
		}
        #endregion

        #region .ctor

        /// <summary> Создает <see cref="AlgManager"/>. </summary>
        /// <param name="providers"> Фабрики алгоритмов локализации. </param>
        public AlgManager(params IDetectionAlgProvider[] providers)
		{
			Algorithms = new List<IDetectionAlg>();

            if(providers.Length > 0)
            {
                foreach (var factory in providers)
                {
                    var algorithm = factory.CreateDetectionAlgorithm();
                    Algorithms.Add(algorithm);
                }

                SelectedAlgorithm = Algorithms[0];
            }
		}
		#endregion

		#region Properties

		/// <summary> Список алгоритмов локализации. </summary>
		public IList<IDetectionAlg> Algorithms { get; }

		/// <summary> Выбранный алгоритм локализации. </summary>
		public IDetectionAlg SelectedAlgorithm { get; protected set; }
		#endregion

		#region Methods

		/// <summary> Задает алгоритм текущим. </summary>
		/// <param name="type">Тип данных алгоритма локализации. </param>
		public void Select(Type type)
		{
			var types = Algorithms
				.Select(e => e.GetType())
				.ToList();

			if(types.Contains(type))
			{
				SelectedAlgorithm = Algorithms[types.IndexOf(type)];
				OnAlgorithmChanged(new AlgChangedEventArgs(SelectedAlgorithm));				
			}
			else
			{
				throw new ArgumentException("Данный тип алгоритма отсутствует.", nameof(type));
			}
		} 
		#endregion
	}

	/// <summary> Аргументы события <see cref="AlgManager.AlgChanged"/></summary>
	public class AlgChangedEventArgs : EventArgs
	{
		public AlgChangedEventArgs(IDetectionAlg selectedAlgorithm)
		{
			SelectedAlgorithm = selectedAlgorithm;
		}

		/// <summary> Выбранный алгоритм. </summary>
		public IDetectionAlg SelectedAlgorithm { get; }
	}
}
