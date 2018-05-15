using OpenCvSharp;

using System.Drawing;

namespace PlateDetector.Detection
{
    public interface IDetector
    {
        DetectionResult Detect(Mat image);

        DetectionResult Detect(Bitmap image);
    }
}
