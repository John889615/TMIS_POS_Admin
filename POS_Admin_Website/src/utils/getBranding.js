import branding from "../config/brandingConfig";

const getBranding = () => {
  const tenant = import.meta.env.VITE_TENANT || "default";
  return branding[tenant] || branding.default;
};

export default getBranding;