import axios from "axios";

export const addNonProfitUser = async (user) => {
  console.log(user);
  await axios.post("http://localhost:7033/api/NonProfit/ADD", user);
};

export const addCampaign = async (campaign, userEamil) => {
  console.log(campaign);
  console.log(userEamil);
  await axios.post(
    `http://localhost:7033/api/Campaigns/ADD/${userEamil}`,
    campaign
  );
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

export const deleteCampaignFromDb = async (campaignName) => {
  try {
    console.log(campaignName);
    let endpoint = `http://localhost:7033/api/Campaigns/DELETE/${campaignName}`;
    console.log(endpoint);
    await axios.delete(endpoint);
  } catch (error) {
    console.error(error);
  }
};

export const updateCampaign = async (campaignUpdate, campaignName) => {
  await axios.post(
    `http://localhost:7033/api/Campaigns/UPDATE/${campaignName}`,
    campaignUpdate
  );
};
