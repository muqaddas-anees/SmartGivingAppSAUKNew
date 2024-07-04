using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DeffinityAppDev.api
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }


    }
    
    public class ProductController : ApiController
    {
       
        public static Lazy<List<Product>> products = new Lazy<List<Product>>();//Static variable use only for demo, don’t use unless until require in project. 
        public static int PgaeLoadFlag = 1; // Page load count. 
        public static int ProductID = 4;
        public ProductController()
        {
            if (PgaeLoadFlag == 1) //use this only for first time page load
            {
                //Three product added to display the data
                products.Value.Add(new Product { ID = 1, Name = "bus", Category = "Toy", Price = 200.12M });
                products.Value.Add(new Product { ID = 2, Name = "Car", Category = "Toy", Price = 300 });
                products.Value.Add(new Product { ID = 3, Name = "robot", Category = "Toy", Price = 3000 });
                PgaeLoadFlag++;
            }
        }

        // GET api/product
        
        public List<Product> GetAllProducts() //get method
        {
            //Instedd of static variable you can use database resource to get the data and return to API
            return products.Value; //return all the product list data
        }
        // GET api/product
        
        [HttpGet]
        public List<Product> Get() //get method
        {
            //Instedd of static variable you can use database resource to get the data and return to API
            return products.Value; //return all the product list data
        }

        // GET api/product/5
        public IHttpActionResult GetProduct(int id)
        {
            Product product = products.Value.FirstOrDefault((p) => p.ID == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST api/product
        public void ProductAdd(Product product) //post method
        {
            product.ID = ProductID;
            products.Value.Add(product); //add the post product data to the product list
            ProductID++;
            //instead of adding product data to the static product list you can save data to the database.
        }
        //// GET api/<controller>
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //// GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        //// POST api/<controller>
        //public void Post([FromBody]string value)
        //{
        //}

        //// PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}
    }
}