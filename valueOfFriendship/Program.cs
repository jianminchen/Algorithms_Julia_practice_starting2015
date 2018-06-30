using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace valueOfFriendship
{
    class Program
    {        
        internal class FriendshipCircle{
            public HashSet<int> ids; 


        }

        static void Main(string[] args)
        {
            int queries = Convert.ToInt16(Console.ReadLine()); 

            Tuple<int, int>[] friendships = new Tuple<int, int>[queries]; 

            for(int i = 0; i < queries; i++)
            {
                int[] data = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

                friendships[i] = new Tuple<int,int>(data[0], data[1]);                 
            }

            Console.WriteLine(GetMaximumValueOfTotal(friendships)); 
        }

        /*
         * 
         */
        public void RunSampleTestcase()
        {

        }

        /*
         * Review requirements and specifications:
         * 
         * an unordered list of m distinct direct friendships between n students. 
         * Look for the maximum value of total among all possible orderings of formed
         * friendships 
         * 
         * Bidirectional - a is b's friend, vice versa
         * Transitive - a is b and b is c, so a is c's friend
         * 
         * students:
         * 1 <= n <= 100000
         * m - number of distinct direct friendships 
         * 1 <= m <= min(n(n-1)/2, 2 * 100000)
         */
        public static long GetMaximumValueOfTotal(Tuple<int, int>[] friendships)
        {
            IList<FriendshipCircle> frienshipCircles = new List<FriendshipCircle>();

            IList<Tuple<int, int>> bothInCircle = new List<Tuple<int, int>>(); 

            for(int i = 0; i < friendships.Length; i++)
            {
                int id1 = friendships[i].Item1;
                int id2 = friendships[i].Item2;
                
                // create a new circle
                if(NotInAnyCircle(id1, id2, frienshipCircles))
                {

                }
                else if (InTwoDifferenctCircles(id1, id2, frienshipCircles))
                {
                    // one of them in one circle, another one is in another circle - circle merge

                }
                else if (InOneCircle(id1, id2, frienshipCircles))
                {
                    // hold the friendship update until all new friendships are handled
                    // add to bothInCircel data structure
                }              
            }

            foreach(Tuple<int, int> tuple in bothInCircle)
            {
                // update the count of friendship

            }
        }
    }
}
