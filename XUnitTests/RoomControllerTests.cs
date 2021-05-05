using ChatApp;
using ChatApp.Controllers;
using ChatApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace XUnitTests
{
    public class RoomControllerTests
    {
        [Fact]
        public void Index_ReturnsActionResult_WithARoomList()
        {
            var mockContext = new Mock<IHubContext<ChatHub>>();
            var singletonManager = new SingletonManager();
            singletonManager.Rooms.Add(new Room { Name = "TestRoom1", Description = "ABCD" });

            var controller = new RoomController(mockContext.Object, singletonManager);

            var result = controller.Index();
            var actionResult = Assert.IsType<ActionResult<IEnumerable<Room>>>(result);

            var model = Assert.IsAssignableFrom<IEnumerable<Room>>(actionResult.Value);
            Assert.Equal(singletonManager.Rooms.Count(), model.Count());
        }

        [Fact]
        public void Get_ReturnsActionResult_WithASpecificRoom()
        {
            var mockContext = new Mock<IHubContext<ChatHub>>();
            var singletonManager = new SingletonManager();
            singletonManager.Rooms.Add(new Room { Name = "TestRoom1", Description = "ABCD" });
            singletonManager.Rooms.Add(new Room { Name = "TestRoom2", Description = "DCBA" });

            var controller = new RoomController(mockContext.Object, singletonManager);

            var result = controller.Get(singletonManager.Rooms.First().Name);
            var actionResult = Assert.IsType<ActionResult<Room>>(result);

            var model = Assert.IsAssignableFrom<Room>(actionResult.Value);
            Assert.Equal(singletonManager.Rooms.First(), model);
        }

        [Fact]
        public void Create_ReturnsCreatedResult_WithACreatedRoom()
        {
            var mockContext = new Mock<IHubContext<ChatHub>>();
            var singletonManager = new SingletonManager();
            Room newRoom = new Room { Name = "Newly Created Room", Description = "Some text" };

            var controller = new RoomController(mockContext.Object, singletonManager);

            var result = controller.Create(newRoom);
            var createdResult = Assert.IsAssignableFrom<CreatedResult>(result);
            
            var model = Assert.IsAssignableFrom<Room>(createdResult.Value);

            Assert.Equal(newRoom, model);
            Assert.Single(singletonManager.Rooms);
        }

        [Fact]
        public void Create_ReturnsBadRequest_WithInvalidModelState()
        {
            var mockContext = new Mock<IHubContext<ChatHub>>();
            var singletonManager = new SingletonManager();

            var controller = new RoomController(mockContext.Object, singletonManager);
            controller.ModelState.AddModelError("Name", "Required");

            var result = controller.Create(new Room { Description = "Some text" });

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Create_ReturnsBadRequest_WithAlreadyExistingRoom()
        {
            var mockContext = new Mock<IHubContext<ChatHub>>();
            var singletonManager = new SingletonManager();
            Room room = new Room() { Name = "Room1", Description = "Some text" };
            singletonManager.Rooms.Add(room);

            var controller = new RoomController(mockContext.Object, singletonManager);

            var result = controller.Create(room);

            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Remove_ReturnsBadRequest_WithNonExistingRoom()
        {
            var mockContext = new Mock<IHubContext<ChatHub>>();
            var singletonManager = new SingletonManager();
            var controller = new RoomController(mockContext.Object, singletonManager);

            var result = controller.Remove("FictionalRoom");
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void Remove_ReturnsNoContentResult_WithExistingRoom()
        {
            var mockContext = new Mock<IHubContext<ChatHub>>();
            var singletonManager = new SingletonManager();
            singletonManager.Rooms.Add(new Room { Name = "Room", Description = "Description" });
            var controller = new RoomController(mockContext.Object, singletonManager);

            var result = controller.Remove("Room");
            Assert.IsType<NoContentResult>(result);
        }
    }
}
