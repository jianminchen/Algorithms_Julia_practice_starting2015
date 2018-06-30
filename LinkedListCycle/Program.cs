using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedListCycle
{
    class ListNode {
        public int val;
        public ListNode next;
        public ListNode(int x) { 
            val = x; 
            next = null; 
        }
  }
   
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
        }

        public ListNode detectCycle(ListNode a)
        {
            if (a == null)
                return null;

            ListNode normalRunner = a;
            ListNode doubleRunner = a;

            while (normalRunner != null
            && doubleRunner != null
            && normalRunner != doubleRunner)
            {
                normalRunner = normalRunner.next;
                if (doubleRunner.next == null)
                    return null;
                else
                    doubleRunner = doubleRunner.next.next;
            }

            //if(normalRunner == null)  // bug001 
            if (normalRunner == null || doubleRunner == null)
                return null;

            ListNode thirdRunner = a;

            while (thirdRunner != null
            && normalRunner != null
            && thirdRunner != normalRunner)
            {
                thirdRunner = thirdRunner.next;
                normalRunner = normalRunner.next;
            }

            return normalRunner;
        }

    }
}
