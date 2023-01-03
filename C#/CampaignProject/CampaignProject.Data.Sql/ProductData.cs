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

            while (reader.Read())
            {
               Model.Product product = new Model.Product();
                product.productName = reader.GetString(2);
                product.price = reader.GetDecimal(3);
                product.businessID = reader.GetInt32(4);
                product.campaignID = reader.GetInt32(5);
                product.IsBought = reader.GetBoolean(6);
                product.IsDelivered = reader.GetBoolean(7);
                product.ActivistBuyerID = reader.GetInt32(8);
                ProductsList.Add(product);

                //Cheking If Hashtable contains the key
         
           
            }
            return ProductsList;
        }


        public object SqlQueryToReadOrganizationsFromDB()
        {
            string SqlQuery = "select * from NonProfit";
            object retDict = null;
            retDict = DAL.SqlQuery.getDataFromDB(SqlQuery, ReadFromDb);
            return retDict;
        }

        public object SqlQueryToReadProductsFromDB()
        {
            string SqlQuery = "select * from Products";
            object retDict = null;
            retDict = DAL.SqlQuery.getDataFromDB(SqlQuery, ReadFromDb);
            return retDict;
        }

        public void SendSqlQueryToInsertNewOrganizationToDB(Model.Product newProduct)
        {
            string uploadNewProductQuery =
             "insert into Products values('" + newProduct.productName + "'," + newProduct.price + "," + newProduct.businessID + "," + newProduct.campaignID + "," + (newProduct.IsBought ? 1 : 0) + "," + (newProduct.IsDelivered ? 1 : 0) + "," + newProduct.ActivistBuyerID + ")";
            DAL.SqlQuery.Update_Delete_Insert_RowInDB(uploadNewProductQuery);

        }
    }
}
