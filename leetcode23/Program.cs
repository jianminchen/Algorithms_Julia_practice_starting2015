using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Leetcode23_MergeKSortedLists
{
    public class NodeIndex
    {
        public ListNode Node { get; set; }
        public int Index { get; set; }

        public NodeIndex(ListNode node, int index)
        {
            Node = node;
            Index = index;
        }
    }

    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int x)
        {
            val = x;
        }
    }

    /// <summary>
    /// Leetcode 23 - Merge K sorted lists - hard level algorithm
    /// https://leetcode.com/problems/merge-k-sorted-lists/?tab=Solutions#/description
    /// 
    /// Code review on June 26, 2017
    /// Learn how to write SortedSet 
    /// Need to review code later
    /// 
    /// </summary>    
    class Leetcode23_MergeKSortedLists
    {
        static void Main(string[] args)
        {
        }

        /// <summary>
        /// code review on JUne 29, 2017
        /// </summary>
        /// <param name="lists"></param>
        /// <returns></returns>
        public ListNode MergeKLists(ListNode[] lists)
        {
            var sortedSet = new SortedSet<NodeIndex>(
            Comparer<NodeIndex>.Create((a, b) => a.Node.val == b.Node.val ? a.Index - b.Index : a.Node.val - b.Node.val));

            var head = new ListNode(int.MinValue);
            var p = head;

            for (int i = 0; i < lists.Length; i++)
            {
                if (lists[i] != null)
                {
                    sortedSet.Add(new NodeIndex(lists[i], i));
                }
            }

            while (sortedSet.Count != 0)
            {
                var nextMerge = sortedSet.Min;

                p.next = nextMerge.Node;
                p = p.next;

                sortedSet.Remove(nextMerge);

                if ((nextMerge.Node = nextMerge.Node.next) != null)
                {
                    sortedSet.Add(nextMerge);
                }
            }

            return head.next;
        }
    }
}