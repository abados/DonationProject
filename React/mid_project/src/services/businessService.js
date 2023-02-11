import axios from "axios";

export const addBusinessUser = async (user) => {
  console.log(user);
  await axios.post("http://localhost:7033/api/Business/ADD", user);
};

export const getCampaings = async () => {
  return await axios
    .get("http://localhost:7033/api/Campaigns/GET")
    .then((response) => {
      return Object.values(response.data);
    })
    .catch((error) => {
      console.log(error);
    });
};

export const getActivist = async (allOrNot) => {
  return await axios
    .get(`http://localhost:7033/api/Business/GET_ACTIVIST/${allOrNot}`)
    .then((response) => {
      return Object.values(response.data);
    })
    .catch((error) => {
      console.log(error);
    });
};

export const getDonates = async (Identifier, shipment) => {
  return await axios
    .get(
      `http://localhost:7033/api/Business/GETMYPRODUCTS/${Identifier}/${shipment}`
    )
    .then((response) => {
      console.log("function success");
      return Object.values(response.data);
    })
    .catch((error) => {
      console.log("function failed");
      console.log(error);
    });
};

export const uploadProduct = async (
  businessID,
  campaignName,
  productName,
  price,
  defaultVariableIS,
  defaultActivist
) => {
  return await axios
    .post("http://localhost:7033/api/Business/UPLOADPRODUCT", {
      variable1: businessID,
      variable2: campaignName,
      variable3: productName,
      variable4: price,
      variable5: defaultVariableIS,
      variable6: defaultVariableIS,
      variable7: defaultActivist,
    })
    .catch((error) => {
      console.log(error);
    });
};

export const deleteProductFromDb = async (product) => {
  try {
    console.log(product);
    let endpoint = "http://localhost:7033/api/Business/DELETEAPRODUCT";
    console.log(endpoint, product);
    await axios.delete(endpoint, { data: product });
  } catch (error) {
    console.error(error);
  }
};

export const sendTheItem = async (productID) => {
  return await axios
    .post(`http://localhost:7033/api/Business/SHIPIT/${productID}`)
    .catch((error) => {
      console.log(error);
    });
};
