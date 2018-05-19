using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Platedetector.Utils.Logging
{
    public sealed partial class LogView : UserControl
    {
        #region Data

        private readonly Brush _reportsForegroundBrushSelected;
        private readonly Brush _reportsBackgroundBrushSelected;
        private readonly Brush _reportsBackgroundBrush1;
        private readonly Brush _reportsBackgroundBrush2;

        #endregion

        public LogView()
        {
            InitializeComponent();

            _reportsForegroundBrushSelected = new SolidBrush(Color.White);
            _reportsBackgroundBrushSelected = new SolidBrush(Color.FromKnownColor(KnownColor.Highlight));
            _reportsBackgroundBrush1 = new SolidBrush(Color.LightGray);
            _reportsBackgroundBrush2 = new SolidBrush(Color.WhiteSmoke);

            Font = SystemFonts.MessageBoxFont;

            logListBox.DrawMode = DrawMode.OwnerDrawFixed;
            logListBox.DrawItem += OnDrawItem;
        }

        #region Properties

        [Category("Logging")]
        [DefaultValue(200)]
        [Description("Ограничение на максимальное количество отображаемых событий в контроле.")]
        public int RecordLimit { get; set; }

        [Browsable(false)]
        public ListBox.ObjectCollection Items
        {
            get
            {
                return logListBox.Items;
            }
        }

        [Category("Appearance")]
        [DefaultValue(18)]
        [Description("Высота элемента в списке.")]
        public int ItemHeight
        {
            get
            {
                return logListBox.ItemHeight;
            }
            set
            {
                logListBox.ItemHeight = value;
            }
        }

        [Browsable(false)]
        public int TopIndex
        {
            set
            {
                logListBox.TopIndex = value;
            }
        }
        #endregion

        #region EventHandlers

        private void OnDrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            bool selected = ((e.State & DrawItemState.Selected) == DrawItemState.Selected);

            int index = e.Index;
            if (index >= 0 && index < logListBox.Items.Count)
            {
                Graphics g = e.Graphics;

                // background:
                Brush backgroundBrush;

                if (selected)
                {
                    backgroundBrush = _reportsBackgroundBrushSelected;
                }
                else if ((index % 2) == 0)
                {
                    backgroundBrush = _reportsBackgroundBrush1;
                }
                else
                {
                    backgroundBrush = _reportsBackgroundBrush2;
                }

                g.FillRectangle(backgroundBrush, e.Bounds);

                // Get the current item and cast it to LogItem
                if (logListBox.Items[e.Index] is LogItem item)
                {
                    // text:
                    var foregroundBrush = (selected) ?
                        _reportsForegroundBrushSelected : new SolidBrush(item.Message.Color);

                    g.DrawString(
                        item.Message.FormattedMessage,
                        e.Font,
                        foregroundBrush,
                        logListBox.GetItemRectangle(index).Location);
                }
            }

            e.DrawFocusRectangle();
        }

        #endregion
    }
}
