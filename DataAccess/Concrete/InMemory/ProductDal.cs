using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class ProductDal : IProductDal
    {
        List<Product> _products;
        public ProductDal()
        {
            _products = new List<Product> {
                new Product{CategoryId = 1, ProductId = 1 , ProductName = "Bardak", UnitsInStock = 15, UnitPrice = 15},
                new Product{CategoryId = 2, ProductId = 1 , ProductName = "Kamera", UnitsInStock = 3, UnitPrice = 500},
                new Product{CategoryId = 3, ProductId = 2 , ProductName = "Telefon", UnitsInStock = 2, UnitPrice = 1500},
                new Product{CategoryId = 4, ProductId = 2 , ProductName = "Klavye", UnitsInStock = 65, UnitPrice = 150},
                new Product{CategoryId = 5, ProductId = 2 , ProductName = "Mouse", UnitsInStock = 1, UnitPrice = 855}
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            Product productToDelete = null;

            productToDelete = _products.SingleOrDefault(p=> p.ProductId == product.ProductId);            
            
            /*foreach (var p in _products)
            {
                   if (product.ProductId == p.ProductId)
                {
                    productToDelete = p;
                } 
            }*/

            _products.Remove(productToDelete);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void UpDate(Product product)
        {
            Product productToUpDate = null;

            productToUpDate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);

            productToUpDate.ProductName = product.ProductName;
            productToUpDate.CategoryId = product.CategoryId;
            productToUpDate.UnitsInStock = product.UnitsInStock;
            productToUpDate.UnitPrice = product.UnitPrice;

    }
    }
}
