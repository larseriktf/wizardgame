using Microsoft.Graphics.Canvas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using WizardGame.App.Classes.Entities;
using WizardGame.App.Interfaces;
using static System.Math;

namespace WizardGame.App.Classes
{
    public static class CanvasDebugger
    {
        private static readonly int xIndent = 25;
        private static readonly int yIndent = 25;
        public static Dictionary<object, string> DebugMessages { get; set; } = new Dictionary<object, string>();

        public static void DrawMessages(CanvasDrawingSession ds)
        {
            if (DebugMessages.Count() > 0)
            {
                ds.DrawText("Canvas Debugger", xIndent, yIndent, Colors.White);
                ds.DrawText("---------------", xIndent, yIndent * 2, Colors.White);

                int i = 0;
                foreach (KeyValuePair<object, string> message in DebugMessages)
                {
                    ds.DrawText(message.Value, xIndent, yIndent * (i + 3), Colors.White);
                    i++;
                }
            }
        }

        public static void Debug(object obj, string message)
        {
            if (!DebugMessages.ContainsKey(obj))
            {
                DebugMessages.Add(obj, message);
            }
            else
            {
                DebugMessages[obj] = message;
            }
        }

        public static void TestDrawing(CanvasDrawingSession ds)
        {


        }
    }
}
