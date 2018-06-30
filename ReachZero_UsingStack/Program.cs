using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReachZero
{
    class Program
    {
        static void Main(string[] args)
        {
            RunTestcase();
        }

        /// <summary>
        /// arr = [3, 4, 2, 3, 0, 3, 1, 2, 1]
        /// </summary>
        public static void RunTestcase()
        {
            var numbers = new int[] { 3, 4, 2, 3, 0, 3, 1, 2, 1 };

            var route = new List<int>();

            var result = CanReachZero(numbers, 0);
        }

        /// <summary>
        /// depth first search using recursive function, 
        /// need to check if there is a cycle or not
        /// </summary>
        /// <param name="numbers"></param>
        /// <param name="startIndex"></param>
        /// <param name="memo"></param>
        /// <returns></returns>
        public static bool CanReachZero(int[] numbers, int startIndex)
        {
            if (numbers == null || numbers.Length == 0)
            {
                return false;
            }

            int length = numbers.Length;
            if (startIndex < 0 || startIndex > length - 1)
            {
                return false;
            }

            bool[] memo = new bool[length];

            var stack = new Stack<string[]>();

            stack.Push(new string[] { startIndex.ToString(), "" });
            string route = "";

            while (stack.Count > 0)
            {
                var visit = stack.Pop();
                var index = Convert.ToInt32(visit[0]);
                route = visit[1];

                var value = numbers[index];
                memo[index] = true;

                route += " " + index;

                if (value == 0)
                {
                    return true;   // base case is here!
                }

                var goRight = index + value;
                var goLeft = index - value;

                pushToStack(ref stack, goRight, memo, length, route);
                pushToStack(ref stack, goLeft, memo, length, route);
            }

            return false;   // true/ false       
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stack"></param>
        /// <param name="index"></param>
        /// <param name="memo"></param>
        /// <param name="length"></param>
        /// <param name="route"></param>
        private static void pushToStack(ref Stack<string[]> stack, int index, bool[] memo, int length, string route)
        {
            if (index < 0 || index >= length || memo[index])
            {
                return;
            }

            stack.Push(new string[] { index.ToString(), route });
        }
    }
}