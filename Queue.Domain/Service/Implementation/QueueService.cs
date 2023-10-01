using System;
using System.Collections.Generic;
using Queue.Domain.Service.Interfaces;

namespace Queue.Domain.Service.Implementation
{
    public class QueueService : IQueueService
    {
        Queue<int> myQueue = new Queue<int>();
        public int[] Enqueue(int[] Array)
        {
            if (Array == null || Array.Length == 0)
            {
                throw new ArgumentException("Array cannot be empty");
            }
            

            foreach (int element in Array)
            {
                myQueue.Enqueue(element);
            }

            int[] resultArray = myQueue.ToArray();

            return resultArray;
        }


        public int Dequeue()
        {
            if (myQueue.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            return myQueue.Dequeue();
        }

        public int Peek()
        {
            if (myQueue.Count == 0)
            {
                throw new InvalidOperationException("Queue is empty");
            }

            return myQueue.Peek();
        }

        public bool IsEmpty()
        {
            return myQueue.Count == 0;
        }
    }
}
