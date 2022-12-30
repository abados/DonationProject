import axios from "axios";

export const addActivistUser = async (user) => {
  console.log(user);
  await axios.post("http://localhost:7033/api/Activist/ADD", user);
};
