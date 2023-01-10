using CampaignProject.Model;
using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity
{
    public class CampaignManager
    {

        //Global Dictionary
        public Dictionary<string, Campaign> CampaignsDict = new Dictionary<string, Campaign>();

        public List<Campaign> CampaignsList = new List<Campaign>();

        public List<ActiveCampaigns> ActiveCampaignsList = new List<ActiveCampaigns>();
        public Dictionary<string, Campaign> getCampaignsFromDBInDIctionary()
        {
            Data.Sql.CampaignData campaign = new Data.Sql.CampaignData();
            try {
                CampaignsDict = (Dictionary<string, Campaign>)campaign.SqlQueryToReadCampaignsFromDBIntoDict();
                return CampaignsDict;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                return null;
            }
           
        }

        public List<Campaign> getCampaignsFromDBInList()
        {
            Data.Sql.CampaignData campaign = new Data.Sql.CampaignData();
            try
            {
                CampaignsList = (List<Campaign>)campaign.SqlQueryToReadCampaignsFromDBIntoList();
                return CampaignsList;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                return null;
            }

        }


        public List<ActiveCampaigns> getActiveCampaignsFromDBInList()
        {
            Data.Sql.CampaignData campaign = new Data.Sql.CampaignData();
            try
            {
                ActiveCampaignsList = (List<ActiveCampaigns>)campaign.SqlQueryToReadActiveCampaignsFromDBIntoList();
                return ActiveCampaignsList;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                return null;
            }

        }

        public Dictionary<string, Campaign> getSpecificCampaignsFromDB(string organizationEmail)
        {
            Data.Sql.CampaignData campaign = new Data.Sql.CampaignData();
            try {
                CampaignsDict = (Dictionary<string, Campaign>)campaign.specificCampaigns(organizationEmail); ;
            return CampaignsDict;
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                return null;
            }
        }

        public void SendNewInputToDataLayer(Model.Campaign newCampaign, string uesrEmail)
        {
            Data.Sql.CampaignData campaign = new Data.Sql.CampaignData();
            try
            {
                campaign.SendSqlQueryToInsertToDB(newCampaign, uesrEmail);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
    
            }
        }

        public void DeleteACampaingByName(string campaingName)
        {
            Data.Sql.CampaignData campaign = new Data.Sql.CampaignData();
            try { 
            campaign.DeleteCampaign(campaingName);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                
            }
        }

        public void UpdateAProductInDb(string campaignName, Model.Campaign campaign)
        {
            Data.Sql.CampaignData campaignData = new Data.Sql.CampaignData();
            try
            {
                campaignData.UpdateACampaing(campaignName, campaign);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
             
            }
        }
    }
}
