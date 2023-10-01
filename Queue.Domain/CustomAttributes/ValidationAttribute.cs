using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Queue.Domain.CustomAttributes
{
    public class CalculateQueueAttribute : ValidationAttribute
    {
        public CalculateQueueAttribute()
        {
            ErrorMessage = "Array is not correct for Queue.";
        }

        public override bool IsValid(object value)
        {
            if (value is int[] Array)
            {
                return IsQueueValid(Array);
            }

            return false;
        }

        private static bool IsQueueValid(int[] Array)
        {
            if (Array.Length < 2)
            {
                return false;
            }

            int front = Array[0];
            int rear = Array[Array.Length - 1];

            if (Array.All(x => x == front) || Array.All(x => x == rear))
            {
                return false;
            }

            return true;
        }
    }
}
