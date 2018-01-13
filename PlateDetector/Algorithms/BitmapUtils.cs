using System.Drawing;
using System.Drawing.Imaging;

using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace PlateDetector.Algorithms
{
	public static class BitmapUtils
	{
		public unsafe static float[,,,] BitmapToFloatRgb(Bitmap bmp)
		{
			int width = bmp.Width,
				height = bmp.Height;
			float[,,,] res = new float[1, height, width, 3];
			BitmapData bd = bmp.LockBits(
				new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, bmp.PixelFormat);
			try
			{
				byte* curpos;
				fixed (float* _res = res)
				{
					float* _r = _res, _g = _res + 1, _b = _res + 2;
					for(int h = 0; h < height; h++)
					{
						curpos = ((byte*)bd.Scan0) + h * bd.Stride;
						for(int w = 0; w < width; w++)
						{
							*_b = *(curpos++) / 255f; _b += 3;
							*_g = *(curpos++) / 255f; _g += 3;
							*_r = *(curpos++) / 255f; _r += 3;
						}
					}
				}
			}
			finally
			{
				bmp.UnlockBits(bd);
			}
			return res;
		}

		public unsafe static float[,,,] BitmapToFloatGrayScale(Bitmap bmp)
		{
			int width = bmp.Width,
				height = bmp.Height;
			float[,,,] res = new float[1, height, width, 1];
			BitmapData bd = bmp.LockBits(
				new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, bmp.PixelFormat);
			try
			{
				byte* curpos;
				fixed (float* _res = res)
				{
					float* _r = _res;
					for(int h = 0; h < height; h++)
					{
						curpos = ((byte*)bd.Scan0) + h * bd.Stride;
						for(int w = 0; w < width; w++)
						{
							*_r = *(curpos++) / 255f; ++_r;
						}
					}
				}
			}
			finally
			{
				bmp.UnlockBits(bd);
			}
			return res;
		}

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

		public static Mat Resize(Mat mat, OpenCvSharp.Size newSize)
		{
			var resizedMat = mat.Resize(
				newSize,
				fx: 1.0,
				fy: 1.0,
				interpolation: InterpolationFlags.Cubic);

			return resizedMat;
		}


		public static Rectangle GetRectangle(float[] coord, int width, int height)
		{
			for(int i = 0; i < coord.Length; i++)
			{
				coord[i] = TransormCood(coord[i], i % 2 == 0 ? width : height, "0..1");
			}

			return new Rectangle((int)coord[0], (int)coord[1], (int)coord[2] - (int)coord[0], (int)coord[3] - (int)coord[1]);
		}

		private static float TransormCood(float x, int a, string type = "None")
		{
			if(type == "0..1")
				return a * x;
			else return x;
		}
	}
}
