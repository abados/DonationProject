import axios from "axios";

export const checkIfExsits2 = async (userEmail) => {
  let url = `http://localhost:7033/api/Owner/Find/${userEmail}`;
  let answer = await axios.get(url);
  return answer.data;
};

export const addOwnerUser = async (user) => {
  console.log(user);
  await axios.post("http://localhost:7033/api/Owner/ADD", user);
};

export const CheckTweets = async (user) => {
  console.log(user);
  await axios.post("http://localhost:7033/api/Owner/TWEET");
};

export const GenerateReport = async (Table, search) => {
  try {
    const response = await axios.post(
      "http://localhost:7033/api/Owner/REPORT",
      {
        variable1: Table,
        variable2: search,
      }
    );

    const result = Object.values(response.data);
    return result;
  } catch (error) {
    console.log(error);
  }
};

