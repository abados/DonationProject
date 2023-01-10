import React, { useState } from "react";
import { GenerateReport } from "../../../../../services/ownerService";
import "../css/reportPage.css";
import { CampaignReport } from "./reportsOptions/campaignReport";
import { ProductReport } from "./reportsOptions/productReport";
import { UsersReport } from "./reportsOptions/usersReport";

export const ReportParamsPage = () => {
  // State variables to store the selected values of the dropdown lists
  const [option1, setOption1] = useState("");
  const [option2, setOption2] = useState("");
  const [option3, setOption3] = useState("");
  const [listOfData, setListOfData] = useState([]);
  const [showOptions, setShowOptions] = useState(false);

  // An array of options for each dropdown list
  const options1 = ["Products", "Users", "Campaigns"];
  const options2Product = [
    "All products",
    "Bought products",
    "Bought and not delivered",
  ];
  const options2Users = [
    "Business users",
    "Nonprofits users",
    "Activists users",
    "Sum of the users Money",
  ];
  const options2Campaigns = ["All campaigns", "Active campaigns"];
  const options3 = ["Table", "PDF", "CVS"];

  // A function to handle changes to the dropdown lists
  const handleChange = (event) => {
    if (event.target.name === "option1") {
      setOption1(event.target.value);
    } else if (event.target.name === "option2") {
      setOption2(event.target.value);
    } else if (event.target.name === "option3") {
      setOption3(event.target.value);
    }
    if (showOptions) setShowOptions(false);

    console.log(option1);
    console.log(option2);
    console.log(option3);
  };

  const handleSendReport = async () => {
    console.log("send report");

    var dataArray = await GenerateReport(option1, option2, option3);
    setListOfData(dataArray);

    setShowOptions(!showOptions);
  };

  return (
    <div className="reportPage">
      <nav className="reportNav">
        <div className="reportHeader">
          {/* Dropdown list for option1 */}
          <label className="lblReport" htmlFor="option1">
            Chose Table Subject:
          </label>
          <select
            className="selectReport"
            name="option1"
            value={option1}
            onChange={handleChange}
          >
            <option value="" disabled>
              Select an option
            </option>
            {options1.map((option) => (
              <option key={option} value={option}>
                {option}
              </option>
            ))}
          </select>

          {/* Conditional rendering for dropdown list 2 */}
          {option1 === "Products" && (
            <div>
              <label className="lblReport" htmlFor="option2">
                Show:
              </label>
              <select
                className="selectReport"
                name="option2"
                value={option2}
                onChange={handleChange}
              >
                <option value="" disabled>
                  Select an option
                </option>
                {options2Product.map((option) => (
                  <option key={option} value={option}>
                    {option}
                  </option>
                ))}
              </select>
            </div>
          )}
          {option1 === "Users" && (
            <div>
              <label className="lblReport" htmlFor="option2">
                Show:
              </label>
              <select
                className="selectReport"
                name="option2"
                value={option2}
                onChange={handleChange}
              >
                <option value="" disabled>
                  Select an option
                </option>
                {options2Users.map((option) => (
                  <option key={option} value={option}>
                    {option}
                  </option>
                ))}
              </select>
            </div>
          )}
          {option1 === "Campaigns" && (
            <div>
              <label className="lblReport" htmlFor="option2">
                Show:
              </label>
              <select
                className="selectReport"
                name="option2"
                value={option2}
                onChange={handleChange}
              >
                <option value="" disabled>
                  Select an option
                </option>
                {options2Campaigns.map((option) => (
                  <option key={option} value={option}>
                    {option}
                  </option>
                ))}
              </select>
            </div>
          )}
          <div>
            <label className="lblReport" htmlFor="option3">
              Generate:
            </label>
            <select
              className="selectReport"
              name="option3"
              value={option3}
              onChange={handleChange}
            >
              <option value="" disabled>
                Select an option
              </option>
              {options3.map((option) => (
                <option key={option} value={option}>
                  {option}
                </option>
              ))}
            </select>
          </div>
          <button
            className="btn btn-primary btnReport"
            onClick={() => handleSendReport()}
          >
            Send Request
          </button>
        </div>
      </nav>
      <body>
        {showOptions && (
          <div>
            {option1 === "Products" && (
              <ProductReport Productlist={listOfData} />
            )}
            {option1 === "Users" && <UsersReport Userslist={listOfData} />}
            {option1 === "Campaigns" && (
              <CampaignReport Campaignlist={listOfData} result={option2} />
            )}
          </div>
        )}
      </body>
    </div>
  );
};
