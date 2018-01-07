using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PlateDetector.Algorithms;

namespace PlateDetector
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();

			//var convNet = new ConvNeuralNetFactory().CreateDetectionAlgorithm();
			//convNet.Load(Path.Combine(Directory.GetCurrentDirectory(), "test_model7", "neuralnet.pb"));
			//var r = convNet.Predict(new Bitmap(@"E:\YandexDisk\testsamples\frames\Беларусь(BY)\full_images_BY\1260958590_hiop.ru_gai_03[1].jpg"));
		}
	}
}
