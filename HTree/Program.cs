using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTree_JuneMocking
{
    class Program
    {
        static void Main(string[] args)
        {
            DrawHTree(0, 0, 1024, 2);
        }

        public static IList<string> lines = new List<string>();

        public static void DrawHTree(double centerX, double centerY, double length, int depth)
        {
            if (depth == 0)
            {
                return;
            }

            var half = length / 2;

            // draw H tree - | first is to draw left vertical line, - second, | third is to draw right vertical line
            // drawLine(x1, y1, x2, y2) two points (x1, y1) to (x2, y2)
            drawLine(centerX - half, centerY + half, centerX - half, centerY - half); // left vertical line 
            drawLine(centerX - half, centerY, centerX + half, centerY);        // horizontal line
            drawLine(centerX + half, centerY + half, centerX + half, centerY - half); // right vertical line

            var nextLength = length / 2;
            var nextDepth = depth - 1;

            DrawHTree(centerX - half, centerY + half, nextLength, nextDepth);
            DrawHTree(centerX - half, centerY - half, nextLength, nextDepth);
            DrawHTree(centerX + half, centerY + half, nextLength, nextDepth);
            DrawHTree(centerX + half, centerY - half, nextLength, nextDepth);
        }

        private static void drawLine(double x1, double y1, double x2, double y2)
        {
            lines.Add(x1 + " " + y1 + " " + x2 + " " + y2);
        }
    }
}