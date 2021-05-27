using Microsoft.Graphics.Canvas.Text;

namespace WizardGame.App.GameFiles
{
    public static class ApplicationSettings
    {
        public static CanvasTextFormat standardFormat { get; set; } = new CanvasTextFormat()
        {
            HorizontalAlignment = CanvasHorizontalAlignment.Center,
            VerticalAlignment = CanvasVerticalAlignment.Center,
            FontSize = 32f
        };
    }
}
