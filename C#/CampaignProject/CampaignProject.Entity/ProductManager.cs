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

        //Global List
        public List<Product> ProductsList = new List<Product>();

        public List<Product> getUnBoughtProductsOfSpecificBusinessFromDB(int userID)
        {//to get products that specific Business donate
            Data.Sql.ProductData product = new Data.Sql.ProductData();
            ProductsList = (List<Product>)product.SqlQueryToReadUnBoughtProductsFromDB(userID);
            return ProductsList;
        }
        public List<Shipment> ProductsToShipList = new List<Shipment>();
        public List<Shipment> getBoughtProductsOfSpecificBusinessFromDB(int userID)
        {//to get products that specific Business donate
            Data.Sql.ProductData product = new Data.Sql.ProductData();
            ProductsToShipList = (List<Shipment>)product.SqlQueryToReadBoughtProductsFromDB(userID);
            return ProductsToShipList;
        }

        public List<Product> getPurchesProductsOFromDB(string userEmail)
        {//to get bought products of specific Activist
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
        {//only by the business man and only if not bought
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
