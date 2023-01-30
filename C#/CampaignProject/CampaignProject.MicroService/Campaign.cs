using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Reflection;
using RestSharp;
using Newtonsoft.Json.Linq;
using CampaignProject.Entity;
using LoggingLibrary;

namespace CampaignProject.MicroService
{
    public static class Campaign
    {
        [FunctionName("ProLobby")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", "post","delete", Route = "Campaigns/{action}/{Identifier?}")] HttpRequest req,
            string action, string Identifier, ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");
            string requestBody;
            switch (action)
            {
                case "ADD"://adding a new campaign to the campaigns table 
                    Logger.LogEvent("adding campaign to the DB ", LoggingLibrary.LogLevel.Event);
                    try { 
                    Model.Campaign campaign = new Model.Campaign();
                    campaign = System.Text.Json.JsonSerializer.Deserialize<Model.Campaign>(req.Body);
                    MainManager.Instance.Campaign.InsertNewItem(campaign, Identifier);
                    }
                    catch(Exception ex)
                    {
                        Logger.LogException(ex.ToString(), ex);
                    }
                    break;
                case "GET"://get All/Specific Campaign list
                    Logger.LogEvent("get All/Specific Campaign list from the DB ", LoggingLibrary.LogLevel.Event);
                    try { 
                    if(Identifier==null)
                    { 
                    return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Campaign.getCampaignsFromDBInDIctionary()));
                    }
                    else
                    {
                        return new OkObjectResult(System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Campaign.getSpecificCampaignsFromDB(Identifier)));
                    }
                    }
                    catch(Exception ex)
                    {
                        Logger.LogException(ex.ToString(), ex);
                    }
                    break;
                case "UPDATE"://Update a campaign
                    Logger.LogEvent("Update a campaign called ", LoggingLibrary.LogLevel.Event);
                    try
                    {

                        requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                        Model.Campaign campaignTOupdate = System.Text.Json.JsonSerializer.Deserialize<Model.Campaign>(requestBody);
                        MainManager.Instance.Campaign.UpdateAProductInDb(Identifier, campaignTOupdate);

                    }
                    catch (Exception ex)
                    {
                        Logger.LogException(ex.ToString(), ex);
                    }

                    break;
                case "DELETE"://Delete a campaign(only if it now Active)
                    Logger.LogEvent("delete a campaign called ", LoggingLibrary.LogLevel.Event);
                    try { 
                    MainManager.Instance.Campaign.DeleteACampaingByName(Identifier);
                    }
                    catch(Exception ex)
                    {
                        Logger.LogException(ex.ToString(), ex);
                    }
                    break;
                case "ROLE"://Chekcing the user Role from AUTH0
                    Logger.LogEvent("search a role in auth0 ", LoggingLibrary.LogLevel.Event);
                    try { 
                    var urlGetRole = $"https://dev-qjf7hgqeu16fymem.us.auth0.com/api/v2/users/{Identifier}/roles";
                    var client = new RestClient(urlGetRole);
                    var request = new RestRequest("",Method.Get);

                        request.AddHeader("authorization",""+MainManager.Instance.Campaign.GetBearer("Bearer"));
                        var response = client.Execute(request);

                    if(response.IsSuccessful)   
                    {
                        var Json = JArray.Parse(response.Content);
                        return new OkObjectResult(Json);
                    }
                    else
                    {
                        return new NotFoundResult();
                    }
                    }
                    catch (Exception ex)
                    {
                        Logger.LogException(ex.ToString(), ex);
                    }

                    break;

                default:
                    break;

            }

            return new OkObjectResult("not reaching");
        }
    }
}
