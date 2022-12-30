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
