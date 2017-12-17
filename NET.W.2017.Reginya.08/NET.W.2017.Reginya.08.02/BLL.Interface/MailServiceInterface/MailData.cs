using System;
using System.Net.Mail;

namespace BLL.Interface.MailServiceInterface
{
    public class MailData
    {
        #region Private fields

        private string _from;
        private string _password;
        private string _to;

        #endregion 

        #region Public constructors

        public MailData()
        {
        }

        public MailData(string from, string password, string to, string subject, string message)
        {
            From = from;
            Password = password;
            To = to;
            Subject = subject;
            Message = message;
        }

        #endregion 

        #region Public properties

        public string From
        {
            get => _from;                      

            set
            {
                VerifyEmailAddress(value, $"{nameof(From)} is invalid.", nameof(From));
                _from = value;
            }
        }

        public string Password
        {
            get => _password;                

            set
            {
                VerifyString(value, $"{nameof(Password)} is invalid", nameof(Password));
                _password = value;
            }
        }

        public string To
        {
            get => _to;            

            set
            {
                VerifyEmailAddress(value, $"{nameof(To)} is invalid.", nameof(To));
                _to = value;
            }
        }

        public string Subject { get; set; }

        public string Message { get; set; }

        #endregion

        #region Private methods

        private static void VerifyEmailAddress(string email, string errorMessage, string paramName)
        {
            try
            {
                VerifyString(email, errorMessage, paramName);
                var mailAddress = new MailAddress(email);
            }
            catch (Exception)
            {
                throw new ArgumentException(errorMessage, paramName);
            }
        }

        private static void VerifyString(string data, string errorMessage, string paramName)
        {
            if (string.IsNullOrWhiteSpace(data))
            {
                throw new ArgumentException(errorMessage, paramName);
            }
        }

        #endregion
    }
}
