using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _210CourseProject
{
    public class Solution
    {
        /*
         * Leetcode 210: course schedule II
         * https://leetcode.com/submissions/detail/91139534/
         */
        static void Main(string[] args)
        {
            //RunSampleTestcase();
            //RunSampleTestcase1();
            RunSampleTestcase2(); 
        }

        public static void RunSampleTestcase1()
        {
            int[,] prerequisites = new int[,] { { 1, 0 } };
            var coursesInOrder = FindOrder(2, prerequisites);

            string concatenatedCourses = string.Join(",", coursesInOrder);
            Debug.Assert(concatenatedCourses.CompareTo("0,1") == 0 
                );
        }

        /*
         * int[,] prerequisite
         * 
         * [[1,0]]
           There are a total of 2 courses to take. To take course 1 
         * you should have finished course 0. So the correct course order is [0,1]
         * 
         * 
         *   4, [[1,0],[2,0],[3,1],[3,2]]
         *   There are a total of 4 courses to take. To take course 3 
         *  you should have finished both courses 1 and 2. Both courses 
         *  1 and 2 should be taken after you finished course 0. So 
         *  there are 4 possible orderings.  
         *  [4,0,1,2,3]
         *  [4,0,2,1,3]
         *  [0,4,2,1,3]
         *  [0,4,1,2,3]
         */
        public static void RunSampleTestcase2()
        {
            int[,] prerequisites = new int[,]{{1,0},{2,0},{3,1},{3,2}}; 
            var coursesInOrder = FindOrder(5, prerequisites);

            string concatenatedCourses = string.Join(",", coursesInOrder);
            Debug.Assert(concatenatedCourses.CompareTo("4,0,1,2,3") == 0 ||
                         concatenatedCourses.CompareTo("4,0,2,1,3") == 0 ||
                         concatenatedCourses.CompareTo("0,4,2,1,3") == 0 ||
                         concatenatedCourses.CompareTo("0,4,1,2,3") == 0
                ); 
        }

        /*
         * 1. Take prerequisites first
         * 2. There may be multiple correct orders, you just need to return one of them.
         * 3. If it is impossible to finish all courses, return an empty array
         * Using topological sort, find all courses without prerequisites, put into the queue, 
         * and then dequeue one by one. 
         */
        public static int[] FindOrder(int numCourses, int[,] prerequisites)
        {
            var dependents = new Stack<int>[numCourses];

            for (int i = 0; i < numCourses; ++i)
            {
                dependents[i] = new Stack<int>();  
            }

            // build dependency list for each prerequisite course, 
            // and set up indegree variable for each courses matching with prerequisite courses' number.
            int[] indegree = new int[numCourses];
            for (int i = 0; i < prerequisites.GetLength(0); ++i)
            {
                int takeFirst = prerequisites[i, 1];
                int takeAfter = prerequisites[i, 0];

                dependents[takeFirst].Push(takeAfter); 
                ++indegree[takeAfter];             
            }

            // add those courses with 0 indegree to the queue
            Queue<int> queue = new Queue<int>();
            for (int courseId = 0; courseId < numCourses; ++courseId)
            {
                if (indegree[courseId] == 0)
                {
                    queue.Enqueue(courseId);
                }
            }

            // take courses with indgree zero only
            var coursesByTakingOrder = new int[numCourses];
            int count = 0;
            while (queue.Count > 0)
            {                   
                int readyToTake = queue.Dequeue();

                coursesByTakingOrder[count++] = readyToTake;   

                foreach (int courseId in dependents[readyToTake])
                {
                    // decrement value of indegree by 1 
                    indegree[courseId]--;

                    // add to the queue if there is no prerequisite left
                    if (indegree[courseId] == 0)   
                    {
                        queue.Enqueue(courseId);
                    }
                }
            }

            if (count != numCourses)
            {   
                return new int[]{}; 
            }

            return coursesByTakingOrder;
        }
    }
}