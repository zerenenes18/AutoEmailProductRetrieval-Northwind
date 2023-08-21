using MailKit.Net.Imap;
using MailKit.Security;
using MailKit;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatchRequestFromMail
{
    public class GetEmail
    {
        public async Task<IEnumerable<MimeMessage>> ReceiveEmails()
        {
            var messages = new List<MimeMessage>();

            using (var client = new ImapClient())
            {
                try
                {
                    // Gmail IMAP sunucusuna bağlanın
                    await client.ConnectAsync("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);

                    // Gmail e-posta hesabınıza giriş yapın
                    await client.AuthenticateAsync($"{MailRules.senderEmail}", $"{MailRules.appPassword}");

                    // Gelen kutusunu açın
                    await client.Inbox.OpenAsync(FolderAccess.ReadOnly);

                    // En son 10 e-postayı alın
                    var messageCount = client.Inbox.Count;
                    var startIndex = Math.Max(0, messageCount - 1); // Son 10 e-postayı almak için

                    for (int i = startIndex; i < messageCount; i++)
                    {
                        var message = await client.Inbox.GetMessageAsync(i);
                        messages.Add(message);
                    }
                }
                catch (Exception ex)
                {
                    // E-posta alırken bir hata oluştu
                    Console.WriteLine("E-posta alırken bir hata oluştu: " + ex.Message);
                }
                finally
                {
                    // Bağlantıyı kapatın
                    client.Disconnect(true);
                }
            }

            return messages;
        }
    }
}
