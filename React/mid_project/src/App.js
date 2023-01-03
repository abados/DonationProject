import React, { useEffect, useState } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import { Routes, Route } from "react-router-dom";
import "./App.css";
import { NavBarComponent } from "./components/navBar/navBar";
import { LoginPage } from "./components/pages/LoginPage/LoginPage";
import { HomePage } from "./components/pages/HomePage/homePage";
import { UserRoleContext } from "./context/context";
import { ActivistUserForm } from "./components/pages/UniqePaths/ActivistUser/addingInfo.Activist.page";
import { BusinessUserForm } from "./components/pages/UniqePaths/BusinessUser/signIn/addingInfo.Business.page";
import { NonProfitUserForm } from "./components/pages/UniqePaths/NonProfitUser/SignIn/addingInfo.NonProfit.page";
import { OwnerUserForm } from "./components/pages/UniqePaths/Owner/addingInfo.Owner.page";
import { FormContext } from "./context/context";
import { AfterSighInpage } from "./components/pages/afterSignIn/afterSighIn.page";
import { AddCampaignPage } from "./components/pages/UniqePaths/NonProfitUser/addCampagin/addCampaign.page";
import { CampaingsPage } from "./components/pages/UniqePaths/NonProfitUser/MyCampaings/myCampaing.page";
import { UpadateCampaignPage } from "./components/pages/UniqePaths/NonProfitUser/UpdateACampaing/updateCampaign.page";
import { NewRegisterPage } from "./components/pages/HomePage/newRegister.page";
import { AddProductPage } from "./components/pages/UniqePaths/BusinessUser/addProduct/addProduct.page";
import { checkIfExsits, getRole } from "./services/serviceToAll";
import { BusinessCampaignPage } from "./components/pages/UniqePaths/BusinessUser/allOrganizations/business.campaign.page";

function App() {
  const { isAuthenticated, isLoading, user } = useAuth0();
  const [RoleContext, setRoleContext] = useState();
  const [formSubmitted, setFormSubmitted] = useState(false);

  const checkingExsits = async () => {
    if (isAuthenticated) {
      var role = await getRole(user.sub);
      setRoleContext(role[0].name);
      let result = await checkIfExsits(user.email, role[0].name);
      setFormSubmitted(result === "true");
    }
  };

  useEffect(() => {
    checkingExsits();
  }, [isAuthenticated]);

  if (isLoading) {
    return <div className="App">Loading</div>;
  } else if (isAuthenticated) {
    return (
      <div className="App">
        <FormContext.Provider value={{ formSubmitted, setFormSubmitted }}>
          <UserRoleContext.Provider value={{ RoleContext, setRoleContext }}>
            <NavBarComponent onFormSubmit={formSubmitted} />
            <Routes>
              <Route path="/" element={<HomePage />}></Route>
              <Route path="/about"></Route>
              <Route path="/contactus"></Route>
              <Route
                path="/business/Campaigns"
                element={<BusinessCampaignPage />}
              ></Route>
              <Route path="/newRegister" element={<NewRegisterPage />}></Route>
              <Route
                path="//editCampaing"
                element={<UpadateCampaignPage />}
              ></Route>
              <Route path="/profile"></Route>
              <Route path="/thankYou" element={<AfterSighInpage />}></Route>
              <Route path="/donateProduct" element={<AddProductPage />}></Route>
              <Route path="/addCampagin" element={<AddCampaignPage />}></Route>
              <Route path="/MyCampagins" element={<CampaingsPage />}></Route>
              <Route
                path="/Activist/addingAInfo"
                element={<ActivistUserForm onFormSubmit={setFormSubmitted} />}
              ></Route>
              <Route
                path="/Business/addingBInfo"
                element={<BusinessUserForm onFormSubmit={setFormSubmitted} />}
              ></Route>
              <Route
                path="/NonProfit/addingNInfo"
                element={<NonProfitUserForm onFormSubmit={setFormSubmitted} />}
              ></Route>
              <Route
                path="/Admin/addingOInfo"
                element={<OwnerUserForm onFormSubmit={setFormSubmitted} />}
              ></Route>
            </Routes>
          </UserRoleContext.Provider>
        </FormContext.Provider>
      </div>
    );
  } else {
    return <LoginPage />;
  }
}

export default App;
