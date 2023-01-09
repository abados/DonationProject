using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity
{
    public class BusinessManager
    {
        public string getProductByIDFromDB(string UserEmail)
        {
            try
            {
                Data.Sql.BusinessData Business = new Data.Sql.BusinessData();
                return (string)Business.SendSqlQueryToReadFromDBForOneUser(UserEmail);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                return null;
            }

        }

        public string[] getIDS(string userEmail, string campaignName)
        {
            Data.Sql.BusinessData Business = new Data.Sql.BusinessData();
            try { 
            return Business.sqlQuertyToSearchIDS(userEmail, campaignName);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                return null;
            }

        }

        public void SendNewInputToDataLayer(Model.BusinessUser newOwner)
        {
            Data.Sql.BusinessData user = new Data.Sql.BusinessData();
            string userType = "Business";

            try { 
            string userID = user.AddNewUser(userType);
            user.SendSqlQueryToInsertToDB(newOwner, int.Parse(userID));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
          
            }
        }

        public void SendTheItems(int productID)
        {
            Data.Sql.BusinessData businessUser = new Data.Sql.BusinessData();
            try { 
            businessUser.DeliveredTheItem(productID);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
               
            }
        }

    }
}
