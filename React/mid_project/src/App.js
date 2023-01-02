import React, { useState } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import { Routes, Route } from "react-router-dom";
import "./App.css";
import { NavBarComponent } from "./components/navBar/navBar";
import { LoginPage } from "./components/pages/LoginPage/LoginPage";
import { HomePage } from "./components/pages/HomePage/homePage";
import { UserRoleContext } from "./context/context";
import { ActivistUserForm } from "./components/pages/UniqePaths/ActivistUser/addingInfo.Activist.page";
import { BusinessUserForm } from "./components/pages/UniqePaths/BusinessUser/addingInfo.Business.page";
import { NonProfitUserForm } from "./components/pages/UniqePaths/NonProfitUser/SignIn/addingInfo.NonProfit.page";
import { OwnerUserForm } from "./components/pages/UniqePaths/Owner/addingInfo.Owner.page";
import { FormContext } from "./context/context";
import { AfterSighInpage } from "./components/pages/afterSignIn/afterSighIn.page";
import { AddCampaignPage } from "./components/pages/UniqePaths/NonProfitUser/addCampagin/addCampaign.page";
import { CampaingsPage } from "./components/pages/UniqePaths/NonProfitUser/MyCampaings/myCampaing.page";
import { UpadateCampaignPage } from "./components/pages/UniqePaths/NonProfitUser/UpdateACampaing/updateCampaign.page";
import { NewRegisterPage } from "./components/pages/HomePage/newRegister.page";

function App() {
  const { isAuthenticated, isLoading } = useAuth0();
  const [RoleContext, setRoleContext] = useState();
  const [formSubmitted, setFormSubmitted] = useState(false);

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
              <Route path="/newRegister" element={<NewRegisterPage />}></Route>
              <Route
                path="//editCampaing"
                element={<UpadateCampaignPage />}
              ></Route>
              <Route path="/profile"></Route>
              <Route path="/thankYou" element={<AfterSighInpage />}></Route>
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
