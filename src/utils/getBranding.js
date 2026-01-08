import branding from "../config/brandingConfig";

const getBranding = () => {
  const tenant = process.env.REACT_APP_TENANT || "default";
  return branding[tenant] || branding.default;
};

export default getBranding;