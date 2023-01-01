using CampaignProject.Model;
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
        public Dictionary<string, Campaign> CampaignsList = new Dictionary<string, Campaign>();

        public Dictionary<string, Campaign> getCampaignsFromDB()
        {
            Data.Sql.CampaignData campaign = new Data.Sql.CampaignData();
            CampaignsList = (Dictionary<string, Campaign>)campaign.SqlQueryToReadFromDB();
            return CampaignsList;
        }

        public void SendNewInputToDataLayer(Model.Campaign newCampaign, string uesrEmail)
        {
            Data.Sql.CampaignData campaign = new Data.Sql.CampaignData();
            campaign.SendSqlQueryToInsertToDB(newCampaign, uesrEmail);

        }

        public void DeleteACampaingByName(string campaingName)
        {
            Data.Sql.CampaignData campaign = new Data.Sql.CampaignData();
            campaign.DeleteCampaign(campaingName);
        }

        public void UpdateAProductInDb(string campaignName, Model.Campaign campaign)
        {
            Data.Sql.CampaignData campaignData = new Data.Sql.CampaignData();
            campaignData.UpdateACampaing(campaignName, campaign);
        }
    }
}
