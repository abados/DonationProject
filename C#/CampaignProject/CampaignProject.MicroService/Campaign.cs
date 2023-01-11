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
                case "ADD":
                    try { 
                    Model.Campaign campaign = new Model.Campaign();
                    campaign = System.Text.Json.JsonSerializer.Deserialize<Model.Campaign>(req.Body);
                    MainManager.Instance.Campaign.SendNewInputToDataLayer(campaign, Identifier);
                    }
                    catch(Exception ex)
                    {
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }
                    break;
                case "GET":
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
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }
                    break;
                case "UPDATE":
                    try
                    {

                        requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                        Model.Campaign campaignTOupdate = System.Text.Json.JsonSerializer.Deserialize<Model.Campaign>(requestBody);
                        MainManager.Instance.Campaign.UpdateAProductInDb(Identifier, campaignTOupdate);

                    }
                    catch (Exception ex)
                    {
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }

                    break;
                case "DELETE":
                    try { 
                    MainManager.Instance.Campaign.DeleteACampaingByName(Identifier);
                    }
                    catch(Exception ex)
                    {
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }
                    break;
                case "ROLE":
                    try { 
                    var urlGetRole = $"https://dev-qjf7hgqeu16fymem.us.auth0.com/api/v2/users/{Identifier}/roles";
                    var client = new RestClient(urlGetRole);
                    var request = new RestRequest("",Method.Get);

                        request.AddHeader("authorization",""+MainManager.Instance.Campaign.GetAuth0Bearer());
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
                        Logger.Log(ex.ToString(), LoggingLibrary.LogLevel.Error);
                    }

                    break;

                default:
                    break;

            }

            return new OkObjectResult("not reaching");
        }
    }
}
