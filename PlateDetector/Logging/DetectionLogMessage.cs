using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using PlateDetector.Algorithms;

namespace PlateDetector.Logging
{
	public sealed class DetectionLogMessage : ILogMessage
	{
		public DetectionLogMessage(string text, Rect rect)
		{
			Text			 = text;
			Rect			 = rect;
			Type			 = MessageType.Detection;
			Color			 = Color.Green;
			FormattedMessage = ToString();
		}

		public Color Color { get; }
		public Rect Rect { get; }
		public string FormattedMessage { get; }
		public string Text { get; }
		public MessageType Type { get; }

		private string GetFormattedString()
		{
			return $"[{DateTime.Now}][{Type.ToString().ToUpper()}]: {GetRectFormatted(Rect)}";
		}

		private string GetRectFormatted(Rect rect)
		{
			return $"X = {rect.X}, Y = {rect.Y}, Ширина = {rect.Width}, Высота = {rect.Height}";
		}

		public override string ToString()
		{
			return GetFormattedString();
		}
	}
}
