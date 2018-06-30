using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
class Solution
{
    /*
     * Hackerrank ID: hannesRSA 
     * 
     * January 19, 2016
     */
    public struct Group
    {
        public int Links;
        public Stack<int> Nodes;

        /*
         * Small group will join the bigger group. 
         */
        public static void MergeTwoGroups(Group[] groups, int smallGroupId, int bigGroupId, int[] nodeGroupId)
        {
            groups[bigGroupId].Links += groups[smallGroupId].Links + 1;

            Stack<int> targetStack = groups[bigGroupId].Nodes;
            Stack<int> sourceStack = groups[smallGroupId].Nodes;

            while (sourceStack.Count > 0)
            {
                int node = sourceStack.Pop();
                nodeGroupId[node] = bigGroupId;
                targetStack.Push(node);
            }
        }
    }

    public class HeadComparer : Comparer<Group>
    {
        public override int Compare(Group x, Group y)
        {
            return (y.Nodes.Count - x.Nodes.Count);// descending
        }
    }

    /*
     * January 19, 2017
     */
    public class FriendshipValueCalculation
    {
        public static long FRIENDSHIPS_MAXIMUM = 200000;

        public static ulong[] GetLookupTable()
        {
            ulong[] friendshipsLookupTable = new ulong[FRIENDSHIPS_MAXIMUM];// 1.6mb

            ulong valueOfFriendship = 0;

            for (int i = 1; i < FRIENDSHIPS_MAXIMUM; i++)
            {
                valueOfFriendship += (ulong)i * (ulong)(i + 1);
                friendshipsLookupTable[i] = valueOfFriendship;
            }

            return friendshipsLookupTable;
        }
    }

    static void Main(String[] args)
    {
        HeadComparer headComparer = new HeadComparer();

        int queries = Convert.ToInt32(Console.ReadLine());
        for (int query = 0; query < queries; query++)
        {
            string[] tokens_n = Console.ReadLine().Split(' ');
            int studentsCount = Convert.ToInt32(tokens_n[0]);
            int friendshipsCount = Convert.ToInt32(tokens_n[1]);

            Group[] groups = new Group[studentsCount / 2 + 1];   //1-based  ~1mb+
            int[] studentGroupIDTable = new int[studentsCount + 1];         // 0.8mb / 1-based

            int groupIndex = 0;
            int groupCount = 0;

            for (int i = 0; i < friendshipsCount; i++)
            {
                string[] relationship = Console.ReadLine().Split(' ');

                int id1 = Convert.ToInt32(relationship[0]);
                int id2 = Convert.ToInt32(relationship[1]);

                int groupId1 = studentGroupIDTable[id1];
                int groupId2 = studentGroupIDTable[id2];

                // 1) neither in a group: create new group with 2 nodes
                // 2) only one in a group : add the other
                // 3) both already in same group - increase Links
                // 4) both already in different groups... join groups
                if (groupId1 == 0 || groupId2 == 0)
                {
                    if (groupId1 == 0 && groupId2 == 0)
                    {
                        groupIndex++;
                        groupCount++;

                        groups[groupIndex].Links = 1;
                        groups[groupIndex].Nodes = new Stack<int>();
                        groups[groupIndex].Nodes.Push(id1);
                        groups[groupIndex].Nodes.Push(id2);

                        studentGroupIDTable[id1] = groupIndex;
                        studentGroupIDTable[id2] = groupIndex;
                    }
                    else if (groupId1 == 0)
                    {
                        // add student1 into student2's group
                        groups[groupId2].Nodes.Push(id1);
                        groups[groupId2].Links++;

                        studentGroupIDTable[id1] = groupId2;
                    }
                    else
                    {
                        // add student2 into studnet1's group
                        groups[groupId1].Nodes.Push(id2);
                        groups[groupId1].Links++;
                        studentGroupIDTable[id2] = groupId1;
                    }
                }
                else
                {
                    if (groupId1 == groupId2)
                    {
                        groups[groupId1].Links++;
                    }
                    else   // merge two groups 
                    {
                        groupCount--;

                        if (groups[groupId1].Nodes.Count < groups[groupId2].Nodes.Count)
                        {
                            // small, big, groupId, nodeGroupId
                            Group.MergeTwoGroups(groups, groupId1, groupId2, studentGroupIDTable);
                        }
                        else
                        {
                            Group.MergeTwoGroups(groups, groupId2, groupId1, studentGroupIDTable);
                        }
                    }
                }
            }

            // Get all groups large to small
            Group[] sortedHeads = new Group[groupCount];
            int j = 0;
            for (int i = 1; i <= groupIndex; i++)
            {
                if (groups[i].Nodes.Count > 0)
                {
                    sortedHeads[j++] = groups[i];
                }
            }
            Array.Sort(sortedHeads, headComparer);

            ulong additionalLinks = 0;
            ulong total = 0;
            ulong totalFriends = 0;

            // Each group is maximized in order... additionalLinks added at end
            foreach (Group head in sortedHeads)
            {
                ulong essentialLinks = (ulong)(head.Nodes.Count - 1);

                total += FriendshipValueCalculation.GetLookupTable()[essentialLinks] + totalFriends * essentialLinks;

                additionalLinks += (ulong)head.Links - essentialLinks;

                totalFriends += essentialLinks * (essentialLinks + 1);
            }

            total += additionalLinks * totalFriends;

            Console.WriteLine(total);

        }
    }
}