using System;
using System.Collections.Generic;
using System.Linq;

namespace PlateDetector.Algorithms
{
	/// <summary> Реализует менеджер алгоритмов, способный переключаться между алгоритмами локализации. </summary>
	public class AlgorithmManager
	{
		#region .ctor

		/// <summary> Создает <see cref="AlgorithmManager"/>. </summary>
		/// <param name="factories"> Абстрактные фабрики алгоритмов локализации. </param>
		public AlgorithmManager(params IDetectionAlgorithmFactory[] factories)
		{
			Algorithms = new List<IDetectionAlgorithm>();

			if(factories != null)
			{
				foreach(var factory in factories)
				{
					var algorithm = factory.CreateDetectionAlgorithm();
					Algorithms.Add(algorithm);
				}
			}
		}
		#endregion

		#region Properties

		/// <summary> Список алгоритмов локализации. </summary>
		public List<IDetectionAlgorithm> Algorithms { get; }

		/// <summary> Выбранный алгоритм локализации. </summary>
		public IDetectionAlgorithm SelectedAlgorithm { get; protected set; }
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

				Console.WriteLine($"Selected alorithm {type.Name}");
			}
			else
			{
				Console.WriteLine($"Algorithm is absent.");
				throw new ArgumentException("Данный тип алгоритма отсутствует.", nameof(type));
			}
		} 
		#endregion
	}
}
