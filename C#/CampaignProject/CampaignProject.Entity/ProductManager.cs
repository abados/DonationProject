using CampaignProject.Model;
using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity
{
    public class ProductManager : BaseEntity
    {
        public ProductManager(Logger log) : base(log)
        {
        }

        //Global List
        public List<Product> ProductsList = new List<Product>();

        public List<Product> getUnBoughtProductsOfSpecificBusinessFromDB(int userID)
        {//to get products that specific Business donate
            Data.Sql.ProductData product = new Data.Sql.ProductData(Logger);
            try {
                ProductsList = (List<Product>)product.SqlQueryToReadUnBoughtProductsFromDB(userID);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return ProductsList;
        }
        public List<Shipment> ProductsToShipList = new List<Shipment>();

       

        public List<Shipment> getBoughtProductsOfSpecificBusinessFromDB(int userID)
        {//to get products that specific Business donate
            Data.Sql.ProductData product = new Data.Sql.ProductData(Logger);
            try
            {
                ProductsToShipList = (List<Shipment>)product.SqlQueryToReadBoughtProductsFromDB(userID);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return ProductsToShipList;
        }


        public List<Product> getPurchesProductsOFromDB(string userEmail)
        {//to get bought products of specific Activist
            Data.Sql.ProductData product = new Data.Sql.ProductData(Logger);
            try {
                ProductsList = (List<Product>)product.SqlQueryToReadPurchesProductsFromDB(userEmail);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return ProductsList;
        }

        public List<Product> getProductsOfSpecificCampaignFromDB(string campaignName)
        {
            Data.Sql.ProductData product = new Data.Sql.ProductData(Logger);
            try {
                ProductsList = (List<Product>)product.SqlQueryToReadProductsFromDB(campaignName);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return ProductsList;
        }

        public void DeleteAProduct(string productName, int businessID,int campaignID)
        {//only by the business man and only if not bought
            Data.Sql.ProductData campaign = new Data.Sql.ProductData(Logger);
            try {
                campaign.DeleteProduct(productName, businessID, campaignID);
            } 
            catch(Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
        }

    public void InsertNewItem(Model.Product newProduct)
        {
            Data.Sql.ProductData product = new Data.Sql.ProductData(Logger);
            try { 
            product.SendSqlQueryToInsertNewProductToDB(newProduct);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
        }


        /////////////Report functions////////////
        

        public List<Product> getBoughtProductsFromDB()
        {//to get products that specific Business donate
            Data.Sql.ProductData product = new Data.Sql.ProductData(Logger);
            try
            {
            ProductsList = (List<Product>)product.SqlQueryToReadAllBoughtProductsFromDB();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return ProductsList;
        }

        public List<Product> getBoughtAndNotDeliverdProductsFromDB()
        {//to get products that specific Business donate
            Data.Sql.ProductData product = new Data.Sql.ProductData(Logger);
            try { 
            ProductsList = (List<Product>)product.SqlQueryToReadAllBoughtAndNotDeliverdProductsFromDB();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return ProductsList;
        }

        public List<Product> getAllProductsForReport()
        {
            Data.Sql.ProductData product = new Data.Sql.ProductData(Logger);
            try { 
            ProductsList = (List<Product>)product.SqlQueryToReadAllProductsFromDB();
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return ProductsList;
        }
    }
}
