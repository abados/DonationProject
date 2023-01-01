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
  let answer = await axios.get(url);
  return answer.data;
};
