using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Trekking.Repository.Models.DB;
using Trekking.Services;

// using Trekking.Services;

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

        [Route("api/products")]
        [HttpPost]
        public async Task<bool?> Store()
        {
            ProductModel product = new ProductModel();
            
            string root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);
            var StoragePath = AppDomain.CurrentDomain.BaseDirectory + "Content\\Resources";
            // Read the form data and return an async task.
            await Request.Content.ReadAsMultipartAsync(provider);

            // Show all the key-value pairs.
            List<String> arr = new List<string>();
            try
            {
                foreach (var key in provider.FormData.AllKeys)
                {
                    foreach (var val in provider.FormData.GetValues(key))
                    {
                        arr.Add(string.Format("{0}: {1}", key, val));
                        if (key == "Name")
                            product.Name = val;
                        if (key == "Price")
                            product.Price = Convert.ToDecimal(val);
                        if (key == "Description")
                            product.Description = val;
                    }
                }
                foreach (MultipartFileData file in provider.FileData)
                {
                    string fileName = file.Headers.ContentDisposition.FileName;
                    if (fileName.StartsWith("\"") && fileName.EndsWith("\""))
                    {
                        fileName = fileName.Trim('"');
                    }
                    if (fileName.Contains(@"/") || fileName.Contains(@"\"))
                    {
                        fileName = Path.GetFileName(fileName);
                    }
                    File.Move(file.LocalFileName, Path.Combine(StoragePath, fileName));
                    product.Path = "\\Content\\Resources\\" + fileName;
                }
                return  ProductService.CreateProduct(product);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        
        
        [Route("api/products/{id}")]
        [HttpGet]
        public ProductModel Show(int id)
        {
            return ProductService.GetProductById(id);
        }

        [Route("api/products/{id}")]
        [HttpDelete]
        public bool? Destroy(int id)
        {
            return  ProductService.DeleteProduct(id);
        }
        
        [Route("api/products/{id}")]
        [HttpPatch]
        public bool? Update(ProductModel product)
        {
            return  ProductService.UpdateProduct(product);
        }
        
    }
}