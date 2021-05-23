using Microsoft.Graphics.Canvas.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WizardGame.App.Classes
{
    public static class ApplicationSettings
    {
        public static CanvasTextFormat standardFormat { get; set; } = new CanvasTextFormat()
        {
            FontFamily = "ms-appx:///Fonts/Curse-Casual.ttf#Curse-Casual",
            HorizontalAlignment = CanvasHorizontalAlignment.Center,
            VerticalAlignment = CanvasVerticalAlignment.Center,
            FontSize = 32f
        };
    }
}
