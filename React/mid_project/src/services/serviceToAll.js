import axios from "axios";

export const getRole = async (userId) => {
  return await axios
    .get(`http://localhost:7033/api/Campaigns/ROLE/${userId}`)
    .then((response) => {
      return response.data;
    })
    .catch((error) => {
      console.log(error);
    });
};

export const checkIfExsits = async (userEmail, Table) => {
  let url = `http://localhost:7033/api/${Table}/Find/${userEmail}`;
  console.log(url);
  let answer = await axios.get(url);
  return answer.data;
};

export const getSpecificCampaigns = async (organName) => {
  return await axios
    .get(`http://localhost:7033/api/Campaigns/GET/${organName}`)
    .then((response) => {
      return Object.values(response.data);
    })
    .catch((error) => {
      console.log(error);
    });
};
