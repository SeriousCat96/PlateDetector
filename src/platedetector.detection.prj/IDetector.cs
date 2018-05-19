using OpenCvSharp;

using System.Drawing;

namespace Platedetector.Detection
{
    public interface IDetector
    {
        DetectionResult Detect(Mat image);

        DetectionResult Detect(Bitmap image);
    }
}
