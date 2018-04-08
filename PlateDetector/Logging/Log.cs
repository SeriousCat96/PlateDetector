using System;
using System.IO;
using System.Text;
using OpenCvSharp;
using PlateDetector.Algorithms;

namespace PlateDetector.Logging
{
	/// <summary> Реализует логгирование. </summary>
	public sealed class Log : IDisposable
	{
		#region Constants

		/// <summary> Имя лог-файла. </summary>
		private const string FileName = "LOG.txt";
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

		/// <summary> Происходит обновлении записей в лог-файле. </summary>
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
			Message(new InfoLogMessage(message));
		}

		/// <summary> Отправляет в лог сообщение о детектировании. </summary>
		/// <param name="message"> Сообщение для лога. </param>
		public void Detection(string message, Rect rect)
		{
			Message(new DetectionLogMessage(message, rect));
		}

		/// <summary> Отправляет в лог сообщение об ошибке. </summary>
		/// <param name="message"> Сообщение для лога. </param>
		public void Error(string message)
		{
			Message(new ErrorLogMessage(message));
		}

		/// <summary> Инициализирует поля класса. </summary>
		private void Initialize()
		{
			_streamWriter = new StreamWriter(FileName, true, Encoding.GetEncoding("Windows-1251"));
		}

		/// <summary> Реализует отправку сообщения в лог. </summary>
		/// <param name="messageType"> Тип сообщения. </param>
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
