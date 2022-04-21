using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrowaveTools.Settings
{
    // Chart and data display settings
    public enum DataType
    {
        re_im,
        mag_ang,
        magDB_ang,
    }

    public static class Settings
    {
        // Color settings
        public static Color GridColor = Color.DarkGray;
        public static bool lightMode = false;
        public static Pen lightPen = new Pen(Color.White);
        public static Pen darkPen = new Pen(Color.Black);
        public static Pen drawPen = lightPen;

        // Substrate settings

        // Frequency settings
        public static float fStart = 1.0f;
        public static float fStep = 0.1f;
        public static float fStop = 10.0f;

        public static DataType dt = DataType.mag_ang;

        // Static constructor
        static Settings()
        {
            if (lightMode)
                drawPen = darkPen;
            else
                drawPen = lightPen;
        }
    }
}
