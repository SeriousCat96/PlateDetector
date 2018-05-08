﻿using OpenCvSharp;

using System;
using System.IO;
using System.Text;

namespace PlateDetector.Logging
{
	/// <summary> Реализует логирование информации. </summary>
	public sealed class Log : IDisposable
	{
		#region Constants

		/// <summary> Имя лог-файла. </summary>
		private const string FileName = "logs.log";
		#endregion

		#region Data

		/// <summary> Поток для записи текста в лог-файл.</summary>
		private StreamWriter _streamWriter;
		#endregion

		#region .ctor

		/// <summary> Создаёт <see cref="Log"/>. </summary>
		public Log()
		{
			Initialize();
		}
		#endregion

		#region Properties

		/// <summary> Возвращает значение, указывающее, был ли элемент управления помечен <seealso cref="GC"/> как Disposed. </summary>
		public bool IsDisposed { get; private set; }
		#endregion

		#region Events

		/// <summary> Происходит при добавлении записи в лог-файл. </summary>
		public event EventHandler<LogEventArgs> LogFileUpdated;

		/// <summary> Вызывает событие <see cref="LogFileUpdated"/>. </summary>
		/// <param name="e"> Данные события <see cref="LogFileUpdated"/>. </param>
		private void OnLogFileUpdated(LogEventArgs e)
		{
			LogFileUpdated?.Invoke(this, e);
		}
		#endregion

		#region Methods

		/// <summary> Выполняет определяемые приложением задачи, связанные с удалением, высвобождением или сбросом неуправляемых ресурсов. </summary>
		public void Dispose()
		{
			if(_streamWriter != null)
			{
				_streamWriter.Close();
				_streamWriter.Dispose();
				_streamWriter = null;
			}

			IsDisposed = true;
		}

		/// <summary> Отправляет в лог информационное сообщение. </summary>
		/// <param name="message"> Сообщение для лога. </param>
		public void Info(string message)
		{
			Message(new LogInfoMessage(message));
		}

		/// <summary> Отправляет в лог сообщение о детектировании. </summary>
		/// <param name="detection"> Найденный объект. </param>
		public void Detection(Detection.Detection detection)
		{
			Message(new LogDetectionMessage(detection));
		}

		/// <summary> Отправляет в лог сообщение об ошибке. </summary>
		/// <param name="message"> Сообщение для лога. </param>
		public void Error(string message)
		{
			Message(new LogErrorMessage(message));
		}

		/// <summary> Отправляет в лог предупреждающее сообщение. </summary>
		/// <param name="message"> Сообщение для лога. </param>
		public void Warning(string message)
		{
			Message(new LogWarningMessage(message));
		}

		/// <summary> Инициализирует поля класса. </summary>
		private void Initialize()
		{
			_streamWriter = new StreamWriter(FileName, true, Encoding.GetEncoding("Windows-1251"));
		}

		/// <summary> Реализует отправку сообщения в лог. </summary>
		/// <param name="message"> Сообщение для лога. </param>
		private void Message(ILogMessage message)
		{
			_streamWriter.WriteLine(message.ToString());
			_streamWriter.Flush();

			OnLogFileUpdated(new LogEventArgs(message));
		}

		#endregion

	}
}
