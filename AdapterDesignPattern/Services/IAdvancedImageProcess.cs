using System.Drawing;

namespace AdapterDesignPattern.Services
{
    public interface IAdvancedImageProcess
    {
        void AddWatermarkImage(Stream stream, string text, string filePath, Color color, Color outlineColor);
    }
}
