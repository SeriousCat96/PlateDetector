using OpenCvSharp;
using OpenCvSharp.UserInterface;

using PlateDetector.Detection.Utils;

using System.Collections.Generic;

namespace PlateDetector.Markup
{
    public class RegionSelectionController
	{
		#region .ctor
		/// <summary> Создает <see cref="RegionSelectionController"/>.</summary>
		/// <param name="image"> Обрабатываемое изображение. </param>
		public RegionSelectionController(PictureBoxIpl picBox)
		{
            PicBox = picBox;
			Image  = picBox.ImageIpl?.Clone();
		}
		#endregion

		#region Properties
		/// <summary> Обрабатываемое изображение. </summary>
		public Mat Image { get; set; }
		/// <summary> Цвет выделяемых областей. </summary>
		public Scalar Color => Scalar.Aqua;

        public PictureBoxIpl PicBox { get; }
		
		#endregion

		#region Methods
		/// <summary> Выделяет размеченные области на изображении. </summary>
		/// <param name="regions"> Размеченные области. </param>
		public void SelectRegions(IEnumerable<Rect> regions)
		{
			foreach(var region in regions)
			{
				Image.AddRectangle(region, Color, new Size(PicBox.Width, PicBox.Height));
			}
		}
		#endregion
	}
}
