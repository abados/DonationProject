using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CampaignProject.DAL;
using CampaignProject.Model;
using LoggingLibrary;
using NUnit.Framework;

namespace CampaignProject.Entity.Test
{
    public class TestActivistPath
    {
		string UserEmail;
		string answer;
		string productName;
		decimal productPrice;
		Model.ActivistUser Activist = new Model.ActivistUser();
		public Logger myLogger { get; set; }

		[SetUp]//first to run
		public void Init()
		{
			Logger.LogItemsQueue = new Queue<LogItem>();
			myLogger = new Logger("Console");

			UserEmail = "economy.telhai@gmail.com";
			productName = "Car";
			productPrice = 500;
			SqlQuery.connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=MidProject;Data Source=localhost\\SQLEXPRESS";

			Activist.fullName = "John Doe";
			Activist.email = "johndoe@example.com";
			Activist.cellPhone = "123-456-7890";
			Activist.address = "123 Main St, Anytown, USA";
			Activist.TwitterAcount = "@johndoe";
			Activist.Earnings = 1000;

			

	}

		[Test, Order(1), Category("Find Activist")]
		public void RunFindTest()
		{
			//check if it can find the user
			 answer = MainManager.Instance.Activist.FindTheUser(UserEmail);
			Assert.True(answer== "true", "found the user - working");

			//check if it can find that the user dont exsits
			 answer = MainManager.Instance.Activist.FindTheUser("aaa");
			Assert.True(answer == "false", "found that user dont exsits - working");

		}

		[Test, Order(2), Category("Add Activist")]
		public void RunAddUserTest()
		{

			//check if it can find that the user dont exsits before the adding
			answer = MainManager.Instance.Activist.FindTheUser(Activist.email);
			Assert.True(answer == "false", "found that user dont exsits - working");

			//adds the user
			MainManager.Instance.Activist.InsertNewMember(Activist);

			//check if it can find the user after the adding
			answer = MainManager.Instance.Activist.FindTheUser(Activist.email);
			Assert.True(answer == "true", "found the user - working");


			//deleting the tested member
			SqlQuery.Update_Delete_Insert_RowInDB("delete from Activists where Email=" + "'" + Activist.email + "'");

			//check if deleted 
			answer = MainManager.Instance.Activist.FindTheUser(Activist.email);
			Assert.True(answer == "false", "found that user dont exsits - working");

		}

		[Test, Order(3), Category("Find Activist earnings")]
		public void RunGetEarningsByIDFromDBTest()
		{
			answer = MainManager.Instance.Activist.getEarningsByIDFromDB(UserEmail);
			decimal earnings;
			bool isDecimal = decimal.TryParse(answer.ToString(), out earnings);
			Assert.IsTrue(isDecimal && earnings >= 0m);

			
		}
		public List<NonProfitUser> nonProfitsList = new List<NonProfitUser>();

		[Test, Order(4), Category("bring the organization list")]
		public void RunGetNonProfitListFromDBTest()
		{
			nonProfitsList = MainManager.Instance.Activist.getNonProfitListFromDB();
			Assert.NotNull(nonProfitsList);
		}


		public List<ActiveCampaigns> ActiveCampaignsList = new List<ActiveCampaigns>();
		[Test, Order(5), Category("bring the Active Campaigns List of the user")]
		public void RunGetActiveCampaignsOfUserFromDBTest()
		{
			ActiveCampaignsList = MainManager.Instance.Activist.getActiveCampaignsOfUserFromDB(UserEmail);
			Assert.NotNull(ActiveCampaignsList);
		}

		[Test, Order(6), Category("purches a product")]
		public void RunMakeAPurchesTest()
		{
			//check that the item isn't mark as bought
			answer = SqlQuery.getOneDataFromDBInString("select IsBought from Products where ProductName =" + "'" + productName + "'");

			Assert.True(answer == "False", "found that  item isn't mark as bought - working");

			//make the purches
			MainManager.Instance.Activist.makeAPurchesChanges(productName, productPrice, UserEmail);


			//now check that the item mark as bought
			answer = SqlQuery.getOneDataFromDBInString("select IsBought from Products where ProductName =" + "'" + productName + "'");

			Assert.True(answer == "True", "found that  item  mark as bought - working");

			//make the product available again
			SqlQuery.Update_Delete_Insert_RowInDB("update Products set IsBought=0,ActivistBuyerID=0 where ProductName=" + "'" + productName + "'");

			//check that the item isn't mark as bought
			answer = SqlQuery.getOneDataFromDBInString("select IsBought from Products where ProductName =" + "'" + productName + "'");

			Assert.True(answer == "False", "found that  item isn't mark as bought - working");
		}
	}
}
