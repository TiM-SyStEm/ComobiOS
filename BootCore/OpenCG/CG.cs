using System;
using System.Drawing;
using Sys = Cosmos.System;
using Cosmos.System.Graphics;
using Cosmos.System.Graphics.Fonts;

namespace ComobiOS.BootCore.OpenCG
{
    class CG
    {
        //ComobiGraphics 1
        public static string version = "1.0";
        public static int screenX = 800;
        public static int screenY = 600;
        private static Canvas canvas;
        public static ColorDepth ColorDepth = ColorDepth.ColorDepth8;

        public static void Init()
        {
            canvas = FullScreenCanvas.GetFullScreenCanvas();
            canvas.Mode = new Mode(screenX, screenY, ColorDepth.ColorDepth32);
        }
        public static void Clear(Color c)
        {
            canvas.Clear(c);
        }
        public class Simple{
            public static void drawPoint(int x, int y, Color c)
            {
                /*
                  x, y
                   *
                */
                Pen pen = new Pen(c);
                canvas.DrawPoint(pen, x, y);
            }
            public static void drawline(int x1, int y1, int x2, int y2, Color c)
            {
                /*
                   x1, y1      x2, y2
                   *              *
                   ----------------
                */
                Pen pen = new Pen(c);
                canvas.DrawLine(pen, x1, y1, x2, y2);
            }
            public static void drawRect(int x, int y, int width, int height, Color c)
            {
                /*
                   x1, y1
                   O******************O
                   *                  *
                   *                  *  height
                   *                  *
                   O******************O
                          width
                */
                Pen pen = new Pen(c);
                canvas.DrawRectangle(pen, x, y, width, height);
            }
            public static void drawFilledRect(int x, int y, int width, int height, Color c)
            {
                /*
                   x1, y1
                   O******************O
                   ********************
                   ******************** height
                   ********************
                   O******************O
                          width
                */
                Pen pen = new Pen(c);
                canvas.DrawFilledRectangle(pen, new Sys.Graphics.Point(x, y), width, height);
            }
            public static void drawCircle(int x_center, int y_center, int radius, Color c)
            {
                /*
                 
                    ******
                   *      *
                  *    O   *  radius
                  *        * 
                   *      *
                    ******
                 O - x_center, y_center

                */
                Pen pen = new Pen(c);
                canvas.DrawCircle(pen, x_center, y_center, radius);
            }
            public static void drawFilledCircle(int x_center, int y_center, int radius, Color c)
            {
                /*
                 
                    ******
                   ********
                  *****O****  radius
                  ********** 
                   ********
                    ******
                 O - x_center, y_center

                */
                Pen pen = new Pen(c);
                canvas.DrawFilledCircle(pen, x_center, y_center, radius);
            }
            public static void drawString(string str, int x, int y, Color c)
            {
                /*
                 
                  TEXT
                 
                */
                Pen pen = new Pen(c);
                canvas.DrawString(str, PCScreenFont.Default, pen, new Sys.Graphics.Point(x, y));
            }
        }
    }
}
