using CampaignProject.Model;
using LoggingLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity.CommandPattern.CommandClasses.CommonCommands
{
    public class AddUser : CommandManager, ICommand
    {
        public AddUser(Logger log) : base(log)
        {
        }
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[1] != null)
            {
                Logger.LogEvent("adding user to the DB: ", LoggingLibrary.LogLevel.Event);
                try//adding the user to the users table and to Business table
                {
    

                    if (param[3].Equals("Activist"))
                    {
                        Model.ActivistUser Activist = new Model.ActivistUser();
                        Activist = System.Text.Json.JsonSerializer.Deserialize<Model.ActivistUser>((string)param[1]);
                        MainManager.Instance.Activist.InsertNewMember(Activist);
                    }
                    else if (param[3].Equals("NonProfit"))
                    {
                        NonProfitUser NonProfit = new NonProfitUser();
                        NonProfit = System.Text.Json.JsonSerializer.Deserialize<NonProfitUser>((string)param[1]);
                        MainManager.Instance.NonProfit.InsertNewItem(NonProfit);
                    }
                    else if (param[3].Equals("Business"))
                    {
                        Model.BusinessUser Business = new Model.BusinessUser();
                        Business = System.Text.Json.JsonSerializer.Deserialize<Model.BusinessUser>((string)param[1]);
                        MainManager.Instance.Business.InsertNewMember(Business);
                    }
                    else
                    {
                        Model.Owner owner = new Model.Owner();
                        owner = System.Text.Json.JsonSerializer.Deserialize<Model.Owner>((string)param[1]);
                        MainManager.Instance.Owner.InsertNewItem(owner);
                    }

                    return System.Text.Json.JsonSerializer.Serialize("Succes Request");
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex.ToString(), ex);
                    return System.Text.Json.JsonSerializer.Serialize("Faild Request");
                }
            }
            else
            {
                Logger.LogError("something went wrong", LoggingLibrary.LogLevel.Error);

                return System.Text.Json.JsonSerializer.Serialize("Faild Request");
            }

        }
    }
}
