using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatchRequestFromMail
{
    public class MailBodyFilter
    {
        string[] models = new string[] { ConstantsRequests.GetUnderMinCountProducts,
                                         ConstantsRequests.GetOverTheCountProducts,
                                         ConstantsRequests.GetAllProductDto,
                                         ConstantsRequests.GetAllProductDetailById,
                                         ConstantsRequests.GetAllProductByCategoryId};


        GetEmail getEmail = new GetEmail();

       public async Task<string>  GetLastEmail()
        {
            var receivedEmails = await getEmail.ReceiveEmails();
            string lastMessage = "";
            
            foreach (var message in receivedEmails)
            {

                lastMessage = message.TextBody;


                //Console.WriteLine($"Gönderen: {message.From}");
                //Console.WriteLine($"Konu: {message.Subject}");
                //Console.WriteLine($"Gövde: {message.TextBody}");

                //foreach (var attachment in message.Attachments)
                //{
                //    // Ekleri işleyin (örn. kaydetmek için)
                //    using (var stream = File.Create(attachment.FileName))
                //    {
                //        attachment.ContentObject.DecodeTo(stream);
                //    }
                //}
            }
            
            return lastMessage;
        }
        
        public async Task<string> GetResultEmail()
        {
            
            string message = await GetLastEmail();
            string result = Filter(message);
            
            return result;
        }
       

        public string Filter(string target)
        {
            var temp = "not sense";

            for (int i = 0; i < models.Length - 1; i++)
            {
                temp = MatchTwoSentenceAndTarget(models[i], models[i + 1], target);
                if (temp == models[i])
                {
                    //Console.WriteLine(models[i]);
                    models[i + 1] = models[i];
                }
            }

            return temp;

        }







        public string MatchTwoSentenceAndTarget(string pattern1, string pattern2, string target)
        {
            string resultPattern = ComparePatterns(pattern1, pattern2, target);
            //Console.WriteLine("Daha fazla eşleşen kalıp: " + resultPattern);

            return resultPattern;
        }

        public string ComparePatterns(string pattern1, string pattern2, string target)
        {
            int distanceToPattern1 = CalculateLevenshteinDistance(pattern1, target);
            int distanceToPattern2 = CalculateLevenshteinDistance(pattern2, target);

            if (distanceToPattern1 <= distanceToPattern2)
                return pattern1;
            else
                return pattern2;
        }

        public int CalculateLevenshteinDistance(string a, string b)
        {
            int[,] dp = new int[a.Length + 1, b.Length + 1];

            for (int i = 0; i <= a.Length; i++)
                dp[i, 0] = i;

            for (int j = 0; j <= b.Length; j++)
                dp[0, j] = j;

            for (int i = 1; i <= a.Length; i++)
            {
                for (int j = 1; j <= b.Length; j++)
                {
                    int cost = (a[i - 1] == b[j - 1]) ? 0 : 1;
                    dp[i, j] = Math.Min(Math.Min(dp[i - 1, j] + 1, dp[i, j - 1] + 1), dp[i - 1, j - 1] + cost);
                }
            }

            return dp[a.Length, b.Length];
        }
    }


}
