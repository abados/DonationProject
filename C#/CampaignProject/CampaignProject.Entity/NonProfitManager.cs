﻿using CampaignProject.Model;
using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity
{
    public class NonProfitManager : BaseEntity
    {
        public NonProfitManager(Logger log) : base(log)
        {
        }

        public string FindTheUser(string UserEmail)
        {
            Data.Sql.NonProfitData NonProfit = new Data.Sql.NonProfitData(Logger);
            try
            {
                return (string)NonProfit.SendSqlQueryToReadFromDBForOneUser(UserEmail);
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
                return null;
            }
}

        public void InsertNewItem(Model.NonProfitUser nonProfit)
        {
            Data.Sql.NonProfitData user = new Data.Sql.NonProfitData(Logger);
            string userType = "NonProfit";
            try { 
            string userID = user.AddNewUser(userType);
            user.SendSqlQueryToInsertToDB(nonProfit, int.Parse(userID));
            }
            catch (Exception ex)
            {
                Logger.LogException(ex.ToString(), ex);
            
            }
        }
    }
}
