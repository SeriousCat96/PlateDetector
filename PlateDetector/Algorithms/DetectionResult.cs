using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlateDetector.Algorithms
{
	public class DetectionResult
	{
		#region Data

		private List<Rectangle> _boundBoxes;
		#endregion

		#region .ctor

		public DetectionResult(List<Rectangle> boundBoxes)
		{
			_boundBoxes = boundBoxes;
		}
		#endregion

		#region Methods

		public List<Rectangle> GetBoundBoxes()
		{
			return _boundBoxes;
		} 
		#endregion
	}
}
