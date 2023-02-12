using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoggingLibrary;

namespace CampaignProject.Entity.CommandClasses.BusinessCommands
{
    public class UpluadNewProduct : CommandManager, ICommand
    {
        public UpluadNewProduct(Logger log) : base(log)
        {
        }

        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[1] != null)
            {
                try//upload a new product from a specific user to a specific Campaign
                {
                    Logger.LogEvent("Upload a new item: ", LoggingLibrary.LogLevel.Event);

                    dynamic data = JsonConvert.DeserializeObject<JObject>((string)param[1]);
                    string EmailToSearch = data.Value<string>("variable1");
                    string CampaignToSearch = data.Value<string>("variable2");

                    string[] ids = MainManager.Instance.Business.getIDS(EmailToSearch, CampaignToSearch);


                    Model.Product product = new Model.Product();
                    product.businessID = int.Parse(ids[0]);
                    product.campaignID = int.Parse(ids[1]);
                    product.productName = data.Value<string>("variable3");
                    string priceToPrase = data.Value<string>("variable4");
                    product.price = decimal.Parse(priceToPrase);

                    product.IsBought = data.Value<bool>("variable5");
                    product.IsDelivered = data.Value<bool>("variable6");
                    product.ActivistBuyerID = data.Value<int>("variable7");

                    MainManager.Instance.Product.InsertNewItem(product);

                    return System.Text.Json.JsonSerializer.Serialize(MainManager.Instance.Business.getIDS(EmailToSearch, CampaignToSearch));

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

                return "Faild Request";
            }

        }
    }
}