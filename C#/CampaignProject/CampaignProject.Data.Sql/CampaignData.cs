using CampaignProject.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Data.Sql
{
    public class CampaignData
    {
        public Dictionary<string, CampaignProject.Model.Campaign> ReadFromDb(SqlDataReader reader)
        {
            Dictionary<string, CampaignProject.Model.Campaign> CampaignsList = new Dictionary<string, CampaignProject.Model.Campaign>();

            //Clear Hashtable Before Inserting Information From Sql Server
            CampaignsList.Clear();

            while (reader.Read())
            {
                CampaignProject.Model.Campaign campaign = new CampaignProject.Model.Campaign();
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
            return CampaignsList;
        }

        public object SqlQueryToReadFromDB()
        {
            string SqlQuery = "select * from Campaigns";
            object retDict = null;
            retDict = DAL.SqlQuery.getDataFromDB(SqlQuery, ReadFromDb);
            return retDict;
        }




        public void SendSqlQueryToInsertToDB(Model.Campaign newCampaign, string userEmail)
        {
            string uploadNewCampaignQuery = "declare @NonID int\n" +
            "select @NonID = (select id from NonProfits where Email = '"+ userEmail+"')\n"+
             "insert into Campaigns values(@NonID,'"+ newCampaign.campaignName+ "','"+ newCampaign.campaignInfo+ "','"+ newCampaign.campaignHashtag+ "','"+ newCampaign.campaignUrl+"',0)";
                DAL.SqlQuery.Update_Delete_Insert_RowInDB(uploadNewCampaignQuery);

        }


        public void DeleteCampaign(string name)
        {
            string deleteQuery = "delete from Campaigns where CampaignName ='" + name + "'";
            DAL.SqlQuery.Update_Delete_Insert_RowInDB(deleteQuery);

        }
        public void UpdateACampaing(string campaignName, Model.Campaign campaign)
        {


            string updateQuery = "update Campaigns set CampaignName ='" + campaign.campaignName + "', CampaignInfo='" + campaign.campaignInfo + "', CampaignHashtag='" + campaign.campaignHashtag + "', CampaignWebUrl='" + campaign.campaignUrl + "'where CampaignName='" + campaignName + "'";
            DAL.SqlQuery.Update_Delete_Insert_RowInDB(updateQuery);

        }
    }
}
