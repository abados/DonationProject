import axios from "axios";

export const addNonProfitUser = async (user) => {
  console.log(user);
  await axios.post("http://localhost:7033/api/NonProfit/ADD", user);
};
