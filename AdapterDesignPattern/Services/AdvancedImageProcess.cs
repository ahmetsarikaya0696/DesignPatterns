using LazZiya.ImageResize;
using System.Drawing;

namespace AdapterDesignPattern.Services
{
    public class AdvancedImageProcess : IAdvancedImageProcess
    {
        public void AddWatermarkImage(Stream stream, string text, string filePath, Color textColor, Color outlineColor)
        {
            using (var img = Image.FromStream(stream))
            {
                var tOps = new TextWatermarkOptions
                {
                    TextColor = textColor,

                    OutlineColor = outlineColor
                };

                img.AddTextWatermark(text, tOps)
                   .SaveAs(filePath);
            }
        }
    }
}
