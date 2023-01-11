using CampaignProject.Model;
using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Data.Sql
{
    public class ProductData
    {
        public List<Model.Product> ReadFromDb(SqlDataReader reader)
        {
            List<Model.Product> ProductsList = new List<Model.Product>();

            //Clear Hashtable Before Inserting Information From Sql Server
            ProductsList.Clear();

            try
            {
                while (reader.Read())
                {
                    Model.Product product = new Model.Product();
                    product.productName = reader.GetString(1);
                    product.price = reader.GetDecimal(2);
                    product.businessID = reader.GetInt32(3);
                    product.campaignID = reader.GetInt32(4);
                    product.IsBought = reader.GetBoolean(5);
                    product.IsDelivered = reader.GetBoolean(6);
                    product.ActivistBuyerID = reader.GetInt32(7);
                    ProductsList.Add(product);




                }
                return ProductsList;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return null;
        }

        public List<Model.Shipment> ReadShipmentFromDb(SqlDataReader reader)
        {
            List<Model.Shipment> ProductToShipsList = new List<Model.Shipment>();

            //Clear Hashtable Before Inserting Information From Sql Server
            ProductToShipsList.Clear();
            try { 
            while (reader.Read())
            {
                Model.Shipment productToShip = new Model.Shipment();
                productToShip.Id = reader.GetInt32(0);
                productToShip.productName = reader.GetString(1);
                productToShip.price = reader.GetDecimal(2);
                productToShip.ActivistBuyerID = reader.GetInt32(3);
                productToShip.IsDelivered= reader.GetBoolean(4);
                productToShip.fullName = reader.GetString(5);
                productToShip.addressToShip = reader.GetString(6);
                
                ProductToShipsList.Add(productToShip);

              


            }
            return ProductToShipsList;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return null;
        }


        public object SqlQueryToReadOrganizationsFromDB()
        {
            string SqlQuery = "select * from NonProfit";
            object retDict = null;
            try { 
            retDict = DAL.SqlQuery.getDataFromDB(SqlQuery, ReadFromDb);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
           
            return retDict;
        }

        public object SqlQueryToReadUnBoughtProductsFromDB(int userID)
        {
            string SqlQuery = "select * from Products where BusinessUser="+userID+ " and IsBought = 0";
            object retDict = null;
            try { 
            retDict = DAL.SqlQuery.getDataFromDB(SqlQuery, ReadFromDb);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return retDict;
        }

        public object SqlQueryToReadBoughtProductsFromDB(int userID)
        {
            string SqlQuery = "SELECT p.id, p.ProductName, p.Price, p.ActivistBuyerID,p.IsDelivered , a.FullName, a.Address FROM Products p INNER JOIN Activists a ON p.ActivistBuyerID = a.id WHERE p.BusinessUser ="+userID+ " AND p.IsBought = 1";
            object retDict = null;
            try
            {
                retDict = DAL.SqlQuery.getDataFromDB(SqlQuery, ReadShipmentFromDb);
            } 
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return retDict;
        }

        public object SqlQueryToReadProductsFromDB(string campaignName)
        {
            string SqlQuery = "select * from Products where Campaign=(select CampaignId from Campaigns where CampaignName='" + campaignName + "') and IsBought = 0";
            object retDict = null;
            try { 
            retDict = DAL.SqlQuery.getDataFromDB(SqlQuery, ReadFromDb);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return retDict;
        }

        public object SqlQueryToReadPurchesProductsFromDB(string userEmail)
        {
            string SqlQuery = "select * from Products where ActivistBuyerID=\r\n(select id from Activists where Email ='" + userEmail + "') and IsBought = 1";
            object retDict = null;
            try { 
            retDict = DAL.SqlQuery.getDataFromDB(SqlQuery, ReadFromDb);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return retDict;
        }

        public void SendSqlQueryToInsertNewProductToDB(Model.Product newProduct)
        {
            string uploadNewProductQuery =
             "insert into Products values('" + newProduct.productName + "'," + newProduct.price + "," + newProduct.businessID + "," + newProduct.campaignID + "," + (newProduct.IsBought ? 1 : 0) + "," + (newProduct.IsDelivered ? 1 : 0) + "," + newProduct.ActivistBuyerID + ")";
            try { 
            DAL.SqlQuery.Update_Delete_Insert_RowInDB(uploadNewProductQuery);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            string updateTheDonateInTheCampaign = "UPDATE Campaigns SET DonationsAmount=DonationsAmount +" + newProduct.price + " WHERE CampaignId= " + newProduct.campaignID + "";
            try { 
            DAL.SqlQuery.Update_Delete_Insert_RowInDB(updateTheDonateInTheCampaign);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
        }

        public void DeleteProduct(string productName, int businessID)
        {
            string deleteQuery = "delete from Products where ProductName ='" + productName + "' and BusinessUser=" + businessID + "";
            try { 
            DAL.SqlQuery.Update_Delete_Insert_RowInDB(deleteQuery);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
        }

        /////////////////Reports functions////////////
        

        public object SqlQueryToReadAllBoughtProductsFromDB()
        {
            string SqlQuery = "select * from Products where IsBought=1";
            object retDict = null;
            try { 
            retDict = DAL.SqlQuery.getDataFromDB(SqlQuery, ReadFromDb);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return retDict;
        }

        public object SqlQueryToReadAllBoughtAndNotDeliverdProductsFromDB()
        {
            string SqlQuery = "select * from Products where IsBought=1 and IsDelivered=0";
            object retDict = null;
            try { 
            retDict = DAL.SqlQuery.getDataFromDB(SqlQuery, ReadFromDb);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return retDict;
        }

        public object SqlQueryToReadAllProductsFromDB()
        {
            string SqlQuery = "select * from Products";
            object retDict = null;
            try { 
            retDict = DAL.SqlQuery.getDataFromDB(SqlQuery, ReadFromDb);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
            return retDict;
        }
    }
}
