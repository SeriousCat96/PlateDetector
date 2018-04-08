using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlateDetector.Logging;

namespace PlateDetector.UI
{
	public sealed class LogItem
	{
		public LogItem(ILogMessage message)
		{
			Message = message;
		}

		public ILogMessage Message { get; }
	}
}
