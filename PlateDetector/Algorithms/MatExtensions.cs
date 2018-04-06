using System.Drawing;
using System.Drawing.Imaging;

using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace PlateDetector.Algorithms
{
	/// <summary> Хранит методы-расширения класса <see cref="Mat"/>.</summary>
	public static class MatExtensions
	{
		/// <summary> Преобразует Bitmap в ч/б цвет в массив float</summary>
		/// <param name="bmp"> Исходное изображение </param>
		/// <returns> Возвращает 4-мерный массив пикселей float со значениями в диапазоне [0, 1]. </returns>
		public unsafe static float[,,,] ToFloatArray(this Mat img)
		{
			int width = img.Width,
				height = img.Height;
			float[,,,] res = new float[1, height, width, 3];

			int stride = img.Channels();

			byte* curpos;
			fixed (float* _res = res)
			{
				float* _r = _res, _g = _res + 1, _b = _res + 2;
				for(int h = 0; h < height; h++)
				{
					curpos = (byte*)img.Ptr(h);
					for(int w = 0; w < width; w++)
					{
						*_b = *(curpos++) / 255f; _b += 3;
						*_g = *(curpos++) / 255f; _g += 3;
						*_r = *(curpos++) / 255f; _r += 3;
					}
				}
			}
			
			return res;
		}

		/// <summary> Преобразует Bitmap в RGB цвет в массив float</summary>
		/// <param name="img"> Исходное изображение </param>
		/// <returns> Возвращает 4-мерный массив пикселей float со значениями в диапазоне [0, 1]. </returns>
		public unsafe static float[,,,] ToFloatArrayGrayScale(this Mat img)
		{
			int width = img.Width,
				height = img.Height;
			float[,,,] res = new float[1, height, width, 1];
			int stride = img.Channels();
			
			byte* curpos;
			fixed (float* _res = res)
			{
				float* _r = _res;
				for(int h = 0; h < height; h++)
				{
					curpos = (byte*)img.Ptr(h);
					for(int w = 0; w < width; w++)
					{

						*_r = *(curpos++) / 255f; ++_r;
					}
				}
			}
			
			return res;
		}
	}
}
