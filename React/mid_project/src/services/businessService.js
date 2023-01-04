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

export const getDonates = async (userEmail) => {
  return await axios
    .get(`http://localhost:7033/api/Business/GETMYPRODUCTS/${userEmail}`)
    .then((response) => {
      return Object.values(response.data);
    })
    .catch((error) => {
      console.log(error);
    });
};

export const searchIDS = async (
  businessID,
  campaignName,
  productName,
  price,
  defaultVariableIS,
  defaultActivist
) => {
  console.log(
    businessID,
    campaignName,
    productName,
    price,
    defaultVariableIS,
    defaultActivist
  );
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
