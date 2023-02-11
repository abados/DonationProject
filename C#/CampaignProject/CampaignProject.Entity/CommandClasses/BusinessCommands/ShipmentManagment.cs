using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity.CommandClasses.BusinessCommands
{
    public class ShipmentManagment : CommandManager, ICommand
    {

        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null)
            {
                try////the business man can see orders that didn't completed, if he psuh "send" we get here to finish them
                {

                    int id;
                    if (int.TryParse(param[0].ToString(), out id))
                    {
                        MainManager.Instance.myLogger.LogEvent("Ship an item : ", LoggingLibrary.LogLevel.Event);
                        MainManager.Instance.Business.SendTheItems(id);
                        return "Success Request";
                    }
                    else
                    {
                        MainManager.Instance.myLogger.LogError("something went wrong", LoggingLibrary.LogLevel.Error);
                        return System.Text.Json.JsonSerializer.Serialize("Faild Request");
                    }


                }
                catch (Exception ex)
                {
                    MainManager.Instance.myLogger.LogException(ex.ToString(), ex);

                    return System.Text.Json.JsonSerializer.Serialize("Faild Request");
                }
            }
            else
            {
                MainManager.Instance.myLogger.LogError("something went wrong", LoggingLibrary.LogLevel.Error);

                return "Faild Request";
            }

        }
    }
}

