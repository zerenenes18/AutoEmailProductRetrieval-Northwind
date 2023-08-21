using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductService
    {
        IDataResult<List<Product>> GetAll();
        IResult Delete(Product product);
        IDataResult<List<Product>> GetAllByCategoryId(int id);
        IDataResult<List<Product>> GetByUnitsInStock(decimal min, decimal max);
        IDataResult<List<ProductDetailDto>> GetPrnoductDetails();
        IDataResult<Product> GetById(int id);
        IResult Add(Product product);
    }
}
