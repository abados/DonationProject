using CampaignProject.Model;
using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity
{
    public class CampaignManager : BaseEntity
    {
        public CampaignManager(Logger log) : base(log)
        {
        }
        //Global Dictionary
        public Dictionary<string, Campaign> CampaignsDict = new Dictionary<string, Campaign>();

        public List<Campaign> CampaignsList = new List<Campaign>();

        public List<ActiveCampaigns> ActiveCampaignsList = new List<ActiveCampaigns>();

     

        public Dictionary<string, Campaign> getCampaignsFromDBInDIctionary()
        {
            Data.Sql.CampaignData campaign = new Data.Sql.CampaignData(Logger);
            try {
                CampaignsDict = (Dictionary<string, Campaign>)campaign.SqlQueryToReadCampaignsFromDBIntoDict();
                return CampaignsDict;
            }
            catch (Exception ex)
            {
                
                Logger.LogException(ex.ToString(), ex);
                return null;
            }
           
        }

        public List<Campaign> getCampaignsFromDBInList()
        {
            Data.Sql.CampaignData campaign = new Data.Sql.CampaignData(Logger);
            try
            {
                CampaignsList = (List<Campaign>)campaign.SqlQueryToReadCampaignsFromDBIntoList();
                return CampaignsList;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
                return null;
            }

        }


        public List<ActiveCampaigns> getActiveCampaignsFromDBInList()
        {
            Data.Sql.CampaignData campaign = new Data.Sql.CampaignData(Logger);
            try
            {
                ActiveCampaignsList = (List<ActiveCampaigns>)campaign.SqlQueryToReadActiveCampaignsFromDBIntoList();
                return ActiveCampaignsList;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
                return null;
            }

        }

        public Dictionary<string, Campaign> getSpecificCampaignsFromDB(string organizationEmail)
        {
            Data.Sql.CampaignData campaign = new Data.Sql.CampaignData(Logger);
            try {
                CampaignsDict = (Dictionary<string, Campaign>)campaign.specificCampaigns(organizationEmail); ;
            return CampaignsDict;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
                return null;
            }
        }

        public void InsertNewItem(Model.Campaign newCampaign, string uesrEmail)
        {
            Data.Sql.CampaignData campaign = new Data.Sql.CampaignData(Logger);
            try
            {
                campaign.SendSqlQueryToInsertToDB(newCampaign, uesrEmail);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
    
            }
        }

        public void DeleteACampaingByName(string campaingName)
        {
            Data.Sql.CampaignData campaign = new Data.Sql.CampaignData(Logger);
            try { 
            campaign.DeleteCampaign(campaingName);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
                
            }
        }

        public void UpdateAProductInDb(string campaignName, Model.Campaign campaign)
        {
            Data.Sql.CampaignData campaignData = new Data.Sql.CampaignData(Logger);
            try
            {
                campaignData.UpdateACampaing(campaignName, campaign);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
             
            }
        }

        public string GetBearer(string Bearer)
        {
            Data.Sql.CampaignData campaignData = new Data.Sql.CampaignData(Logger);
            try { 
            return campaignData.getBearer(Bearer);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return null;
        }
    }
}
