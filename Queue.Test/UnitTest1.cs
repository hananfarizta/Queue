using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Queue.Api.Controllers;
using Queue.Domain.Service.Interfaces;
using Xunit;

namespace Queue.Api.Tests
{
    public class QueueControllerTests
    {
        [Fact]
        public void Enqueue_ValidArray_ReturnsOkResult()
        {
            // Arrange
            var queueServiceMock = new Mock<IQueueService>();
            var controller = new QueueController(queueServiceMock.Object);
            int[] inputArray = { 1, 2, 3 };
            int[] expectedResultArray = { 1, 2, 3 };

            queueServiceMock.Setup(q => q.Enqueue(inputArray)).Returns(expectedResultArray);
            queueServiceMock.Setup(q => q.Dequeue()).Returns(1);
            queueServiceMock.Setup(q => q.Peek()).Returns(1);
            queueServiceMock.Setup(q => q.IsEmpty()).Returns(false);

            // Act
            IActionResult result = controller.Enqueue(inputArray);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var responseMessage = Assert.IsType<string>(okResult.Value);
            Assert.Contains("Removed elements: 1,", responseMessage);
            Assert.Contains("Elements at the front of the queue: 1,", responseMessage);
            Assert.Contains("Queue elements: 1, 2, 3", responseMessage);
            Assert.Contains("Empty queue: False", responseMessage);
        }

        [Fact]
        public void Enqueue_EmptyArray_ReturnsBadRequest()
        {
            // Arrange
            var queueServiceMock = new Mock<IQueueService>();
            var controller = new QueueController(queueServiceMock.Object);
            int[] inputArray = Array.Empty<int>();

            // Act
            IActionResult result = controller.Enqueue(inputArray);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errorMessage = Assert.IsType<string>(badRequestResult.Value);
            Assert.Equal("Array cannot be empty.", errorMessage);
        }

        [Fact]
        public void Enqueue_ServiceThrowsArgumentException_ReturnsBadRequestWithError()
        {
            // Arrange
            var queueServiceMock = new Mock<IQueueService>();
            var controller = new QueueController(queueServiceMock.Object);
            int[] inputArray = { 1, 2, 3 };

            queueServiceMock.Setup(q => q.Enqueue(inputArray)).Throws(new ArgumentException("Invalid argument"));

            // Act
            IActionResult result = controller.Enqueue(inputArray);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            var errorMessage = Assert.IsType<string>(badRequestResult.Value);
            Assert.Equal("Error: Invalid argument", errorMessage);
        }
    }
}
