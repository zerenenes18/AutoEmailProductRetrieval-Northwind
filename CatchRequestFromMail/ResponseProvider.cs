using Business.Abstract;
using Core.Utilities.Results;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CatchRequestFromMail
{
    public class ResponseProvider
    {
        IProductService _productService;

        public ResponseProvider(IProductService productService)
        {
            _productService = productService; 
        }

        MailBodyFilter mailBodyFilter = new MailBodyFilter();
        GetEmail getEmail = new GetEmail();

        public string GetLastEmailRequestMessage()
        {
            string resultRequestMessage = mailBodyFilter.GetLastEmail().Result;
            return resultRequestMessage;
        }
      
        public string GetResultResponse()
        {
            string resultRequest = mailBodyFilter.GetResultEmail().Result;
            string resultRequestMessage =  GetLastEmailRequestMessage();
            

            if(resultRequest == ConstantsRequests.GetAllProductByCategoryId)
            {
                int categoryID = GetNumber(resultRequestMessage);
                var resultListData = _productService.GetAllByCategoryId(categoryID).Data;
                string dataResult = "<h3 align=\"left\">...PRODUCTS...</h3>";
                foreach (var product in resultListData)
                {
                    dataResult += $"[Product Name : {product.ProductName}, Product Price :{product.UnitPrice},__Product Counts : {product.UnitsInStock} CategoryID: {product.CategoryId}]<br>";
                }
                //string json = JsonConvert.SerializeObject(resultListData, Formatting.Indented);
                return dataResult;

            }
            else if (resultRequest == ConstantsRequests.GetUnderMinCountProducts)
            {
                int min = GetNumber(resultRequestMessage);
                var resultListData = _productService.GetByUnitsInStock((decimal)0, (decimal)min).Data;
                string dataResult = "<h3 align=\"left\">...PRODUCTS...</h3>";
                foreach (var product in resultListData)
                {
                    dataResult += $"[_Product Name => {product.ProductName}, __Product Price : {product.UnitPrice},__Product Counts : {product.UnitsInStock}   CategoryID:  [ {product.CategoryId} ]  ]<br>";
                }
                //string json = JsonConvert.SerializeObject(resultListData, Formatting.Indented);
                return dataResult;
            }
            else if (resultRequest == ConstantsRequests.GetOverTheCountProducts)
            {
                int max = GetNumber(resultRequestMessage);
                var resultListData = _productService.GetByUnitsInStock((decimal)max, (decimal)100).Data;
                string dataResult = "<h3 align=\"left\">...PRODUCTS...</h3>";
                foreach (var product in resultListData)
                {
                    dataResult += $"[_Product Name => {product.ProductName}, __Product Price : {product.UnitPrice},__Product Counts : {product.UnitsInStock},   CategoryID:  [ {product.CategoryId} ]  ]<br>";
                }
                //string json = JsonConvert.SerializeObject(resultListData, Formatting.Indented);
                return dataResult;
            }
            else if (resultRequest == ConstantsRequests.GetAllProductDto)
            {
                int max = GetNumber(resultRequestMessage);
                var resultListData = _productService.GetPrnoductDetails().Data;
                string dataResult = "<h3 align=\"left\">...PRODUCTS...</h3>";
                foreach (var product in resultListData)
                {
                    dataResult += $"[_Product Name => {product.ProductName}, __CategoryName od Product : {product.CategoryName},  UnitsInStock :  [ {product.UnitsInStock} ]  ]<br>";
                }
                //string json = JsonConvert.SerializeObject(resultListData, Formatting.Indented);
                return dataResult;
            }
            else if (resultRequest == ConstantsRequests.GetAllProductDetailById)
            {
                int id = GetNumber(resultRequestMessage);
                var resultListData = _productService.GetById(id).Data;
                string dataResult = "<h3 align=\"left\">...PRODUCTS...</h3>";
                
                dataResult += $"[_Product Name => {resultListData.ProductName}, __Product Price : {resultListData.UnitPrice},__Product Counts : {resultListData.UnitsInStock},   CategoryID:  [ {resultListData.CategoryId} ]  ]<br>";
                
                //string json = JsonConvert.SerializeObject(resultListData, Formatting.Indented);
                return dataResult;
            }



            return "Özür Dilerim... Sanırım ne dediğinizi anlamadım";


        }


        public int GetNumber(string input)
        {
            string[] words = input.Split(' ');
            foreach (string word in words)
            {
                
                int number;
                if (int.TryParse(word, out number))
                {
                    return number;
                }

            }
            return 0;
        }
    }
}
