using LoggingLibrary;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity.CommandClasses.BusinessCommands
{
    public class GetProductsIdonated : CommandManager, ICommand
    {
        public GetProductsIdonated(Logger log) : base(log)
        {
        }

        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[0] != null)
            {
                Logger.LogEvent("Bring Donated products: ", LoggingLibrary.LogLevel.Event);
                try////get products of the specific Business user
                {
                    string Identifier = (string)param[0];
                   

                    if (Identifier.Contains("@") && param[1] == "" && param[2]==null)
                    {//if we came from a business user path and we send business data
                        var id = MainManager.Instance.Business.getIDS(Identifier, "");
                        return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Product.getUnBoughtProductsOfSpecificBusinessFromDB(int.Parse(id[0])));
                    }
                    else if (param[2].Equals("trackShipment"))
                    {//for shipment tracking page
                        var id = MainManager.Instance.Business.getIDS(Identifier, "");
                        return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Product.getBoughtProductsOfSpecificBusinessFromDB(int.Parse(id[0])));
                    }
                    else
                    {//if we came from a activist user path and we send campaign data
                        return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Product.getProductsOfSpecificCampaignFromDB(Identifier));
                    }

                    
                }
                catch (Exception ex)
                {
                    Logger.LogException(ex.ToString(), ex);
                    return "Faild Request";
                }
            }
            else
            {
                Logger.LogError("something went wrong", LoggingLibrary.LogLevel.Error);

                return "Faild Request";
            }

        }
    }
}

