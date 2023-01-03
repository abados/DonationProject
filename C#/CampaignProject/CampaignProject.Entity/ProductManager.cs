using CampaignProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity
{
    public class ProductManager
    {

        //Global Dictionary
        public List<Product> ProductsList = new List<Product>();

        public List<Product> getProductsFromDB()
        {
            Data.Sql.ProductData product = new Data.Sql.ProductData();
            ProductsList = (List<Product>)product.SqlQueryToReadProductsFromDB();
            return ProductsList;
        }

   

        public void SendNewInputToDataLayer(Model.Product newProduct)
        {
            Data.Sql.ProductData product = new Data.Sql.ProductData();
            product.SendSqlQueryToInsertNewOrganizationToDB(newProduct);

        }
    }
}
