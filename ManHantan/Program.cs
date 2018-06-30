using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhattan2
{
    /// <summary>
    /// https://www.hackerrank.com/contests/booking-womenintech/challenges/manhattan-2
    /// The idea is to start from top left node, always go right or down, and then track the 
    /// sum and max value; 
    /// Because the size of matrix is 100 * 100, queue will cause out-of-memory, use stack, DFS 
    /// search, try to use recursive solution if possible
    /// 200 depth at most, 100 + 100
    /// try to get some points first, and then get the idea - 2:35pm - 7pm 
    /// 7:53pm what is the possible reason to get wrong answer? 
    /// </summary>
    class Manhattan2
    {
        internal class Node
        {
            public long Sum { get; set; }
            public int MaxValue { get; set; }

            public long candies
            {
                get
                {
                    return Sum + SecondsLeft * MaxValue;
                }
            }

            public int SecondsLeft { get; set; }

            public int startRow { get; set; }
            public int startCol { get; set; }
            public int maximumCandies { get; set; }
            public int seconds { get; set; }
            public bool exraSecondsExisting { get; set; }

            // debug the code 
            public string trackNode { get; set; }
        }

        static void Main(string[] args)
        {
            ProcessInput();
            //RunTestcase(); 
        }

        public static void RunTestcase()
        {
            int rows = 5;
            int cols = 100;
            int seconds = 444;

            var matrix = new int[rows][];
            string firstRow = "807 249 73 658 930 272 544 878 923 709 440 165 492 42 987 503 327 729 840 612 303 169 709 157 560 933 99 278 816 335 97 826 512 267 810 633 979 149 579 821 967 672 393 336 485 745 228 91 194 357 1 153 708 944 668 490 124 196 530 903 722 666 549 24 801 853 977 408 228 933 298 981 635 13 865 814 63 536 425 669 115 94 629 501 517 195 105 404 451 298 188 123 505 882 752 566 716 337 438 144";
            string secondRow = "501 897 871 828 137 358 177 397 294 904 609 231 745 175 635 298 142 399 968 412 260 557 594 8 395 968 113 530 6 962 942 365 82 852 767 821 695 712 671 901 590 831 738 57 616 790 640 679 335 6 972 98 95 319 454 223 289 760 905 126 123 506 813 770 238 94 220 844 366 534 226 394 363 738 844 590 550 159 623 947 385 217 272 539 247 385 496 885 623 420 144 968 735 915 625 534 42 11 679 152";
            string thirdRow = "244 295 818 396 692 815 991 33 669 397 553 547 825 210 662 211 808 377 761 625 335 868 995 776 767 439 874 331 556 301 872 560 94 984 755 789 407 15 193 769 680 455 855 506 963 502 676 108 249 331 844 638 808 997 651 849 203 731 531 14 419 775 9 180 929 223 54 260 737 545 317 525 200 256 564 597 648 704 550 150 976 412 554 797 504 381 748 65 378 699 209 129 553 483 447 607 773 322 305 176";
            string fourthRow = "53 224 630 366 400 444 370 285 16 898 155 133 557 576 178 266 357 711 878 614 819 737 133 591 720 762 633 197 31 588 589 873 877 304 358 200 254 960 915 947 480 730 955 546 107 ";
            string fifthRow = "549 542 249 771 310 879 983 970 40 723 650 971 229 318 746 299 230 621 776 124 244 958 696 771 64 560 598 751 940 503 551 801 205 80 418 274 649 413 320 25 12 783 788 117 8 550 324 195 257 511 690 666 410 593 553 565 960 742 403 352 307 141 910 200 799 127 171 787 414 625 641 517 348 842 315 974 445 373 220 911 239 54 305 929 253 189 166 356 304 860 898 592 720 116 580 867 352 939 698 901";
            matrix[0] = new int[] { 2, 1, 1, 1, 1 };
            matrix[1] = new int[] { 2, 2, 1, 1, 1 };
            matrix[2] = new int[] { 1, 2, 1, 1, 1 };
            matrix[3] = new int[] { 2, 2, 1, 1, 3 };
            matrix[4] = new int[] { 2, 2, 2, 2, 2 };

            var node = new Node();
            node.maximumCandies = int.MinValue;
            node.startCol = 0;
            node.startRow = 0;
            node.seconds = seconds;
            node.exraSecondsExisting = seconds > rows - 1 + cols - 1;

            var maximumCandies = FindRouteFromLeftTopToBottomRight(matrix, node);

            Console.WriteLine(maximumCandies);
        }


        public static void ProcessInput()
        {
            var numbers = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int rows = numbers[0];
            int cols = numbers[1];
            int seconds = numbers[2];

            var matrix = new int[rows][];
            for (int i = 0; i < rows; i++)
            {
                matrix[i] = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            }

            if (seconds < rows - 1 + cols - 1)
            {
                Console.WriteLine("Too late");
                return;
            }

            var node = new Node();
            node.maximumCandies = int.MinValue;
            node.startCol = 0;
            node.startRow = 0;
            node.seconds = seconds;
            node.exraSecondsExisting = seconds > rows - 1 + cols - 1;

            var maximumCandies = FindRouteFromLeftTopToBottomRight(matrix, node);

            Console.WriteLine(maximumCandies);
        }

        /// <summary>
        /// Always go two directions
        /// down or right 
        /// recursive may run to error "stack overflow", using stack instead. 
        /// </summary>
        /// <param name="matrix"></param>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static long FindRouteFromLeftTopToBottomRight(
            int[][] matrix,
            Node node)
        {
            int rows = matrix.Length;
            int cols = matrix[0].Length;

            Stack<Node> stack = new Stack<Node>();
            stack.Push(node);

            long maximumCandies = long.MinValue;

            // memorizatio to save the time
            var lookupExisting = new Dictionary<string, HashSet<Node>>();
            bool extraSecondExisting = node.exraSecondsExisting;

            while (stack.Count > 0)
            {
                Node visit = stack.Pop();

                int startRow = visit.startRow;
                int startCol = visit.startCol;
                int seconds = visit.seconds;

                // duplicate work - skip if need 
                if (lookupAndCheckDuplicated(visit, rows, cols, lookupExisting, extraSecondExisting))
                {
                    continue;
                }

                // Reach the last node 
                bool lastNodeReached = isLastNode(startRow, startCol, rows, cols);
                if (lastNodeReached)
                {
                    updateMaximumCandies(matrix, visit, ref maximumCandies);
                    continue;
                }

                if (seconds <= 0)
                {
                    continue;
                }

                // go to right
                if (startCol < cols - 1)
                {
                    visitRightOrDownNeighbors(matrix, visit, stack, 0, 1);
                }

                // go down
                if (startRow < rows - 1)
                {
                    visitRightOrDownNeighbors(matrix, visit, stack, 1, 0);
                }
            }

            return maximumCandies;
        }

        /// <summary>
        /// go right or go down 
        /// </summary>
        /// <param name="matrix"></param>       
        /// <param name="visit"></param>        
        /// <param name="stack"></param>
        /// <param name="incrementX"></param>
        /// <param name="incrementY"></param>
        private static void visitRightOrDownNeighbors(int[][] matrix, Node visit, Stack<Node> stack, int incrementX, int incrementY)
        {
            int startRow = visit.startRow;
            int startCol = visit.startCol;
            int seconds = visit.seconds;

            var current = matrix[startRow][startCol];
            var currentMax = visit.MaxValue;

            var nextNode = new Node();

            nextNode.MaxValue = (current > currentMax) ? current : currentMax;
            nextNode.seconds = seconds - 1;
            nextNode.startRow = startRow + incrementX;
            nextNode.startCol = startCol + incrementY;
            nextNode.Sum = visit.Sum + current;

            stack.Push(nextNode);

            // debug code
            //var currentKey = encode(startRow, startCol);
            //var trackMessage = getTrackMessage(currentKey);
            //nextNode.trackNode = (visit.trackNode == null) ? trackMessage : visit.trackNode + trackMessage;
        }

        /// <summary>
        /// Make the code clear to test 
        /// </summary>
        /// <param name="matrix"></param>       
        /// <param name="visit"></param>        
        /// <param name="maximumCandies"></param>
        private static void updateMaximumCandies(int[][] matrix, Node visit, ref long maximumCandies)
        {
            int startRow = visit.startRow;
            int startCol = visit.startCol;
            int seconds = visit.seconds;

            var current = matrix[startRow][startCol];
            var currentMax = visit.MaxValue;

            long sum = visit.Sum + current;
            long secondsLeft = seconds;
            int maxValue = (current > currentMax) ? current : currentMax;

            // calculate candies - if there are extra time, maxValue will be collected multiple times - secondsLeft
            long currentCandies = sum + secondsLeft * maxValue;

            maximumCandies = (currentCandies > maximumCandies) ? currentCandies : maximumCandies;
        }

        /// <summary>
        /// too complicate! need to make it simple
        /// the logic looks too complicated. 
        /// </summary>
        /// <param name="visit"></param>
        /// <param name="startRow"></param>
        /// <param name="startCol"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <param name="lookupExisting"></param>
        /// <param name="extraSecondExisting"></param>
        /// <returns></returns>
        private static bool lookupAndCheckDuplicated(
            Node visit,
            int rows,
            int cols,
            Dictionary<string, HashSet<Node>> lookupExisting,
            bool extraSecondExisting)
        {
            int startRow = visit.startRow;
            int startCol = visit.startCol;

            var currentKey = encode(startRow, startCol);

            if (isLastNode(startRow, startCol, rows, cols))
            {
                return false;
            }

            if (lookupExisting.ContainsKey(currentKey))
            {
                // compare to existing one, if it is not better then stop                
                bool skipCurrent = skipChecking(lookupExisting, currentKey, visit, extraSecondExisting);
                if (skipCurrent)
                {
                    return true;
                }
                else if (replaceCurrent(lookupExisting, currentKey, visit, extraSecondExisting))
                {
                    var set = new HashSet<Node>();
                    set.Add(visit);

                    lookupExisting[currentKey] = set;
                }
                else
                {
                    var existing = lookupExisting[currentKey];
                    existing.Add(visit);
                    lookupExisting[currentKey] = existing;
                }
            }
            else
            {
                var set = new HashSet<Node>();
                set.Add(visit);

                lookupExisting.Add(currentKey, set);
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="startCol"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        private static bool isNotLastNode(int startRow, int startCol, int rows, int cols)
        {
            return startRow != (rows - 1) || startCol != (cols - 1);
        }

        private static string getTrackMessage(string key)
        {
            return "[" + key + "]";
        }

        /// <summary>
        /// Need to figure out the way to save time 
        /// </summary>
        /// <param name="lookupExisting"></param>
        /// <param name="currentKey"></param>
        /// <param name="visit"></param>
        /// <returns></returns>
        private static bool skipChecking(Dictionary<string, HashSet<Node>> lookupExisting, string currentKey, Node visit, bool extraSecondsExisting)
        {
            var found = lookupExisting[currentKey];

            foreach (var item in found)
            {
                if (extraSecondsExisting && (item.Sum >= visit.Sum && item.MaxValue >= visit.MaxValue))
                {
                    return true;
                }

                if (!extraSecondsExisting && item.Sum >= visit.Sum)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lookupExisting"></param>
        /// <param name="currentKey"></param>
        /// <param name="visit"></param>
        /// <param name="extraSecondsExisting"></param>
        /// <returns></returns>
        private static bool replaceCurrent(Dictionary<string, HashSet<Node>> lookupExisting, string currentKey, Node visit, bool extraSecondsExisting)
        {
            var found = lookupExisting[currentKey];

            foreach (var item in found)
            {
                if (extraSecondsExisting)
                {
                    if (visit.Sum > item.Sum && visit.MaxValue > item.MaxValue)
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }

                if (!extraSecondsExisting)
                {
                    if (visit.Sum > item.Sum)
                    {
                        continue;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public static string encode(int row, int col)
        {
            return row + "-" + col;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startRow"></param>
        /// <param name="startCol"></param>
        /// <param name="rows"></param>
        /// <param name="cols"></param>
        /// <returns></returns>
        private static bool isLastNode(int startRow, int startCol, int rows, int cols)
        {
            return startRow == (rows - 1) && startCol == (cols - 1);
        }
    }
}
