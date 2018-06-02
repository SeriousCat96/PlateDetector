using OpenCvSharp;

using System;

namespace Platedetector.Detection.Utils
{
	/// <summary> Хранит методы-расширения класса <see cref="Mat"/>.</summary>
	public static class MatExtensions
	{
        public static void AddRectangle(this Mat mat, Rect rect, Scalar color, Size controlSize)
        {
            var imageSize = mat.Size();

            var factor = ((float)controlSize.Width / imageSize.Width +
                          (float)controlSize.Height / imageSize.Height) * 0.5;
            var thickness = (int)Math.Round(1.5 / factor);

            mat.Rectangle(rect, color, thickness, LineTypes.Link8);
        }

        /// <summary> Преобразует Mat в цвет в массив float</summary>
        /// <param name="bmp"> Исходное изображение </param>
        /// <returns> Возвращает 4-мерный массив пикселей float со значениями в диапазоне [0, 1]. </returns>
        public unsafe static NdArray<float> ToFloatArray(this Mat img)
		{
			int width = img.Width,
				height = img.Height;
			NdArray<float> res = new NdArray<float>(new float[1, height, width, 3]);

			int stride = img.Channels();

			byte* curpos;
			fixed (float* _res = res.Value)
			{
				float* _r = _res, _g = _res + 1, _b = _res + 2;
				for(int h = 0; h < height; h++)
				{
					curpos = (byte*)img.Ptr(h);
					for(int w = 0; w < width; w++)
					{
						*_b = *(curpos++) / 128f - 1; _b += 3;
						*_g = *(curpos++) / 128f - 1; _g += 3;
						*_r = *(curpos++) / 128f - 1; _r += 3;
					}
				}
			}
			
			return res;
		}
	}
}
