using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CampaignProject.Entity.CommandClasses.BusinessCommands
{
    public class DeleteProduct : CommandManager, ICommand
    {

        public object ExecuteCommand(params object[] param) // param,param2, requestBody
        {
            if (param[1] != null)
            {
                try//the user can see only products that are available and didnt ordered
                {
                    MainManager.Instance.myLogger.LogEvent("Delete a product by the Business Man: ", LoggingLibrary.LogLevel.Event);

                    Model.Product productToDelete = new Model.Product();
                    productToDelete = System.Text.Json.JsonSerializer.Deserialize<Model.Product>((string)param[1]);
                    MainManager.Instance.Product.DeleteAProduct(productToDelete.productName, productToDelete.businessID, productToDelete.campaignID);

                   
                    string answer = MainManager.Instance.Business.FindTheUser((string)param[0]);
                    return System.Text.Json.JsonSerializer.Serialize(answer);


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

