using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using CampaignProject.DAL;
using NUnit.Framework;

namespace CampaignProject.Entity.Test
{
    public class TestActivistPath
    {
		string UserEmail;
		string answer;

		[SetUp]//first to run
		public void Init()
		{
			UserEmail = "economy.telhai@gmail.com";
			SqlQuery.connectionString = "Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=MidProject;Data Source=localhost\\SQLEXPRESS";
			
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

			

		}


	}
}
