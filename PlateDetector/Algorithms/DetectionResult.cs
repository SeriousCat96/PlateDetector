using OpenCvSharp;

using System.Collections.Generic;
using System.Drawing;

namespace PlateDetector.Algorithms
{
	/// <summary> Представляет результат локализации в виде списка ограничивающих прямоугольников. </summary>
	public class DetectionResult
	{
		#region Data

		/// <summary> Список координат ограничивающих прямоугольников <see cref="Rectangle"/>. </summary>
		private List<Rect> _boundBoxes;
		#endregion

		#region .ctor

		/// <summary> Создает <see cref="DetectionResult"/>. </summary>
		/// <param name="boundBoxes"> Список координат ограничивающих прямоугольников <see cref="Rectangle"/>.</param>
		public DetectionResult(List<Rect> boundBoxes)
		{
			_boundBoxes = boundBoxes;
		}
		#endregion

		#region Methods

		/// <summary> Получает список координат результата локализации. </summary>
		public List<Rect> GetBoundBoxes()
		{
			return _boundBoxes;
		} 
		#endregion
	}
}
