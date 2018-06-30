using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace minimumLoss
{
    class Node
    {
        public Int64 max;
        public Int64 min; 

        public Node(Int64 max, Int64 min)
        {
            this.max = max; 
            this.min = min; 
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            process();
            //testcase2(); 
        }

        private static void process()
        {
            int n = int.Parse(Console.ReadLine());

            Int64[] prices = new Int64[n];

            string[] arr = Console.ReadLine().Split(' ');
            prices = Array.ConvertAll(arr, Int64.Parse);

            Console.WriteLine(minimumLossCal(n, prices)); 

        }

        private static void testcase2()
        {
            Int64[] prices = new Int64[5] { 20, 7, 8, 2, 5 };

            Console.WriteLine(minimumLossCal(5, prices)); 

        }

        /*
         * minimum loss
         * finished time: 4:02pm
         * 
         */
        private static Int64 minimumLossCal(int n, Int64[] prices)
        {            
            Dictionary<Int64, Node> data = new Dictionary<Int64, Node>(); 

            // put hourse price into dictionary
            for(int i=0; i< n; i++)
            {
                Int64 runner = prices[i]; 
                if(data.ContainsKey(runner))
                {
                    Node node = data[runner];
                    Int64 max = (i > node.max) ? i : node.max;
                    Int64 min = (i < node.min) ? i : node.min;
                    data[runner] = new Node(max, min); 
                }
                else
                {
                    Node node = new Node(i, i);
                    data.Add(runner, node); 
                }
            }

            Int64[] keys = data.Keys.ToArray();
            Array.Sort(keys);

            Int64 minLoss = Int64.MaxValue;

            Node prev = data[keys[0]]; 
            Int64 pricePrev = keys[0]; 

            for(int i= 1; i < keys.Length; i++)
            {
                Node cur = data[keys[i]];
                Int64 priceCur = keys[i];

                Int64 newV = Math.Abs(pricePrev - priceCur);
                if (newV > minLoss)
                {
                    prev = cur;
                    pricePrev = priceCur; 
                    continue;
                }

                if((priceCur > pricePrev &&
                   (cur.min + 1) < prev.max) ||
                  (pricePrev > priceCur &&
                   (prev.min +1) < cur.max))
                {
                    minLoss = newV;                    
                }

                prev = cur;
                pricePrev = priceCur;    
            }

            return minLoss; 
        }        
    }
}
