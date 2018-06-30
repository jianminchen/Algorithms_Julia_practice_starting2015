using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrickWall
{
    /// <summary>
    /// problem statement:
    /// https://leetcode.com/contest/leetcode-weekly-contest-27/problems/brick-wall/
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            IList<IList<int>> walls = new List<IList<int>>();
            walls.Add(new List<int>() { 1, 2, 2, 1 });
            walls.Add(new List<int>() { 3, 1, 2 });
            walls.Add(new List<int>() { 1, 3, 2 });
            walls.Add(new List<int>() { 2, 4 });
            walls.Add(new List<int>() { 3, 1, 2 });
            walls.Add(new List<int>() { 1, 3, 1,1 });

            var result = LeastBricks(walls); 
        }

        /// <summary>
        /// line sweep algorithm, try it 7:02pm - 7:30pm
        /// </summary>
        /// <param name="wall"></param>
        /// <returns></returns>
        public static int LeastBricks(IList<IList<int>> wall)
        {
            if (wall == null || wall.Count == 0) return 0;

            int length = wall.Count;            

            int[] start = new int[length];

            int count = 0;
            var tracking = new List<int>(); 

            return leastBricksLineSweep(wall, start, ref count, tracking);            
        }

        private static int leastBricksLineSweep(IList<IList<int>> wall, int[] start, ref int count, List<int> tracking)
        {
            int minValue = Int32.MaxValue;
            int length = wall.Count;

            var selected = new HashSet<int>();
            for (int i = 0; i < length; i++)
            {
                int currentIndex = start[i];

                if (currentIndex >= wall[i].Count)
                {
                    break; 
                }

                int current = wall[i][start[i]];

                if (minValue > current)
                {
                    selected.Clear();
                    selected.Add(i);

                    minValue = current;
                }
                else if (minValue == current)
                {
                    selected.Add(i);
                }
            }

            int currentCrossed = length - selected.Count;
            foreach (var id in selected)
            {
                start[id]++; 
            }

            // adjust the value 
            for (int i = 0; i < length; i++ )
            {
                if(!selected.Contains(i))
                {
                    int index = start[i];
                    wall[i][index] -= minValue; 
                }
            }

            if (isAllTerminated(wall, start)) return length; 

            count++;
            tracking.Add(currentCrossed); 
            return Math.Min(currentCrossed, leastBricksLineSweep(wall, start, ref count, tracking));
        }

        private static bool isAllTerminated(IList<IList<int>> wall, int[] start)
        {
            for(int i = 0; i < wall.Count; i++)
            {
                if (wall[i].Count != start[i]) return false; 
            }

            return true; 
        }
    }
}
