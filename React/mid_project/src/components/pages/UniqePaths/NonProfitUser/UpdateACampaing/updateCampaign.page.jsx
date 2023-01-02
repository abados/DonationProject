import React, { useEffect, useState } from "react";
import { useNavigate, useLocation } from "react-router-dom";
import { updateCampaign } from "../../../../../services/nonProfitService";

export const UpadateCampaignPage = () => {
  const [campaignName, setCampaignName] = useState("");
  const [campaignInfo, setCampaignInfo] = useState("");
  const [campaignHashtag, setCampaignHashtag] = useState("");
  const [campaignUrl, setCampaignUrl] = useState("");

  const navigate = useNavigate();

  const location = useLocation();

  const { campaing } = location.state;

  useEffect(() => {
    setCampaignName(campaing.campaignName);
    setCampaignInfo(campaing.campaignInfo);
    setCampaignHashtag(campaing.campaignHashtag);
    setCampaignUrl(campaing.campaignUrl);
  }, []);

  const handleSubmit = async (event) => {
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
      await updateCampaign(newCampaing, campaing.campaignName);
      navigate("/MyCampagins");
    }
  };

  return (
    <div className="formContainer">
      <form onSubmit={handleSubmit} className="formNInfo">
        <h1>Please fill in you details</h1>
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
          Update Campaign
        </button>
      </form>
    </div>
  );
};
