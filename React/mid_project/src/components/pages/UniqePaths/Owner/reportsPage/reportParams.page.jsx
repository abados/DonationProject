import React, { useState } from "react";
import "../css/reportPage.css";

export const ReportParamsPage = () => {
  // State variables to store the selected values of the dropdown lists
  const [option1, setOption1] = useState("");
  const [option2, setOption2] = useState("");
  const [option3, setOption3] = useState("");

  // An array of options for each dropdown list
  const options1 = ["Product", "Users", "Campaigns"];
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
  };

  const handleSendReport = () => {
    console.log("send report");
    console.log(options1);
    console.log(option2);
    console.log(options3);
    console.log(options2Users);
  };

  return (
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
        {option1 === "Product" && (
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
          onClick={() => handleSendReport}
        >
          Send Request
        </button>
      </div>
    </nav>
  );
};
