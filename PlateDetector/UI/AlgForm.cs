using System;
using System.Drawing;
using System.Windows.Forms;
using PlateDetector.Detection;

namespace PlateDetector.UI
{
	public partial class AlgForm : Form
	{
		public AlgForm(AlgManager algManager)
		{
			InitializeComponent();

			AlgManager = algManager;
		}

		public AlgManager AlgManager { get; }

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			MinimumSize = Size;
			BackColor = SystemColors.Window;
			Font = SystemFonts.MessageBoxFont;

			algListBox
				.Items
				.AddRange(AlgManager.Algorithms.ToArray());
		}

		private void OnButtonOKClick(object sender, EventArgs e)
		{
			var selectedAlg = algListBox.SelectedItem as IDetectionAlg;
			AlgManager.Select(selectedAlg.GetType());
		}
	}
}
