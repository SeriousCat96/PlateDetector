using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

using Platedetector.Detection;

namespace Platedetector.UI
{
	public partial class AlgForm : Form
	{
		public AlgForm(AlgManager algManager)
		{
			InitializeComponent();

			AlgManager = algManager;
		}

		public AlgManager AlgManager { get; }

		private void SelectAlg()
		{
			var selectedAlg = algListBox.SelectedItem as IDetectionAlg;
			AlgManager.Select(selectedAlg.GetType());
		}

		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			MinimumSize = Size;
			BackColor   = SystemColors.Window;
			Font        = SystemFonts.MessageBoxFont;

			algListBox
				.Items
				.AddRange(AlgManager.Algorithms.ToArray());

			algListBox.SelectedIndex = AlgManager
				.Algorithms
				.IndexOf(AlgManager.SelectedAlgorithm);
		}

		private void OnButtonOKClick(object sender, EventArgs e)
		{
			SelectAlg();
		}

		private void OnListboxMouseDoubleClick(object sender, MouseEventArgs e)
		{
			SelectAlg();

			DialogResult = DialogResult.OK;
		}
	}
}
