using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity
{
    public class BusinessManager : BaseEntity
    {
        public BusinessManager(Logger log) : base(log)
        {
        }

        public string FindTheUser(string UserEmail)
        {
            try
            {
                Data.Sql.BusinessData Business = new Data.Sql.BusinessData(Logger);
                return (string)Business.SendSqlQueryToReadFromDBForOneUser(UserEmail);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
                return null;
            }

        }

        public string[] getIDS(string userEmail, string campaignName)
        {
            Data.Sql.BusinessData Business = new Data.Sql.BusinessData(Logger);
            try { 
            return Business.sqlQuertyToSearchIDS(userEmail, campaignName);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
                return null;
            }

        }

        public void InsertNewMember(Model.BusinessUser newOwner)
        {
            Data.Sql.BusinessData user = new Data.Sql.BusinessData(Logger);
            string userType = "Business";

            try { 
            string userID = user.AddNewUser(userType);
            user.SendSqlQueryToInsertToDB(newOwner, int.Parse(userID));
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
          
            }
        }

        public void SendTheItems(int productID)
        {
            Data.Sql.BusinessData businessUser = new Data.Sql.BusinessData(Logger);
            try { 
            businessUser.DeliveredTheItem(productID);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
               
            }
        }

    }
}
