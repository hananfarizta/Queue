using System;
using System.Collections.Generic;

namespace Queue.Domain.Service.Interfaces
{
    public interface IQueueService
    {
        int[] Enqueue(int[] Array);
        int Dequeue();
        int Peek();
        bool IsEmpty();
    }
}
