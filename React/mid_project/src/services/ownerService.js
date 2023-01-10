import axios from "axios";
import FileSaver from "file-saver";

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

export const GenerateReport = async (Table, search, File) => {
  try {
    const response = await axios.post(
      "http://localhost:7033/api/Owner/REPORT",
      {
        variable1: Table,
        variable2: search,
        variable3: File,
      }
    );

    const result = Object.values(response.data);
    return result;
  } catch (error) {
    console.log(error);
  }
};

//This code is first making the post request with axios, and wait for the response then using the FileSaver.saveAs method to save the file on the client machine using the binary data of the response and the specified name of the file. It also includes try-catch block to handle any errors that may occur during the request.
//If any exception occurs in the try block, it will be caught in the catch block and you can handle it accordingly.

// export const GenerateReport = async (Table, search, File) => {
//   try {
//     const response = await axios.post(
//       "http://localhost:7033/api/Owner/REPORT",
//       {
//         variable1: Table,
//         variable2: search,
//         variable3: File,
//       }
//       //{ responseType: "blob" }
//     );

//     // await FileSaver.saveAs(response.data, "products.pdf");
//     // console.log(response.data);
//     // console.log(response);
//   } catch (error) {
//     console.log(error);
//   }
// }
