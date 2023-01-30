using CampaignProject.Model;
using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Data.Sql
{
    public class CampaignData
    {
        public Dictionary<string, Model.Campaign> ReadFromDbIntoDict(SqlDataReader reader)
        {
            Dictionary<string, Model.Campaign> CampaignsList = new Dictionary<string, Model.Campaign>();

            //Clear Hashtable Before Inserting Information From Sql Server
            CampaignsList.Clear();
            try { 
            while (reader.Read())
            {
                Model.Campaign campaign = new Model.Campaign();
                campaign.campaignName = reader.GetString(2);
                campaign.campaignInfo = reader.GetString(3);
                campaign.campaignHashtag = reader.GetString(4);
                campaign.campaignUrl = reader.GetString(5);
                campaign.donationAmount = reader.GetDecimal(6);



                //Cheking If Hashtable contains the key
                if (CampaignsList.ContainsKey(campaign.campaignHashtag))
                {
                    //key already exists
                }
                else
                {
                    //Filling a hashtable
                    CampaignsList.Add(campaign.campaignHashtag, campaign);
                }

            }
            
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return CampaignsList;
        }

        public List<Campaign> ReadFromDbIntoList(SqlDataReader reader)
        {
            List<Campaign> CampaignsList = new List<Campaign>();

            //Clear Hashtable Before Inserting Information From Sql Server
            CampaignsList.Clear();
            try { 
            while (reader.Read())
            {
                Model.Campaign campaign = new Model.Campaign();
                campaign.campaignName = reader.GetString(2);
                campaign.campaignInfo = reader.GetString(3);
                campaign.campaignHashtag = reader.GetString(4);
                campaign.campaignUrl = reader.GetString(5);
                campaign.donationAmount = reader.GetDecimal(6);

                   
                 CampaignsList.Add(campaign);
            }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return CampaignsList;
        }

        public List<ActiveCampaigns> ReadFromActiveCampaignsDbIntoList(SqlDataReader reader)
        {
            List<ActiveCampaigns> CampaignsList = new List<ActiveCampaigns>();

            //Clear Hashtable Before Inserting Information From Sql Server
            CampaignsList.Clear();
            try { 
            while (reader.Read())
            {
                Model.ActiveCampaigns active = new Model.ActiveCampaigns();
                active.campaignName = reader.GetString(2);
                active.campaignHashtag = reader.GetString(3);
                active.activeUserId = reader.GetInt32(4);
                active.ActiveUserName = reader.GetString(5);
                
                CampaignsList.Add(active);
            }
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return CampaignsList;
        }
        public object SqlQueryToReadCampaignsFromDBIntoDict()
        {
            string SqlQuery = "select * from Campaigns";
            object retDict = null;
            try { 
            retDict = DAL.SqlQuery.getDataFromDB(SqlQuery, ReadFromDbIntoDict);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return retDict;
        }

        public object SqlQueryToReadActiveCampaignsFromDBIntoList()
        {
            string SqlQuery = "select * from ActiveCampaigns";
            object retDict = null;
            try { 
            retDict = DAL.SqlQuery.getDataFromDB(SqlQuery, ReadFromActiveCampaignsDbIntoList);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return retDict;
        }

        public object SqlQueryToReadCampaignsFromDBIntoList()
        {
            string SqlQuery = "select * from Campaigns";
            object retDict = null;
            try { 
            retDict = DAL.SqlQuery.getDataFromDB(SqlQuery, ReadFromDbIntoList);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return retDict;
        }

        public object specificCampaigns(string organ)
        {
            string SqlQuery = " select * from Campaigns where NonProfitUserID=(select id from NonProfits where Email =" + "'" + organ + "'" + ")";
            object retDict = null;
            try { 
            retDict = DAL.SqlQuery.getDataFromDB(SqlQuery, ReadFromDbIntoDict);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return retDict;
        }




        public void SendSqlQueryToInsertToDB(Model.Campaign newCampaign, string userEmail)
        {
            string uploadNewCampaignQuery = "declare @NonID int\n" +
            "select @NonID = (select id from NonProfits where Email = '"+ userEmail+"')\n"+
             "insert into Campaigns values(@NonID,'"+ newCampaign.campaignName+ "','"+ newCampaign.campaignInfo+ "','"+ newCampaign.campaignHashtag+ "','"+ newCampaign.campaignUrl+"',0)";
            try { 
                DAL.SqlQuery.Update_Delete_Insert_RowInDB(uploadNewCampaignQuery);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }

        }


        public void DeleteCampaign(string name)
        {
            string deleteQuery = "delete from Campaigns where CampaignName ='" + name + "'";
            try { 
            DAL.SqlQuery.Update_Delete_Insert_RowInDB(deleteQuery);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }

        }
        public void UpdateACampaing(string campaignName, Model.Campaign campaign)
        {


            string updateQuery = "update Campaigns set CampaignName ='" + campaign.campaignName + "', CampaignInfo='" + campaign.campaignInfo + "', CampaignHashtag='" + campaign.campaignHashtag + "', CampaignWebUrl='" + campaign.campaignUrl + "'where CampaignName='" + campaignName + "'";
            try { 
            DAL.SqlQuery.Update_Delete_Insert_RowInDB(updateQuery);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }

        }

        public string getBearer(string Bearer)
        {
            string BearerQuery = "select VALUE from Config where [KEY]='"+ Bearer+"'";
            try { 
            string Key = DAL.SqlQuery.getOneDataFromDBInString(BearerQuery);
                return Key;
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            }
            return null;
        }
    }
}
