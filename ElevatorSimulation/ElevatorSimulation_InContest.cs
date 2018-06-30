﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorSimulation
{
    class ElevatorSimulation
    {
        internal class Trip
        {
            public int StartTime { get; set; }
            public IList<Passenger> students { get; set;}
            public IList<Passenger> teachers { get; set;}
            
            public int Capacity { get; set;}

            public bool isComplete(Trip trip)
            {
                return trip.students.Count() == 0 && 
                       trip.teachers.Count() == 0;
            }

            /// <summary>
            /// Start from floor 0, go up to maximum floor of passengers, and
            /// then back to floor 0. 
            /// </summary>
            /// <param name="timeClock"></param>
            /// <param name="trip"></param>
            public void RunElevatorRoundTrip(
                ref TimeClock timeClock, 
                Trip trip, 
                ref int roryTime, 
                ref int lastTripFinishTime, 
                int tripIndex)
            {
                int maximumValue = 0; 
                // need to go to each floor of passengers, unload passengers
                if(trip.students.Count() > 0)
                {
                    maximumValue = trip.students.Max(s => s.Floor);
                }

                if(trip.teachers.Count() > 0)
                {
                    int value2 = trip.teachers.Max(s => s.Floor); 
                    if(value2 > maximumValue)
                    {
                        maximumValue = value2;
                    }
                }
                /*
                maximumValue = Math.Max(trip.students.Max(s => s.Floor),
                                        trip.teachers.Max(s => s.Floor));
                */
                var foundRoryPassenger = trip.students.Where(s => s.isRory).Count(); 
                if(foundRoryPassenger > 0)
                {
                    var rory = trip.students.Where(s => s.isRory);
                    foreach(Passenger p in rory)
                    {
                        roryTime = tripIndex;
                        break; 
                    }                    
                }

                var foundRoryPassenger2 = trip.teachers.Where(s => s.isRory).Count();
                if (foundRoryPassenger2 > 0)
                {
                    var rory = trip.teachers.Where(s => s.isRory);
                    foreach (Passenger p in rory)
                    {
                        roryTime = tripIndex;
                        break;
                    }
                }
                
                // need to go back to floor 0
                lastTripFinishTime = timeClock.Time + maximumValue;            
                timeClock.Time += 2 * maximumValue;                 
            }

            // start time, stop, go up, go down, end the trip
        }

        internal class Passenger
        {
            public int Id { get; set; }
            public int ArrivalTime { get; set;  }
            public int Floor { get; set; }
            public bool isRory { get; set; }

            // methods? 
        }

        internal class WaitLine
        {
            public Queue<Passenger> queue { get; set; }

            public static bool IsEmtpy(WaitLine waitLine)
            {
                return (waitLine.queue == null || waitLine.queue.Count == 0); 
            }
        }

        internal class TimeClock
        {
            public int Time { get; set; }

            public void Click()
            {
                Time++; 
            }
        }

        static void Main(string[] args)
        {
            ProcessInput(); 

            //RunSampleTestcase();
        }

        public static void RunSampleTestcase()
        {
            int numberOfPassengers = 5;
            int waitTime = 2;
            int capacity = 2;
            int roryIndexId = 4;

            var passengers = new List<Passenger>();

            string[] passengersDate = new string[] { 
                "1 2 1",
                "1 3 1",
                "2 4 1",
                "1 5 2",
                "2 6 2"
            };

            for (int i = 0; i < numberOfPassengers; i++)
            {
                var passenger = new Passenger();

                string[] tokens_id = passengersDate[i].Split(' ');

                int id = Convert.ToInt32(tokens_id[0]);
                int arrivalTime = Convert.ToInt32(tokens_id[1]);
                int floor = Convert.ToInt32(tokens_id[2]);

                passenger.Id = id;
                passenger.ArrivalTime = arrivalTime;
                passenger.Floor = floor;

                passenger.isRory = false;

                if ((i +1) == roryIndexId)
                {
                    passenger.isRory = true;
                }

                passengers.Add(passenger);
            }

            var report = SimulationElevator(passengers, waitTime, capacity);

            Console.WriteLine(report);
        }

        public static void ProcessInput()
        {
            string[] tokens_n = Console.ReadLine().Split(' ');

            int numberOfPassengers = Convert.ToInt32(tokens_n[0]);
            int waitTime = Convert.ToInt32(tokens_n[1]);
            int capacity = Convert.ToInt32(tokens_n[2]);
            int roryIndexId = Convert.ToInt32(tokens_n[3]);

            var passengers = new List<Passenger>();

            for (int i = 0; i < numberOfPassengers; i++)
            {
                var passenger = new Passenger();

                string[] tokens_id = Console.ReadLine().Split(' ');

                int id = Convert.ToInt32(tokens_id[0]);
                int arrivalTime = Convert.ToInt32(tokens_id[1]);
                int floor = Convert.ToInt32(tokens_id[2]);

                passenger.Id = id;
                passenger.ArrivalTime = arrivalTime;
                passenger.Floor = floor;

                passenger.isRory = false;

                if ((i +1) == roryIndexId)
                {
                    passenger.isRory = true;
                }

                passengers.Add(passenger);
            }

            var report = SimulationElevator(passengers, waitTime, capacity);

            Console.WriteLine(report);
        }

        public static string SimulationElevator(
            List<Passenger> passengers, 
            int waitTime, 
            int capacity)
        {
            var timeClock = new TimeClock(); 
            timeClock.Time = 0; 

            var studentsWaitLine = new WaitLine();
            var teachersWaitLine = new WaitLine();

            int roryTime = 0;
            int lastTripFinishTime = 0; 

            var trip = new Trip();

            int indexOfPassengers = 0;
            int tripIndex = 1; 
            while(true)
            {
                //studentsWaitLine.queue.Clear();
                //teachersWaitLine.queue.Clear(); 

                // need to compare Math.Max between timeClock.Time and first passenger's time
                int oneByTimeClock = timeClock.Time; 
                int endTime = 0; 
               
                for (int i = indexOfPassengers; i < passengers.Count; i++ )
                {
                    if( i == indexOfPassengers)
                    {
                        int timeByPassengerArrival = passengers[i].ArrivalTime;

                        endTime = oneByTimeClock;
                        // determine endTime
                        bool isWaitLineEmtpy = waitLineIsEmtpy(studentsWaitLine.queue, teachersWaitLine.queue);

                        if (isWaitLineEmtpy)
                        {                            
                            endTime = Math.Max(oneByTimeClock, passengers[i].ArrivalTime);
                        }
                        
                        endTime += waitTime;

                        // update timeClock time
                        if (isWaitLineEmtpy && timeByPassengerArrival > oneByTimeClock)
                        {
                            timeClock.Time = timeByPassengerArrival;
                        }
                    }

                    if (passengers[i].ArrivalTime > endTime )
                    {
                        indexOfPassengers = i; // Load for next trip
                        break; 
                    }

                    if( i == (passengers.Count -1))
                    {
                        indexOfPassengers = passengers.Count; // help to terminate the loop
                    }

                    int id = passengers[i].Id; 
                    if(id == 1)
                    {
                        if (studentsWaitLine.queue == null)
                        {
                            studentsWaitLine.queue = new Queue<Passenger>(); 
                        }

                        studentsWaitLine.queue.Enqueue(passengers[i]); 
                    }
                    else
                    {
                        if (teachersWaitLine.queue == null)
                        {
                            teachersWaitLine.queue = new Queue<Passenger>();
                        }

                        teachersWaitLine.queue.Enqueue(passengers[i]); 
                    }
                }
                                
                // cannot load any passengers, break loop
                if (
                   WaitLine.IsEmtpy(studentsWaitLine) &&
                   WaitLine.IsEmtpy(teachersWaitLine))
                {
                    break;
                }

                int count = 0;                 
                while (count < capacity)
                {
                    if(teachersWaitLine.queue.Count() > 0)
                    {
                        var passenger = teachersWaitLine.queue.Dequeue(); 
                        if(trip.teachers == null)
                        {
                            trip.teachers = new List<Passenger>();
                            trip.teachers.Add(passenger); 
                        }
                        else
                        {
                            var tripPassengers = trip.teachers;
                            tripPassengers.Add(passenger);
                            trip.teachers = tripPassengers;
                        }

                        count++; 
                        continue; 
                    }

                    if (studentsWaitLine.queue.Count() > 0)
                    {
                        var passenger = studentsWaitLine.queue.Dequeue();
                        if (trip.students == null)
                        {
                            trip.students = new List<Passenger>();
                            trip.students.Add(passenger);
                        }
                        else
                        {
                            var tripPassengers = trip.students;
                            tripPassengers.Add(passenger);
                            trip.students = tripPassengers;
                        }

                        count++;
                        continue;
                    }

                    if(teachersWaitLine.queue.Count() == 0 &&
                       studentsWaitLine.queue.Count() == 0)
                    {
                        break; 
                    }
                }

                timeClock.Time += waitTime; 

                // run the trip
                trip.RunElevatorRoundTrip(
                    ref timeClock, 
                    trip, 
                    ref roryTime, 
                    ref lastTripFinishTime,
                    tripIndex);
                tripIndex++; 
                
                // clear trip passengers 
                trip.students.Clear();
                trip.teachers.Clear();                                 
            }

            return roryTime.ToString() +" " + lastTripFinishTime.ToString();
        }

        private static bool waitLineIsEmtpy(Queue<Passenger> queue1, Queue<Passenger> queue2)
        {
            int count = 0; 
            if(queue1 != null)
            {
                count += queue1.Count; 
            }

            if(queue2 != null)
            {
                count += queue2.Count;
            }

            return count == 0; 
        }
    }
}
