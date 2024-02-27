
using System.Drawing;

namespace AdapterDesignPattern.Services
{
    public class AdvancedImageProcessAdapter : IImageProcess
    {
        private readonly IAdvancedImageProcess _advancedImageProcess;

        public AdvancedImageProcessAdapter(IAdvancedImageProcess advancedImageProcess)
        {
            _advancedImageProcess = advancedImageProcess;
        }

        public void AddWatermark(string text, string fileName, Stream imageStream)
        {
            _advancedImageProcess.AddWatermarkImage(imageStream, text, $"wwwroot/watermarks/{fileName}", Color.Red, Color.White);
        }
    }
}
