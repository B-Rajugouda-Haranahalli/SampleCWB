using CWB.CommonUtils.Notifications;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Threading.Tasks;

namespace CWB.Notification
{
    public class EmailNotification
    {
        /// <summary>
        /// Send Otp Email
        /// </summary>
        /// <param name="emailObject"></param>
        /// <param name="toAddress"></param>
        /// <param name="Name"></param>
        /// <param name="emailType"></param>
        /// <param name="emailSettings"></param>
        /// <returns></returns>
        public async Task<bool> SendAsync(object emailObject, string toAddress, string Name, string emailTemplateId, EmailSettings emailSettings)
        {
            var client = new SendGridClient(emailSettings.ApiKey);
            var sendGridMessage = new SendGridMessage();
            sendGridMessage.SetFrom(emailSettings.FromAddress);
            sendGridMessage.AddTo(toAddress, Name);
            sendGridMessage.SetTemplateId(emailTemplateId);
            sendGridMessage.SetTemplateData(emailObject);
            var response = await client.SendEmailAsync(sendGridMessage);
            if (response.StatusCode == System.Net.HttpStatusCode.Accepted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
