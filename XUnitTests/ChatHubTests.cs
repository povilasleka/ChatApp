using ChatApp;
using ChatApp.Models;
using ChatApp.Services;
using Microsoft.AspNetCore.SignalR;
using Moq;
using System;
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

        private readonly Mock<IService<Room>> _mockRoomService = new Mock<IService<Room>>();
        private readonly Mock<IService<Connection>> _mockConnectionService = new Mock<IService<Connection>>();

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

        private ChatHub CreateChatHubInstance()
        {
            return new ChatHub(_mockRoomService.Object, _mockConnectionService.Object)
            {
                Clients = _mockClients.Object,
                Groups = _mockGroups.Object,
                Context = _mockContext.Object
            };
        }

        /*[Fact]
        public void JoinRoom_AddsClientConnection_WithExistingRoomName()
        {
            _mockConnectionService.Setup(x => x.Contains(It.IsAny<Func<Connection, bool>>())).Returns(true);

            var chatHub = CreateChatHubInstance();

            chatHub.JoinRoom(new Connection { Client = "C1", Room = "R1" }).GetAwaiter().GetResult();

            _mockConnectionService.Verify(x => 
                x.Add(It.IsAny<Connection>()), 
                Times.Once());
        }

        [Fact]
        public void JoinRoom_AddsConnectionIdToGroup_WithExistingRoomName()
        {
            _mockConnectionService.Setup(x => x.Contains(It.IsAny<Func<Connection, bool>>())).Returns(true);
            var chatHub = CreateChatHubInstance();

            chatHub.JoinRoom(new Connection { Client = "C1", Room = "R1" }).GetAwaiter().GetResult();

            _mockGroups.Verify(x =>
                x.AddToGroupAsync(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    default(CancellationToken)),
                Times.Once());
        }*/

        [Fact]
        public void JoinRoom_DoesNotAddClientConnection_WithNonExistingRoomName()
        {
            _mockConnectionService.Setup(x => x.Contains(It.IsAny<Func<Connection, bool>>())).Returns(false);

            var chatHub = CreateChatHubInstance();

            chatHub.JoinRoom(new Connection { Client = "C1", Room = "R1" }).GetAwaiter().GetResult();

            _mockConnectionService.Verify(x =>
                 x.Add(It.IsAny<Connection>()),
                 Times.Never);
        }
    }
}
