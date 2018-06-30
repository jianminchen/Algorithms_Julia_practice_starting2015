using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortJobFirst_PracticeUsingIComparer
{
    /// <summary>
    /// code review: July 18, 2017
    /// shortest job first
    /// Ask to calculate the average waiting time.
    /// Study Java code:
    /// https://gist.github.com/jianminchen/73e747971833223e920696fdfbfdd4ea
    /// Study C# code using SortedSet and also defined IComparer interface
    /// 
    /// </summary>
    class Process
    {
        public int arrivalTime { get; set; }
        public int executionTime { get; set; }

        public Process(int arrTime, int exeTime)
        {
            this.arrivalTime = arrTime;
            this.executionTime = exeTime;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            int[] req = { 1, 3, 3, 6, 6, 6, 7 };
            int[] dur = { 2, 2, 3, 2, 4, 4, 2 };

            Console.WriteLine(ProcessShortJobFirst(req, dur));
        }

        /// <summary>
        /// code review on July 18, 2018
        /// Read again IComparer.Create - when to use the API
        /// </summary>
        /// <param name="requests"></param>
        /// <param name="duration"></param>
        /// <returns></returns>
        public static double ProcessShortJobFirst(int[] requests, int[] duration)
        {
            if (requests == null || requests.Length == 0)
            {
                return 0;
            }

            var queue = new SortedSet<Process>(
            Comparer<Process>.Create((a, b) => a.executionTime == b.executionTime ? a.arrivalTime - b.arrivalTime : a.executionTime - b.executionTime));

            int clockTime = 0;
            int waitingTime = 0;
            int index = 0;
            while (index < requests.Length || queue.Count > 0)
            {
                bool isEmpty = queue.Count == 0;

                if (isEmpty)
                {
                    var executionTime = requests[index];
                    var arrivalTime = duration[index];

                    queue.Add(new Process(executionTime, arrivalTime));

                    clockTime = arrivalTime + executionTime;
                    index++;
                }
                else
                {
                    var first = queue.First();
                    queue.Remove(first);

                    waitingTime += (clockTime - first.arrivalTime);
                    clockTime += first.executionTime;

                    while (index < requests.Length && requests[index] <= clockTime)
                    {
                        queue.Add(new Process(requests[index], duration[index]));
                        index++;
                    }
                }
            }

            return (waitingTime + 0.0) / requests.Length;
        }
    }
}