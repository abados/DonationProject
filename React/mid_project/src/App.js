import React, { useState } from "react";
import { useAuth0 } from "@auth0/auth0-react";
import { Routes, Route } from "react-router-dom";
import "./App.css";
import { NavBarComponent } from "./components/navBar/navBar";
import { LoginPage } from "./components/pages/LoginPage/LoginPage";
import { HomePage } from "./components/pages/HomePage/homePage";
import { UserRoleContext } from "./context/context";
import { ActivistUserForm } from "./components/pages/ActivistUser/addingInfo.Activist.page";
import { BusinessUserForm } from "./components/pages/BusinessUser/addingInfo.Business.page";
import { NonProfitUserForm } from "./components/pages/NonProfitUser/addingInfo.NonProfit.page";
import { OwnerUserForm } from "./components/pages/Owner/addingInfo.Owner.page";

function App() {
  const { isAuthenticated, isLoading } = useAuth0();
  const [RoleContext, setRoleContext] = useState();

  if (isLoading) {
    return <div className="App">Loading</div>;
  } else if (isAuthenticated) {
    return (
      <div className="App">
        <UserRoleContext.Provider value={{ RoleContext, setRoleContext }}>
          <NavBarComponent />
          <Routes>
            <Route path="/" element={<HomePage />}></Route>
            <Route path="/about"></Route>
            <Route path="/contactus"></Route>
            <Route path="/products"></Route>
            <Route path="/profile"></Route>
            <Route
              path="/Activist/addingAInfo"
              element={<ActivistUserForm />}
            ></Route>
            <Route
              path="/Business/addingBInfo"
              element={<BusinessUserForm />}
            ></Route>
            <Route
              path="/NonProfit/addingNInfo"
              element={<NonProfitUserForm />}
            ></Route>
            <Route
              path="/Admin/addingOInfo"
              element={<OwnerUserForm />}
            ></Route>
          </Routes>
        </UserRoleContext.Provider>
      </div>
    );
  } else {
    return <LoginPage />;
  }
}

export default App;
