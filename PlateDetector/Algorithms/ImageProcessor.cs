using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace PlateDetector.Algorithms
{
	public static class ImageProcessor
	{
		public static Bitmap Resize(Bitmap srcImg, System.Drawing.Size newSize)
		{
			var mat = BitmapConverter.ToMat(srcImg);

			var resizedMat = mat.Resize(
				new OpenCvSharp.Size(newSize.Width, newSize.Height),
				fx: 1.0,
				fy: 1.0,
				interpolation: InterpolationFlags.Cubic);

			var result = BitmapConverter.ToBitmap(resizedMat);

			return result;
		}
	}
}
