using System;
using Microsoft.AspNetCore.Mvc;
using Queue.Domain.Service.Interfaces;

namespace Queue.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QueueController : ControllerBase
    {
        private readonly IQueueService queueService;

        public QueueController(IQueueService queueService)
        {
            this.queueService = queueService;
        }

        [HttpPost]
        public IActionResult Enqueue([FromBody] int[] array)
        {
            try
            {
                if (array == null || array.Length == 0)
                {
                    return BadRequest("Array cannot be empty.");
                }

                var resultArray = queueService.Enqueue(array);
                int removedItem = queueService.Dequeue();
                int frontItem = queueService.Peek();
                bool isEmpty = queueService.IsEmpty();

                // Menggabungkan elemen-elemen antrian ke dalam string
                string queueElements = string.Join(", ", resultArray);

                var responseMessage =
                    $"Removed elements: {removedItem},\n" +
                    $"Elements at the front of the queue: {frontItem},\n" +
                    $"Queue elements: {queueElements}\n" +
                    $"Empty queue: {isEmpty},\n";

                return Ok(responseMessage);
            }
            catch (ArgumentException ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

    }
}
