import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuth0 } from "@auth0/auth0-react";
import { addCampaign } from "../../../../../services/nonProfitService";
import "./addCampaign.css";

export const AddCampaignPage = () => {
  const [campaignName, setCampaignName] = useState("");
  const [campaignInfo, setCampaignInfo] = useState("");
  const [campaignHashtag, setCampaignHashtag] = useState("");
  const [campaignUrl, setCampaignUrl] = useState("");
  const { user } = useAuth0();

  const navigate = useNavigate();

  const handleSubmit = (event) => {
    event.preventDefault();
    const newCampaing = {
      campaignName,
      campaignInfo,
      campaignHashtag,
      campaignUrl,
    };
    if (
      campaignName === "" ||
      campaignInfo === "" ||
      campaignHashtag === "" ||
      campaignUrl === ""
    ) {
      alert("Please enter all required");
    } else {
      addCampaign(newCampaing, user.email);
      navigate("/");
    }
  };

  return (
    <div className="formContainer">
      <form onSubmit={handleSubmit} className="formCInfo">
        <h1>Add new Campaing</h1>
        <div className="form-group">
          <label className="formLbl" htmlFor="name">
            Campaign Name:
          </label>
          <input
            type="text"
            className="form-control"
            id="name"
            value={campaignName}
            onChange={(event) => setCampaignName(event.target.value)}
          />
        </div>
        <div className="form-group">
          <label className="formLbl" htmlFor="name">
            Campaign Information:
          </label>
          <input
            type="name"
            className="form-control"
            id="name"
            value={campaignInfo}
            onChange={(event) => setCampaignInfo(event.target.value)}
          />
        </div>

        <div className="form-group">
          <label className="formLbl" htmlFor="message">
            Please choose the campaign Hashtag:
          </label>
          <input
            type="text"
            className="form-control"
            id="name"
            value={campaignHashtag}
            onChange={(event) => setCampaignHashtag(event.target.value)}
          />
        </div>
        <div className="form-group">
          <label className="formLbl" htmlFor="message">
            Campaign Url:
          </label>
          <input
            type="text"
            className="form-control"
            id="name"
            value={campaignUrl}
            onChange={(event) => setCampaignUrl(event.target.value)}
          />
        </div>

        <button type="submit" className="btn btn-primary">
          Submit
        </button>
      </form>
    </div>
  );
};
