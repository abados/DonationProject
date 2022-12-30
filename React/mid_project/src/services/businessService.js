import axios from "axios";

export const addBusinessUser = async (user) => {
  console.log(user);
  await axios.post("http://localhost:7033/api/Business/ADD", user);
};
