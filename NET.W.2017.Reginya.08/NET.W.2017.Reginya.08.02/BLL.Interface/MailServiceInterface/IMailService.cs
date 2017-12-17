using System.Threading.Tasks;

namespace BLL.Interface.MailServiceInterface
{
    /// <summary>
    /// Interface of service for mail notifications.
    /// </summary>
    public interface IMailService
    {
        /// <summary>
        /// Send's mail.
        /// </summary>
        /// <param name="mailData">Describes sending mail.</param>
        void SendMail(MailData mailData);

        /// <summary>
        /// Send's mail asynchronously.
        /// </summary>
        /// <param name="mailData">Describes sending mail.</param>
        /// <returns>Asynchronously task.</returns>
        Task SendMailAsync(MailData mailData);
    }
}
