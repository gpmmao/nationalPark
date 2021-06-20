using System;
using System.Collections.Generic;
using System.Text;
using NSubstitute;
using Xunit;

namespace NUnitExp.Test
{
    public class NitifyTest
    {
        private readonly IUserRepository _userRepository;
        private readonly INotifier _notifier;
        private readonly ILogger _logger;
        private readonly NotificationService _notificationService;
        private readonly ILookup _lookup;

        public NitifyTest()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _notifier = Substitute.For<INotifier>();
            _logger = Substitute.For<ILogger>();
            _lookup = Substitute.For<ILookup>();
            _notificationService = new NotificationService(_userRepository, _notifier, _logger);
        }

        [Fact]
        public void call_getUserId()
        {
            _userRepository.GetById(1).Returns(new User { HasActivatedNotification = false });

            _notificationService.NotifyUser(1);

            _userRepository.Received().GetById(1);
        }

        [Fact]
        public void call_Notify_if_HasActivatedNotification_True()
        {
            var user = new User() { HasActivatedNotification = true };
            _userRepository.GetById(1).Returns(user);
            _notificationService.NotifyUser(1);

            _notifier.Received().Notify(user);
        }

        [Fact]
        public void call_Logger_when_GetByID_Exception()
        {
            InvalidUserIdException exp = new InvalidUserIdException();
            var user = new User() { HasActivatedNotification = true };
            _userRepository.GetById(1).Returns(x => throw exp);
            _notificationService.NotifyUser(1);
            _notifier.DidNotReceive().Notify(user);

            _logger.Received().Error(exp.Message);
        }

        [Fact]
        public void Get_out_value()
        {
            _lookup.TryLookup("Good", out Arg.Any<string>())
                   .Returns(x =>
                  {
                      x[1] = "Afternoon!";
                      return true;
                  });

            var result = _lookup.TryLookup("Good", out var value);
            Assert.True(result);
            Assert.Equal("Afternoon!", value);
        }
    }
}

    