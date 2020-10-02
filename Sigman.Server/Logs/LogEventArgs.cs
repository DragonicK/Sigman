using System;
using System.Drawing;

namespace Sigman.Server.Logs {
    public class LogEventArgs {

        public LogEventArgs(string text, string color) {
            Text = text;
            Color = Color.FromName(color);
        }

        public string Text { get; }
        public Color Color { get; }
    }
}