using ChatApp;
using ChatApp.Models;
using Microsoft.AspNetCore.SignalR;
using Moq;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace XUnitTests
{
    public class ChatHubTests
    {
        private readonly Mock<IHubCallerClients> _mockClients = new Mock<IHubCallerClients>();
        private readonly Mock<IClientProxy> _mockClientProxy = new Mock<IClientProxy>();
        private readonly Mock<IGroupManager> _mockGroups = new Mock<IGroupManager>();
        private readonly Mock<HubCallerContext> _mockContext = new Mock<HubCallerContext>();

        public ChatHubTests()
        {
            _mockClients.Setup(x => x.All)
                .Returns(_mockClientProxy.Object);
            _mockClients.Setup(x => x.Caller)
                .Returns(_mockClientProxy.Object);
            _mockClients.Setup(x => x.Group(It.IsAny<string>()))
                .Returns(_mockClientProxy.Object);

            _mockGroups.Setup(x => 
                x.AddToGroupAsync(
                    It.IsAny<string>(), 
                    It.IsAny<string>(), 
                    new System.Threading.CancellationToken()))
                .Returns(Task.FromResult(true));

            _mockGroups.Setup(x =>
                x.RemoveFromGroupAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    new System.Threading.CancellationToken()))
                .Returns(Task.FromResult(true));

            _mockContext.Setup(x => x.ConnectionId)
                .Returns(Guid.NewGuid().ToString());
        }

        private ChatHub CreateChatHubInstance(SingletonManager sm)
        {
            return new ChatHub(sm)
            {
                Clients = _mockClients.Object,
                Groups = _mockGroups.Object,
                Context = _mockContext.Object
            };
        }

        [Fact]
        public void JoinRoom_AddsClientConnection_WithExistingRoomName()
        {
            SingletonManager sm = new SingletonManager();
            sm.Rooms.Add(new Room() { Name = "R1" });
            var chatHub = CreateChatHubInstance(sm);

            chatHub.JoinRoom(new ClientConnection { Client = "C1", Room = "R1" }).GetAwaiter().GetResult();

            Assert.Single(sm.ClientConnections);
        }

        [Fact]
        public void JoinRoom_AddsConnectionIdToGroup_WithExistingRoomName()
        {
            SingletonManager sm = new SingletonManager();
            sm.Rooms.Add(new Room() { Name = "R1" });
            var chatHub = CreateChatHubInstance(sm);

            chatHub.JoinRoom(new ClientConnection { Client = "C1", Room = "R1" }).GetAwaiter().GetResult();

            _mockGroups.Verify(x =>
                x.AddToGroupAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    default(CancellationToken)),
                Times.Once());
        }

        [Fact]
        public void JoinRoom_DoesNotAddClientConnection_WithNonExistingRoomName()
        {
            SingletonManager sm = new SingletonManager();
            var chatHub = CreateChatHubInstance(sm);

            chatHub.JoinRoom(new ClientConnection { Client = "C1", Room = "R1" }).GetAwaiter().GetResult();

            Assert.Empty(sm.ClientConnections);
        }
    }
}
