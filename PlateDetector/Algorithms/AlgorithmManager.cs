using System;
using System.Collections.Generic;
using System.Linq;

namespace PlateDetector.Algorithms
{
	public class AlgorithmManager
	{
		#region .ctor

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

		public List<IDetectionAlgorithm> Algorithms { get; }
		public IDetectionAlgorithm SelectedAlgorithm { get; protected set; }
		#endregion

		#region Methods

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
			}
		} 
		#endregion
	}
}
