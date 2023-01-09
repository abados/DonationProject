using CampaignProject.Model;
using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity
{
    public class OwnerManager
    {
        public string getProductByIDFromDB(string UserEmail)
        {
            Data.Sql.OwnerData Owner = new Data.Sql.OwnerData();
            try { 
            return (string)Owner.SendSqlQueryToReadFromDBForOneUser(UserEmail);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                return null;
            }

        }

        public void SendNewInputToDataLayer(Model.Owner newOwner)
        {
            Data.Sql.OwnerData Owner = new Data.Sql.OwnerData();
            string userType = "Admin";
            try { 
            string userID = Owner.AddNewUser(userType);
            Owner.SendSqlQueryToInsertToDB(newOwner,int.Parse(userID));
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
        }

        public void giveCreditOnActions(int ActivityCount, int activistID)
        {
            Data.Sql.OwnerData Owner = new Data.Sql.OwnerData();
            try { 
            Owner.SendSqlQueryToPay(ActivityCount, activistID);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
            }
        }

        public Dictionary<int, Model.ActiveCampaigns> bringDataAboutCampaignsActivity()
        {
            Data.Sql.OwnerData Owner = new Data.Sql.OwnerData();
            Dictionary<int, Model.ActiveCampaigns> activeCampaignList;
            try { 
            activeCampaignList = (Dictionary<int, Model.ActiveCampaigns>)Owner.bringActiveCampaignsTable();
            return activeCampaignList;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                return null;
            }
        }


    }
}
