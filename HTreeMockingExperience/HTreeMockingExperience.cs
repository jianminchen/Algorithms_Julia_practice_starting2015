using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTreeMockingExperience
{
    ///
    ///H-tree 
    // center (1,1) , starting_length  2, depth 2 
    // |  -  |  => H 
    // drawLine((0,0), (0,2))  = vertical line, x = 0
    // drawLine (0,1), (2, 1)  - horizontal line, height is same, y = 1, center 1,1
    // drawLine (2,0), (2, 2)  - vertical line, x = 2
    //
    // H - tree 
    // H    (10,10),  starting_length 50, depth 5 
    // length = 50, length = 50/ 1.41 = 30, you have to min
    //
    class HTree
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Study the performance");
            RunTestcase();
        }

        public static void RunTestcase()
        {
            var result = DrawHTreeToDepthUsingBFS(50, 50, 50, 5);
        }

        internal class Node
        {
            public Tuple<int, int> node { get; set; }
            public int depth { get; set; }

            public Node(Tuple<int, int> node1, int depth1)
            {
                node = new Tuple<int, int>(node1.Item1, node1.Item2);
                depth = depth1;
            }
        }

        /// <summary>
        /// Time complexity: total tree - 4 * (startLength/1.4) ^ depth 
        /// </summary>
        /// <param name="centerX"></param>
        /// <param name="centerY"></param>
        /// <param name="startLength"></param>
        /// <param name="depth"></param>
        /// <returns></returns>
        public static IList<Node> DrawHTreeToDepthUsingBFS(int centerX, int centerY, int startLength, int depth)
        {
            IList<Node> centers = new List<Node>();

            if (depth <= 0)
            {
                return centers;
            }

            var tuple = new Tuple<int, int>(centerX, centerY);
            var node = new Node(tuple, 0);
            var queue = new Queue<Node>();
            queue.Enqueue(node);

            var adjustLength = startLength;

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();

                var centerX1 = current.node.Item1;
                var centerY1 = current.node.Item2;
                var currentDepth = current.depth;

                var visitNode = new Node(new Tuple<int, int>(centerX1, centerY1), currentDepth);
                centers.Add(visitNode);

                currentDepth++;

                adjustLength = (currentDepth == 1)? adjustLength : ((int) ((double)adjustLength/Math.Sqrt(2)));

                if (currentDepth > depth)
                {
                    break;
                }

                int half = adjustLength / 2;
                int x0 = centerX1 - half;
                int x1 = centerX1 + half;

                int y0 = centerY1 - half;
                int y1 = centerY1 + half;

                var leftTop = new Node(new Tuple<int, int>(x0, y0), currentDepth);
                var leftBottom = new Node(new Tuple<int, int>(x0, y1), currentDepth);
                var rightTop = new Node(new Tuple<int, int>(x1, y0), currentDepth);
                var rightBottom = new Node(new Tuple<int, int>(x1, y1), currentDepth);

                queue.Enqueue(leftTop);
                queue.Enqueue(leftBottom);
                queue.Enqueue(rightTop);
                queue.Enqueue(rightBottom);
            }

            return centers;
        }
    }
}
