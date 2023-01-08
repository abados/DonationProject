import axios from "axios";

export const addActivistUser = async (user) => {
  console.log(user);
  await axios.post("http://localhost:7033/api/Activist/ADD", user);
};

export const getOrganizations = async () => {
  return await axios
    .get("http://localhost:7033/api/Activist/GETORGANIZATIONS")
    .then((response) => {
      return Object.values(response.data);
    })
    .catch((error) => {
      console.log(error);
    });
};

export const getUserEarnings = async (uesrEmail) => {
  return await axios
    .get(`http://localhost:7033/api/Activist/GETEARNINGS/${uesrEmail}`)
    .then((response) => {
      return Object.values(response.data);
    })
    .catch((error) => {
      console.log(error);
    });
};

export const makeAPurchase = async (productName, productPrice, userEmail) => {
  return await axios
    .post("http://localhost:7033/api/Activist/PURCHES", {
      variable1: productName,
      variable2: productPrice,
      variable3: userEmail,
    })
    .catch((error) => {
      console.log(error);
    });
};

export const getPurches = async (Identifier) => {
  return await axios
    .get(`http://localhost:7033/api/Activist/GETMYPRODUCTS/${Identifier}`)
    .then((response) => {
      return Object.values(response.data);
    })
    .catch((error) => {
      console.log(error);
    });
};

export const signInForCampaign = async (campaignName, userEmail) => {
  return await axios
    .post("http://localhost:7033/api/Activist/SIGNCAMPAIGN", {
      variable1: campaignName,
      variable2: userEmail,
    })
    .catch((error) => {
      console.log(error);
    });
};
