using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitExp
{
    public interface INotifier
    {
        void Notify(User user);
    }

    public interface IUserRepository
    {
        User GetById(int userid);
    }

    public interface ILookup
    {
        bool TryLookup(string key, out string value);
    }

    public interface ILogger
    {
        void Error(string message);
    }
    public class User
    {
        public bool HasActivatedNotification { get; set; }
    }

    public class InvalidUserIdException : Exception
    {
        public override string Message
        {
            get { return "Given User ID is invalid"; }
        }
    }
}
