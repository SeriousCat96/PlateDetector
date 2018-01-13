﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;

using OpenCvSharp;
using OpenCvSharp.Extensions;

using TensorFlow;

namespace PlateDetector.Algorithms
{
	/// <summary> Реализует сверточную нейронную сеть. </summary>
	public class ConvNeuralNet : IDetectionAlgorithm, IDisposable
	{
		#region Data
		/// <summary> Граф вычислений модели сверточной нейронной сети. </summary>
		private TFGraph _model;
		#endregion

		#region .ctor

		/// <summary> Создает <see cref="ConvNeuralNet"/>. </summary>
		public ConvNeuralNet()
		{
			_model = new TFGraph();
		}
		#endregion

		#region Methods

		/// <summary> Освобождает ресурсы объекта. </summary>
		public void Dispose()
		{
			if(_model != null)
			{
				_model.Dispose();
			}
		}

		/// <summary> Загрузка параметров алгоритма из файла. </summary>
		/// <param name="filename"> Путь к файлу. </param>
		public void Load(string filename)
		{
			var buffer = new TFBuffer(File.ReadAllBytes(filename));
			_model.Import(buffer, "");
		}

		/// <summary> Предсказывает местоположения объектов на изображении. </summary>
		/// <param name="image"> Анализируемое изображение. </param>
		/// <returns> Список ограничивающих прямоугольников <see cref="Rectangle"/>. </returns>
		public List<Rectangle> Predict(Bitmap image)
		{
			using(var session = new TFSession(_model))
			{
				var runner = session.GetRunner();

				var imgArr = Preprocess(image);
				var tensor = new TFTensor(imgArr);
				runner.AddInput(_model["inputs"][0], tensor);
				runner.AddInput(_model["fc2_c_dropout"][0], new TFTensor(1.0f));
				runner.Fetch(_model["outputs"][0]);

				var output = runner.Run();
				TFTensor result = output[0];
				var res = (float[][])result.GetValue(jagged: true);

				var rects = new List<Rectangle>();
				var rect = BitmapUtils.GetRectangle(res[0], image.Width, image.Height);

				rects.Add(rect);
				return rects;
			}
		}

		/// <summary> Предварительная обработка изображения и конвертация пикселей в массив <see cref="float"/>. </summary>
		/// <param name="bitmap"> Исходное изображение. </param>
		/// <returns> Массив float пикселей изображения. </returns>
		private float[,,,] Preprocess(Bitmap bitmap)
		{
			var mat = BitmapConverter.ToMat(bitmap);

			mat = BitmapUtils.Resize(mat, new OpenCvSharp.Size(128, 96));
			var gray = mat.CvtColor(ColorConversionCodes.RGBA2GRAY);

			bitmap = BitmapConverter.ToBitmap(gray);

			return BitmapUtils.BitmapToFloatGrayScale(bitmap);
		}
		 
		#endregion
	}
}
