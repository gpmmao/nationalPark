using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitExp
{
    public class NotificationService
    {
        private readonly IUserRepository _userRepository;
        private readonly INotifier _notifier;
        private readonly ILogger _logger;

        public NotificationService(IUserRepository userRepository, INotifier notifier, ILogger logger)
        {
            _userRepository = userRepository;
            _notifier = notifier;
            _logger = logger;
        }

        public void NotifyUser(int userid)
        {
            User user;
            try
            {
                //add comment
                user = _userRepository.GetById(userid);
            }
            catch ( Exception ex)
            {
                _logger.Error(ex.Message);
                return;
            }

            if (user.HasActivatedNotification)
            {
                _notifier.Notify(user);
            }
        }
    }
}
