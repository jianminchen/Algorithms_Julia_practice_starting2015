using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FraudulentActivityNotifications
{
    class Program
    {   
        /*
         * https://www.hackerrank.com/contests/openbracket/challenges/fraudulent-activity-notifications
         * 7:45am
         * start to read the problem statement
         * 8:14am
         * To save time, store first d days expenditures into the array, 
         * sort the array. O(dlogd) time complexity
         * To iterate, how to use existing sorted result, remove first one 
         * in the array, and then, add a new one to the sorted result. 
         * 
         * Extra space - sort array -> LinkedList -> remove first one in the array, 
         * and then add one more in the LinkedList 
         * 
         * 8:25am start to write code
         * Write first version - be careful on time complexity
         * Avoid timeout issues
         * 
         */
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split(' ');
            int n = Convert.ToInt32(input[0]);
            int d = Convert.ToInt32(input[1]);

            string[] expenditures = Console.ReadLine().Split(' ');
            Console.WriteLine(calculateNotification(n, d, expenditures)); 
        }

        /*
         * start at 8:34am
         * 
         * 8:52am
         * http://stackoverflow.com/questions/2349589/is-this-a-good-way-to-iterate-through-a-net-linkedlist-and-remove-elements
         * 
         * 9:05am 
         * exit the function
         * Ready to do static analysis 
         * 9:11am
         * first execution on HackerRank
         * 9:18am
         * score 0 out of 20
         * pass test case #0 and #6, 
         * timeout on test case #1 - #5
         */
        private static int calculateNotification(
            int n, 
            int d, 
            string[] expenditures)
        {
            // Extra space
            int[] firstDDays = new int[d];
            for (int i = 0; i < d; i++)
                firstDDays[i] = Convert.ToInt32(expenditures[i]);

            Array.Sort(firstDDays); // assuming increasing order
            LinkedList<int> runner = new LinkedList<int>(firstDDays);

            int count = 0; 
            for (int i = d; i < n; i++ )
            {               
                int[] data = runner.ToArray();
                double medium = (d % 2 == 1) ? 
                             data[d / 2] : 
                             (data[d / 2 - 1] + data[d / 2]) / 2.0;
                
                int toAdd = Convert.ToInt32(expenditures[i]);                

                if (toAdd >= 2 * medium)
                    count++; 

                int inc = i - d;
                int toRemove = Convert.ToInt32(expenditures[0 + inc]);
                runner.Remove(toRemove);

                bool found = false; 
                for(LinkedListNode<int> node = runner.First; 
                    node != runner.Last.Next; 
                    node = node.Next)
                {
                    if(toAdd <= node.Value)
                    {
                        runner.AddBefore(node, toAdd);
                        found = true; 
                        break; 
                    }
                }               

                //edge case
                if (!found)
                    runner.AddLast(toAdd); 
            }

            return count; 
        }
    }
}
