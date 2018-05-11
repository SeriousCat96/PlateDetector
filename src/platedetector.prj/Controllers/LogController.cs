using PlateDetector.Logging;

using System;
using System.Drawing;
using System.Windows.Forms;

namespace PlateDetector.Controllers
{
	/// <summary> Предоставляет методы, обрабатывающие элемент управления <see cref="ListBox"/> для лога. </summary>
	public class LogController : IDisposable
	{
		#region Data
		private SolidBrush _reportsForegroundBrushSelected;
		private SolidBrush _reportsBackgroundBrushSelected;
		private SolidBrush _reportsBackgroundBrush1;
		private SolidBrush _reportsBackgroundBrush2; 

		#endregion

		#region .ctor
		/// <summary> Создаёт <see cref="LogDispatcher"/>. </summary>
		/// <param name="listBox"> <seealso cref="ListBox"/>, в котором отображается лог. </param>
		public LogController(ListBox listBox)
		{
			_reportsForegroundBrushSelected = new SolidBrush(Color.White);
			_reportsBackgroundBrushSelected = new SolidBrush(Color.FromKnownColor(KnownColor.Highlight));
			_reportsBackgroundBrush1		= new SolidBrush(Color.LightGray);
			_reportsBackgroundBrush2		= new SolidBrush(Color.WhiteSmoke);


			LogListBox = listBox;
			LogListBox.DrawMode = DrawMode.OwnerDrawFixed;
			LogListBox.DrawItem += OnDrawItem;
		} 

		#endregion

		#region Properties
		/// <summary> <seealso cref="ListBox"/>, в котором отображается лог. </summary>
		public ListBox LogListBox { get; set; }

		public int RecordLimit { get; set; } = 200;
		
		#endregion

		#region Methods
		public void Dispose()
		{
			LogListBox.DrawItem -= OnDrawItem;
			LogListBox?.Dispose();
		} 

		#endregion

		#region EventHandlers
		private void OnDrawItem(object sender, DrawItemEventArgs e)
		{
			e.DrawBackground();
			bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

			int index = e.Index;
			if(index >= 0 && index < LogListBox.Items.Count)
			{
				Graphics g = e.Graphics;

				// background:
				SolidBrush backgroundBrush;

				if(selected)
				{
					backgroundBrush = _reportsBackgroundBrushSelected;
				}
				else if((index % 2) == 0)
				{
					backgroundBrush = _reportsBackgroundBrush1;
				}
				else
				{
					backgroundBrush = _reportsBackgroundBrush2;
				}

				g.FillRectangle(backgroundBrush, e.Bounds);

				var item = LogListBox.Items[e.Index] as LogItem; // Get the current item and cast it to MyListBoxItem
				if(item != null)
				{
					// text:
					SolidBrush foregroundBrush = (selected) ?
						_reportsForegroundBrushSelected : new SolidBrush(item.Message.Color);

					g.DrawString(
						item.Message.FormattedMessage,
						e.Font,
						foregroundBrush,
						LogListBox.GetItemRectangle(index).Location);
				}
			}

			e.DrawFocusRectangle();
		} 

		#endregion

	}
}
