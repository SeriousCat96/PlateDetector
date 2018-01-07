using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateDetector.Algorithms
{
	public class ConvNeuralNetFactory : IDetectionAlgorithmFactory
	{
		public IDetectionAlgorithm CreateDetectionAlgorithm()
		{
			return new ConvNeuralNet();
		}
	}
}
