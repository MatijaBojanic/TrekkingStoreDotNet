using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Trekking.Models.DB;
using Trekking.Services;

namespace Trekking.Controllers.api
{
    public class ProductsController : ApiController
    {
        [Route("api/products")]
        [HttpGet]
        public List<ProductModel> Index()
        {
            return ProductService.GetProducts();
        }
        
        [Route("api/products/{id}")]
        [HttpGet]
        public ProductModel Show(int id)
        {
            return ProductService.GetProductById(id);
        }

        [Route("api/products")]
        [HttpPost]
        public bool? Store(ProductModel product)
        {
            return  ProductService.CreateProduct(product);
        }

        [Route("api/products/{id}")]
        [HttpDelete]
        public bool? Destroy(int productId)
        {
            return  ProductService.DeleteProduct(productId);
        }
        
        [Route("api/products/{id}")]
        [HttpPatch]
        public bool? Update(ProductModel product)
        {
            return  ProductService.UpdateProduct(product);
        }
        
    }
}