using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlateDetector.Controllers
{
	public partial class EvalForm : Form
	{
		public EvalForm()
		{
			InitializeComponent();
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			MinimumSize = Size;
			Location = new System.Drawing.Point(200, 0);
			Font = SystemFonts.MessageBoxFont;
			//BackColor	= SystemColors.Window;
		}
	}
}
