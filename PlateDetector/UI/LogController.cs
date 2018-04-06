using System.Windows.Forms;

namespace PlateDetector.UI
{
	/// <summary> Предоставляет методы, обрабатывающие элемент управления <see cref="ListBox"/> для лога. </summary>
	public class LogController
	{
		/// <summary> Создаёт <see cref="LogDispatcher"/>. </summary>
		/// <param name="listBox"> <seealso cref="ListBox"/>, в котором отображается лог. </param>
		public LogController(ListBox listBox)
		{
			LogListBox = listBox;
		}

		/// <summary> <seealso cref="ListBox"/>, в котором отображается лог. </summary>
		public ListBox LogListBox { get; set; }

		public int RecordLimit { get; set; } = 1000;
	}
}
