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

        public List<Product> getProductsOfSpecificBusinessFromDB(int userID)
        {
            Data.Sql.ProductData product = new Data.Sql.ProductData();
            ProductsList = (List<Product>)product.SqlQueryToReadProductsFromDB(userID);
            return ProductsList;
        }

        public List<Product> getPurchesProductsOFromDB(string userEmail)
        {
            Data.Sql.ProductData product = new Data.Sql.ProductData();
            ProductsList = (List<Product>)product.SqlQueryToReadPurchesProductsFromDB(userEmail);
            return ProductsList;
        }

        public List<Product> getProductsOfSpecificCampaignFromDB(string campaignName)
        {
            Data.Sql.ProductData product = new Data.Sql.ProductData();
            ProductsList = (List<Product>)product.SqlQueryToReadProductsFromDB(campaignName);
            return ProductsList;
        }

        public void DeleteAProduct(string productName, int businessID)
        {
            Data.Sql.ProductData campaign = new Data.Sql.ProductData();
            campaign.DeleteProduct(productName, businessID);
        }

        public void SendNewInputToDataLayer(Model.Product newProduct)
        {
            Data.Sql.ProductData product = new Data.Sql.ProductData();
            product.SendSqlQueryToInsertNewProductToDB(newProduct);

        }
    }
}
