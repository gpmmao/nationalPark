using System;
using Xunit;
using NSubstitute;

namespace NUnitExp
{
    public class NotificationService_Should
    {
        private readonly IUserRepository _userRepository;
        private readonly INotifier _notifier;
        private readonly ILogger _logger;
        private readonly NotificationService _notificationService;
        public NotificationService_Should()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _notifier = Substitute.For<INotifier>();
            _logger = Substitute.For<ILogger>();
            _notificationService = new NotificationService(_userRepository, _notifier,_logger);
            _userRepository.GetById(Arg.Is<int>(i => i < 10)).Returns(
                new User { HasActivatedNotification = true });
            _userRepository.GetById(Arg.Is<int>(i => i >= 10)).Returns(
                new User { HasActivatedNotification = false });
            _userRepository.GetById(Arg.Is<int>(i => i < 0 )).Returns(
                 User => { throw new InvalidUserIdException(); });
        }

        [Fact]
        public void call_Repository()
        {
         //   _userRepository.GetById(Arg.Any<int>()).Returns(new User());
            _notificationService.NotifyUser(1);
            _userRepository.Received().GetById(Arg.Any<int>());
        }

        [Fact]
        public void Call_Notify_When_User_Has_Activated_Notification()
        {
            _notificationService.NotifyUser(2);

            _notifier.Received().Notify(Arg.Any<User>());
        }

        [Fact]
        public void Does_Not_Call_When_User_Has_Not_Activated_Notification()
        {
            _notificationService.NotifyUser(12);
            _notifier.DidNotReceive().Notify(Arg.Any<User>());
            _notifier.DidNotReceiveWithAnyArgs().Notify(default);
        }

        [Fact]
        public void Call_Logger_When_An_Exception_Is_Thrown()
        {
            _notificationService.NotifyUser(-2);
            _logger.Received().Error("Given User ID is invalid");
        }
    }
}
