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
