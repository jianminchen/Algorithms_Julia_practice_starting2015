    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace GridlandMetro
    {
        /*
         * problem statement:
         * https://www.hackerrank.com/contests/world-codesprint-7/challenges/gridland-metro
         */
        public class MyComparer : IComparer<Tuple<int, int>>
        {
            public int Compare(Tuple<int, int> x, Tuple<int, int> y)
            {
                return (x.Item1 - y.Item1);
            }
        }

        class Program
        {                    
            static void Main(string[] args)
            {
                //RunSampleTestcase(); 
                //RunSampleTestcase2();
                ProcessInput();
            }
        
            /*
             * Sample test case in the problem statement. 
             * The result should be 9 since 
             * 7 cells are taken by 3 train tracks.
             */
            private static void RunSampleTestcase()
            {          
                int k = 3;
                int rows = 4; 
                int columns = 4; 

                var map = new int[k];
                var rowsOfTrainTracks = new List<int>();
                var trainTrackColumns = new List<Tuple<int, int>>();

                var trainTracks = new int[3, 3]{
                    {2,2,3},
                    {3,1,4}, 
                    {4,4,4}
                };

                for (int i = 0; i < k; i++)
                {
                    int row = trainTracks[i, 0];
                    int startColumn = trainTracks[i, 1];
                    int endColumn   = trainTracks[i, 2];

                    rowsOfTrainTracks.Add(row);
                    trainTrackColumns.Add(new Tuple<int, int>(startColumn, endColumn));
                }

                long cellNumberTakenByTrainTracks = CalculateNumberOfCellsTakenByTrainTracks(rowsOfTrainTracks, trainTrackColumns);
                long cellsForLampposts = rows * columns - cellNumberTakenByTrainTracks;
                Debug.Assert(cellsForLampposts == 9); 
            }

            /*
             * Test case: 
             * Row No 2: 4 train tracks, 8 cells are taken by 4 train tracks.
             * 1 - 4 reserved for train track - merge two of [1,4],[2,3] 
             * 6 - 7 train track
             * 9 - 9 train track
             */
            private static void RunSampleTestcase2()
            {
                int k = 5;  // 5 train tracks

                int rows = 2;
                int columns = 10; 

                var map = new int[k];
                var rowsOfTrainTracks = new List<int>();
                var trainTrackColumns = new List<Tuple<int, int>>();

                var trainTracks = new int[5, 3]{
                    {2,8,9},
                    {2,2,3},
                    {1,1,4}, 
                    {2,1,4},
                    {2,6,7}
                };

                for (int i = 0; i < k; i++)
                {
                    int row = trainTracks[i, 0];
                    int startColumn = trainTracks[i, 1];
                    int endColumn = trainTracks[i, 2];

                    rowsOfTrainTracks.Add(row);
                    trainTrackColumns.Add(new Tuple<int, int>(startColumn, endColumn));
                }

                long cellNumberTakenByTrainTracks = CalculateNumberOfCellsTakenByTrainTracks(rowsOfTrainTracks, trainTrackColumns);
                long cellsForLampposts = rows * columns - cellNumberTakenByTrainTracks;
                Debug.Assert(cellsForLampposts == 8);
            }

            public static void ProcessInput()
            {
                string[] summaryRow = Console.ReadLine().Split(' ');
                int rowNumber = Convert.ToInt32(summaryRow[0]);
                int columnNumber = Convert.ToInt32(summaryRow[1]);
                int k = Convert.ToInt32(summaryRow[2]);

                var map = new int[k];
                var rowsOfTrainTracks = new List<int>();
                var trainTracks = new List<Tuple<int, int>>();

                for (int i = 0; i < k; i++)
                {
                    var rowData = Console.ReadLine().Split(' ');

                    int rowNo = Convert.ToInt32(rowData[0]);
                    int startColumn = Convert.ToInt32(rowData[1]);
                    int endColumn = Convert.ToInt32(rowData[2]);

                    rowsOfTrainTracks.Add(rowNo);
                    trainTracks.Add(new Tuple<int, int>(startColumn, endColumn));
                }

                long sum = rowNumber;
                sum *= columnNumber;

                Console.WriteLine(sum - CalculateNumberOfCellsTakenByTrainTracks(rowsOfTrainTracks, trainTracks));
            }

            /*
             * requirement:
             * 1. Lampposts can not be placed on a train track
             * 2. Train track is always on horizontal row, and train tracks may overlap other train tracks
             * within the same row
             * 3. Calculate the number of cells where the mayor can place lampposts
             * 
             * Design: 
             * merge intervals for each row - it is classical problem of Leetcode 56: Merge Intervals
             * 
             * @rowsOfTrainTracks - row number for each train track
             * @trainTracks - train track's start column and end column 
             * 
             * train tracks row information: rowsOfTrainTracks
             * and each train track's columns information are stored in trainTracks respectively
             * 
             */
            public static long CalculateNumberOfCellsTakenByTrainTracks(
                IList<int>             rowsOfTrainTracks,
                IList<Tuple<int, int>> trainTracks
                )
            {
                var distinctRowsOfTrainTracks = rowsOfTrainTracks.Distinct().ToArray();
                Array.Sort(distinctRowsOfTrainTracks);            

                long sum = 0;
                var trainTracksRows = rowsOfTrainTracks.ToArray();

                foreach (int row in distinctRowsOfTrainTracks)
                {
                    var numberOfTrainTracks = rowsOfTrainTracks.Count(a => a == row);                

                    int index = 0;
                    int start = 0;

                    var trainTracksOnSameRow = new List<Tuple<int, int>>();
                    while (index < numberOfTrainTracks)
                    {
                        int rowIndex = Array.IndexOf(trainTracksRows, row, start);
                        var trainTrack = trainTracks[rowIndex];
                        trainTracksOnSameRow.Add(trainTrack);

                        start = rowIndex + 1;
                        index++;
                    }

                    sum += SumCellsTakenByTrainTracksForSameRow(trainTracksOnSameRow);                
                }

                return sum;
            }
        
            /*
             * train tracks on same row
             * - merge intervals - train track stands for one interval
             * - sort train tracks by start column
             * - the array is based on start index 1 not 0
             * Add all cells taken by train tracks on the same row. 
             */
            private static long SumCellsTakenByTrainTracksForSameRow(IList<Tuple<int, int>> trainTrackOnSameRow)
            {
                var trainTracks = trainTrackOnSameRow.ToArray();
                IComparer<Tuple<int, int>> myComparer = new MyComparer();
                Array.Sort(trainTracks, myComparer);             

                var previous = trainTracks[0];
                long cellsTakensByTrainTracks = 0;
                for (int i = 1; i < trainTracks.Length; i++)
                {
                    var current = trainTracks[i];

                    // no overlap 
                    if (previous.Item2 < current.Item1)
                    {
                        cellsTakensByTrainTracks += previous.Item2 - previous.Item1 + 1;
                        previous = current;
                    }

                    // reset the previous 
                    int start = previous.Item1;
                    int end   = Math.Max(previous.Item2, current.Item2);

                    previous = new Tuple<int, int>(start, end);
                }

                // edge case: Last one
                cellsTakensByTrainTracks += previous.Item2 - previous.Item1 + 1;

                return cellsTakensByTrainTracks;
            }
        }
    }