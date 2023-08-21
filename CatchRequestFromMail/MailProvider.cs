using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace CatchRequestFromMail
{
    public class MailProvider
    {
        public async Task SetMail()
        {
            IProductService productService = new ProductManager(new EfProductDal());

            ResponseProvider provider = new ResponseProvider(productService);


            string jsonResult =  provider.GetResultResponse();



            string body = jsonResult;

            try
            {
                // SmtpClient ve MailMessage nesnelerini oluşturun
                using (MailMessage mailMessage = new MailMessage(MailRules.senderEmail, MailRules.recipientEmail, MailRules.subject, body))
                {

                    mailMessage.IsBodyHtml = true;
                    //mailMessage.Attachments.Add(new Attachment("C:\\file.zip"));


                    using (SmtpClient smtpClient = new SmtpClient(MailRules.smtpServer, MailRules.smtpPort))
                    {
                        smtpClient.EnableSsl = true;
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential(MailRules.senderEmail, MailRules.appPassword);

                        smtpClient.Send(mailMessage);
                        Console.WriteLine("E-posta başarıyla gönderildi.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("E-posta gönderirken bir hata oluştu: " + ex.Message);
            }








            //MailBodyFilter provider = new MailBodyFilter();

            //var result = provider.GetResult("Veri tabanından tüm ürünlerin sayısını getir");

            //await Console.Out.WriteLineAsync( result);


            //string input = "Bu bir örnek cümle 123 içindeki 456 sayıları alalım 789";

            //// String içindeki tüm sayıları alalım
            //string[] words = input.Split(' ');
            //foreach (string word in words)
            //{
            //    Console.WriteLine(word);
            //    int number;
            //    if (int.TryParse(word, out number))
            //    {
            //        // Sayıyı başarılı bir şekilde çıkardık, istediğiniz işlemi yapabilirsiniz
            //        Console.WriteLine(number);
            //        Console.WriteLine(word);
            //    }
            //}




            // GET mails -------------

            //var receivedEmails = await GetLastEmails.ReceiveEmails();

            //// Alınan e-postaları işleyin
            //foreach (var message in receivedEmails)
            //{
            //    // E-posta içeriğini işleyin (örn. gönderen, konu, gövde, ekler vb.)
            //    Console.WriteLine($"Gönderen: {message.From}");
            //    Console.WriteLine($"Konu: {message.Subject}");
            //    Console.WriteLine($"Gövde: {message.TextBody}");

            //    // Ekleri işlemek için aşağıdaki gibi kullanabilirsiniz:
            //    //foreach (var attachment in message.Attachments)
            //    //{
            //    //    // Ekleri işleyin (örn. kaydetmek için)
            //    //    using (var stream = File.Create(attachment.FileName))
            //    //    {
            //    //        attachment.ContentObject.DecodeTo(stream);
            //    //    }
            //    //}
            //}





            // SEND MAİL && GitHub -----------------

            //MailObject mailObject = new MailObject();

            //string jsonResult = await mailObject.GetGitHubRepositoryDataAsync();


            ////await Console.Out.WriteLineAsync(jsonResult);

            //string body = jsonResult;

            //try
            //{
            //    // SmtpClient ve MailMessage nesnelerini oluşturun
            //    using (MailMessage mailMessage = new MailMessage(MailRules.senderEmail, MailRules.recipientEmail, MailRules.subject, body))
            //    {

            //        mailMessage.IsBodyHtml = true;
            //        //mailMessage.Attachments.Add(new Attachment("C:\\file.zip"));


            //        using (SmtpClient smtpClient = new SmtpClient(MailRules.smtpServer, MailRules.smtpPort))
            //        {
            //            smtpClient.EnableSsl = true;
            //            smtpClient.UseDefaultCredentials = false;
            //            smtpClient.Credentials = new NetworkCredential(MailRules.senderEmail, MailRules.appPassword);

            //            smtpClient.Send(mailMessage);
            //            Console.WriteLine("E-posta başarıyla gönderildi.");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("E-posta gönderirken bir hata oluştu: " + ex.Message);
            //}

        }
    }
}
