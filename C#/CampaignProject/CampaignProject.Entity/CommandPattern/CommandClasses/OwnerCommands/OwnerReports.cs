using LoggingLibrary;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity.CommandPattern.CommandClasses.OwnerCommands
{
    public class OwnerReports : CommandManager, ICommand
    {
        public OwnerReports(Logger log) : base(log)
        {
        }
        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[1] != null)
            {
                try
                {
                    dynamic data = JsonConvert.DeserializeObject<JObject>((string)param[1]);
                    string Table = data.Value<string>("variable1");
                    string Search = data.Value<string>("variable2");
                    
                    switch (Table)
                    {
                        case "Products":
                            try
                            {
                                if (Search == "All products")
                                {
                                    return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Product.getAllProductsForReport());
                                }
                                else if (Search == "Bought products")
                                {

                                    return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Product.getBoughtProductsFromDB());
                                }
                                else
                                { //Bought and not deliverd products

                                    return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Product.getBoughtAndNotDeliverdProductsFromDB());
                                }
                            }

                            catch (Exception ex)
                            {
                                Logger.LogException(ex.ToString(), ex);
                                return System.Text.Json.JsonSerializer.Serialize("Faild Request");
                            }
                           
                        case "Users":
                            try
                            {
                                if (Search == "Business users")
                                {
                                    return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Owner.getBusinessUsers());
                                }
                                else if (Search == "Nonprofits users")
                                {
                                    return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Owner.getNonProfitUsers());
                                }
                                else if (Search == "Activists users")
                                {
                                    return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Owner.getActivistUsers());
                                }
                                else
                                {
                                    return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Owner.UsersEarningsSum());
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.LogException(ex.ToString(), ex);
                                return System.Text.Json.JsonSerializer.Serialize("Faild Request");
                            }
                           
                        case "Campaigns":
                            try
                            {
                                if (Search == "All campaigns")
                                {
                                    return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Campaign.getCampaignsFromDBInList());
                                }
                                else if (Search == "Tweets")
                                {
                                    return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Owner.getTweets());
                                }
                                else
                                {
                                    return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Owner.bringDataAboutCampaignsActivity());
                                }

                            }
                            catch (Exception ex)
                            {
                                Logger.LogException(ex.ToString(), ex);
                                return System.Text.Json.JsonSerializer.Serialize("Faild Request");
                            }
                           
                        default:
                            Logger.LogError("Invalid choice.", LogLevel.Error);
                            return System.Text.Json.JsonSerializer.Serialize("Invalid choice.");
                           
                    }


                }
                catch (Exception ex)
                {
                    Logger.LogException(ex.ToString(), ex);
                    return System.Text.Json.JsonSerializer.Serialize("Faild Request");
                }
            }
            else
            {
                Logger.LogError("ID Parameter was not found in GetCampaignID class", LoggingLibrary.LogLevel.Error);

                return System.Text.Json.JsonSerializer.Serialize("Faild Request");
            }

        }
    }
}

