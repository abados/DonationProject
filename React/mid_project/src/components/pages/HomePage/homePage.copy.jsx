// import React, { useEffect, useState } from "react";
// import "./homePage.css";
// import { useAuth0 } from "@auth0/auth0-react";
// import { checkIfExsits } from "../../../services/serviceToAll";
// import { useNavigate } from "react-router-dom";

// export const HomePage = () => {
//   const { user, isLoading } = useAuth0();
//   const navigate = useNavigate();
//   const [isExsits, setIsExsits] = useState("");

//   const funct = async () => {
//     await setIsExsits(await checkIfExsits(user.email, "NonProfit"));
//     console.log(isExsits);
//   };

//   useEffect(() => {
//     funct();
//   }, []);

//   if (isLoading) {
//     return <div>Loading...</div>;
//   } else if (isExsits === "false") {
//     navigate("/NonProfit/addingNInfo");
//   } else {
//     return (
//       <div>
//         <h1>Welcome to Home Page</h1>
//         <p>
//           Lorem ipsum dolor sit amet consectetur adipisicing elit. Repellat
//           quisquam qui nostrum. Qui eum amet ullam, reprehenderit adipisci
//           laudantium perspiciatis?
//         </p>
//       </div>
//     );
//   }
// };
