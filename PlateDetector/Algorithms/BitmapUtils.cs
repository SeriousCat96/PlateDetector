using System.Drawing;
using System.Drawing.Imaging;

using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace PlateDetector.Algorithms
{
	//TODO: Переделать класс в BitmapExtensions и MatExtensions, используя механизм расширений классов.
	/// <summary> Обеспечивает функции обработки изображений <see cref="Bitmap"/>.</summary>
	public static class BitmapUtils
	{
		/// <summary> Преобразует Bitmap в ч/б цвет в массив float</summary>
		/// <param name="bmp"> Исходное изображение </param>
		/// <returns> Возвращает 4-мерный массив пикселей float со значениями в диапазоне [0, 1]. </returns>
		public unsafe static float[,,,] BitmapToFloatRgb(Mat img)
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
		public unsafe static float[,,,] BitmapToFloatGrayScale(Mat img)
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

		/// <summary> Преобразование изображения <see cref="Bitmap"/> в новый размер. </summary>
		/// <param name="srcImg"> Исходное изображение. </param>
		/// <param name="newSize"> Новый размер. </param>
		/// <returns> Возвращает <see cref="Bitmap"/> с измененным размером. </returns>
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

		/// <summary> Преобразование массива изображения <see cref="OpenCvSharp.Mat"/> в новый размер</summary>
		/// <param name="srcImg"> Исходное изображение. </param>
		/// <param name="newSize"> Новый размер. </param>
		/// <returns> Возвращает <see cref="OpenCvSharp.Mat"/> с измененным размером. </returns>
		public static Mat Resize(Mat mat, OpenCvSharp.Size newSize)
		{
			var resizedMat = mat.Resize(
				newSize,
				fx: 1.0,
				fy: 1.0,
				interpolation: InterpolationFlags.Cubic);

			return resizedMat;
		}

		/// <summary> Получает <see cref="Rectangle"/> из массива <see cref="float"/>.</summary>
		/// <param name="coord"> Массив координат.</param>
		/// <param name="width"> Исходная ширина изображения.</param>
		/// <param name="height"> Исходная высота изображения.</param>
		/// <returns> Возвращает получившийся <see cref="OpenCvSharp.Rect"/>.</returns>
		public static Rect GetRectangle(float[] coord, int width, int height)
		{
			for(int i = 0; i < coord.Length; i++)
			{
				coord[i] = TransormCood(coord[i], i % 2 == 0 ? width : height, "0..1");
			}

			return new Rect((int)coord[0], (int)coord[1], (int)coord[2] - (int)coord[0], (int)coord[3] - (int)coord[1]);
		}

		/// <summary> Преобразует относительную координату в абсолютную. </summary>
		/// <param name="x"> Исходная относительная координата. </param>
		/// <param name="a"> Множитель для преобразования. </param>
		/// <param name="type"> Тип преобразованиея. </param>
		/// <returns> Возвращает абсолютное значение координаты.</returns>
		private static float TransormCood(float x, int a, string type = "None")
		{
			if(type == "0..1")
				return a * x;
			else return x;
		}
	}
}
