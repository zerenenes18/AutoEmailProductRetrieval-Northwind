using Business.Abstract;
using Business.Concrete;
using CatchRequestFromMail;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            

            //MailBodyFilter mailBodyFilter = new MailBodyFilter();


            //Console.WriteLine(requestModel);

            //GetEmail getEmail = new GetEmail();
            //string request1 = "";
            //string request2 = "";
            //IProductService productService = new ProductManager(new EfProductDal());

            startlistening();


        }


        static async Task startlistening()
        {

            IProductService productService = new ProductManager(new EfProductDal());
            ResponseProvider responseProvider = new ResponseProvider(productService);
            MailProvider mailProvider = new MailProvider();
            string request1 = responseProvider.GetLastEmailRequestMessage(); 
            string request2 = request1;
            while (true)
            {
                request1 = responseProvider.GetLastEmailRequestMessage();
                if (request1 != request2)
                {
                    await Console.Out.WriteLineAsync(request1);
                     await mailProvider.SetMail();
                    request2 = request1;
                }
                Thread.Sleep(1000);
            }

        }




        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());
            var result = productManager.GetProductDetails();
            
            if (result.Success == true)
            {
                foreach (var p in result.Data)
                {

                    Console.WriteLine("Product Name :" + p.ProductName
                                    + " ---CategoryName:" + p.CategoryName
                                    + " ---UnitsInStock:" + p.UnitsInStock);
                }
            }
            else
            {
                Console.WriteLine(result.Message);
            }
        }
    }
}
