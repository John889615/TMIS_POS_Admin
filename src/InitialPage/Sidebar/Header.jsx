import React, { useEffect, useMemo, useState } from "react";
import { Link } from "react-router-dom";
import FeatherIcon from "feather-icons-react";
import ImageWithBasePath from "../../core/img/imagewithbasebath";
import { Search, Settings, User, XCircle } from "react-feather";
import { all_routes } from "../../Router/all_routes";
import { useAuth } from "../../context/AuthContext";
import { useDispatch, useSelector } from "react-redux";
import { getAllDebtors } from "../../services/debtors/debtors";
import getBranding from "../../utils/getBranding";
import Select from "react-select"; // ✅ NEW

const Header = () => {
  const dispatch = useDispatch();
  const route = all_routes;

  const [toggle, SetToggle] = useState(false);
  const [isFullscreen, setIsFullscreen] = useState(false);

  const { user } = useAuth();
  console.log(user)
  const debtors = useSelector((state) => state.debtors_data);
  const selectedDebtorStore = useSelector((state) => state.selectedDebtorStore);

  const branding = getBranding();

  // ✅ React-Select option list
  const debtorOptions = useMemo(() => {
    return (debtors || []).map((d) => ({
      value: d.DebtorID,
      label: `${d.ShortCode} ${d.Name}`,
    }));
  }, [debtors]);

  // ✅ React-Select selected value
  const selectedDebtorOption = useMemo(() => {
    if (!selectedDebtorStore) return null;
    const id = Number(selectedDebtorStore);
    return debtorOptions.find((x) => Number(x.value) === id) || null;
  }, [selectedDebtorStore, debtorOptions]);

  const handleStoreChange = (option) => {
    const selectedId = option?.value ?? "";
    dispatch({ type: "SelectedDebtorStore", payload: selectedId });
  };

  const isElementVisible = (element) => {
    return element.offsetWidth > 0 || element.offsetHeight > 0;
  };

  useEffect(() => {
    const fetchDebtors = async () => {
      try {
        const data = await getAllDebtors();
        dispatch({ type: "Debtors_Data", payload: data });
      } catch (error) {
        console.error("Failed to fetch debtors:", error);
      }
    };

    if (!debtors || debtors.length === 0) {
      fetchDebtors();
    }

    const handleMouseover = (e) => {
      e.stopPropagation();

      const body = document.body;
      const toggleBtn = document.getElementById("toggle_btn");

      if (body.classList.contains("mini-sidebar") && isElementVisible(toggleBtn)) {
        e.preventDefault();
      }
    };

    document.addEventListener("mouseover", handleMouseover);

    return () => {
      document.removeEventListener("mouseover", handleMouseover);
    };
  }, [debtors, dispatch]);

  useEffect(() => {
    const handleFullscreenChange = () => {
      setIsFullscreen(
        document.fullscreenElement ||
          document.mozFullScreenElement ||
          document.webkitFullscreenElement ||
          document.msFullscreenElement
      );
    };

    document.addEventListener("fullscreenchange", handleFullscreenChange);
    document.addEventListener("mozfullscreenchange", handleFullscreenChange);
    document.addEventListener("webkitfullscreenchange", handleFullscreenChange);
    document.addEventListener("msfullscreenchange", handleFullscreenChange);

    return () => {
      document.removeEventListener("fullscreenchange", handleFullscreenChange);
      document.removeEventListener("mozfullscreenchange", handleFullscreenChange);
      document.removeEventListener(
        "webkitfullscreenchange",
        handleFullscreenChange
      );
      document.removeEventListener("msfullscreenchange", handleFullscreenChange);
    };
  }, []);

  const handlesidebar = () => {
    document.body.classList.toggle("mini-sidebar");
    SetToggle((current) => !current);
  };

  const expandMenu = () => {
    document.body.classList.remove("expand-menu");
  };

  const expandMenuOpen = () => {
    document.body.classList.add("expand-menu");
  };

  const sidebarOverlay = () => {
    document?.querySelector(".main-wrapper")?.classList?.toggle("slide-nav");
    document?.querySelector(".sidebar-overlay")?.classList?.toggle("opened");
    document?.querySelector("html")?.classList?.toggle("menu-opened");
  };

  // keeping your original pattern
  let pathname = location.pathname;

  const exclusionArray = [
    "/reactjs/template/dream-pos/index-three",
    "/reactjs/template/dream-pos/index-one",
  ];
  if (exclusionArray.indexOf(window.location.pathname) >= 0) {
    return "";
  }

  const toggleFullscreen = (elem) => {
    elem = elem || document.documentElement;
    if (
      !document.fullscreenElement &&
      !document.mozFullScreenElement &&
      !document.webkitFullscreenElement &&
      !document.msFullscreenElement
    ) {
      if (elem.requestFullscreen) {
        elem.requestFullscreen();
      } else if (elem.msRequestFullscreen) {
        elem.msRequestFullscreen();
      } else if (elem.mozRequestFullScreen) {
        elem.mozRequestFullScreen();
      } else if (elem.webkitRequestFullscreen) {
        elem.webkitRequestFullscreen(Element.ALLOW_KEYBOARD_INPUT);
      }
    } else {
      if (document.exitFullscreen) {
        document.exitFullscreen();
      } else if (document.msExitFullscreen) {
        document.msExitFullscreen();
      } else if (document.mozCancelFullScreen) {
        document.mozCancelFullScreen();
      } else if (document.webkitExitFullscreen) {
        document.webkitExitFullscreen();
      }
    }
  };

  // ✅ react-select styles so it looks good inside header
  const storeSelectStyles = useMemo(
    () => ({
      container: (base) => ({ ...base, minWidth: 280 }),
      control: (base) => ({
        ...base,
        minHeight: 34,
        height: 34,
        borderRadius: 8,
      }),
      valueContainer: (base) => ({ ...base, height: 34, padding: "0 10px" }),
      input: (base) => ({ ...base, margin: 0, padding: 0 }),
      indicatorsContainer: (base) => ({ ...base, height: 34 }),
      menu: (base) => ({ ...base, zIndex: 9999 }),
    }),
    []
  );

return (
  <>
    <div className="header">
      {/* Logo */}
      <div
        className={`header-left ${toggle ? "" : "active"}`}
        onMouseLeave={expandMenu}
        onMouseOver={expandMenuOpen}
      >
        <Link to="/dashboard" className="logo logo-normal">
          <ImageWithBasePath src={branding.logo} alt="img" />
        </Link>
        <Link to="/dashboard" className="logo logo-white">
          <ImageWithBasePath src={branding.logoWhite} alt="img" />
        </Link>
        <Link to="/dashboard" className="logo-small">
          <ImageWithBasePath src={branding.smallLogo} alt="img" />
        </Link>
        <Link
          id="toggle_btn"
          to="#"
          style={{
            display:
              pathname.includes("tasks") || pathname.includes("pos")
                ? "none"
                : pathname.includes("compose")
                ? "none"
                : "",
          }}
          onClick={handlesidebar}
        >
          <FeatherIcon icon="chevrons-left" className="feather-16" />
        </Link>
      </div>
      {/* /Logo */}

      <Link id="mobile_btn" className="mobile_btn" to="#" onClick={sidebarOverlay}>
        <span className="bar-icon">
          <span />
          <span />
          <span />
        </span>
      </Link>

      {/* Header Menu */}
      <ul className="nav user-menu">
        <li className="nav-item nav-searchinputs">
          <div className="top-nav-search">
            <Select
              options={debtorOptions}
              value={selectedDebtorOption}
              onChange={handleStoreChange}
              placeholder="Select location..."
              isClearable
              isSearchable
              classNamePrefix="react-select"
              styles={storeSelectStyles}
            />
          </div>
        </li>

        <li className="nav-item nav-item-box">
          <Link
            to="#"
            id="btnFullscreen"
            onClick={() => toggleFullscreen()}
            className={isFullscreen ? "Exit Fullscreen" : "Go Fullscreen"}
          >
            <FeatherIcon icon="maximize" />
          </Link>
        </li>

        {/* <li className="nav-item nav-item-box">
          <Link to="/general-settings">
            <FeatherIcon icon="settings" />
          </Link>
        </li> */}

        <li className="nav-item dropdown has-arrow main-drop">
          <Link to="#" className="dropdown-toggle nav-link userset" data-bs-toggle="dropdown">
            <span className="user-info">
              <span className="user-letter">
                <ImageWithBasePath
                  src="assets/img/profiles/avator1.jpg"
                  alt="img"
                  className="img-fluid"
                />
              </span>
              <span className="user-detail">
                <span className="user-name">{user?.email}</span>
                <span className="user-role">{user?.code}</span>
              </span>
            </span>
          </Link>

          <div className="dropdown-menu menu-drop-user">
            <div className="profilename">
              <div className="profileset">
                <span className="user-img">
                  <ImageWithBasePath src="assets/img/profiles/avator1.jpg" alt="img" />
                  <span className="status online" />
                </span>
                <div className="profilesets">
                  <h6>John Smilga</h6>
                  <h5>Super Admin</h5>
                </div>
              </div>
              <hr className="m-0" />

              <Link className="dropdown-item" to={route.profile}>
                <User className="me-2" /> My Profile
              </Link>
              <Link className="dropdown-item" to={route.generalsettings}>
                <Settings className="me-2" />
                Settings
              </Link>

              <hr className="m-0" />

              <Link className="dropdown-item logout pb-0" to="/signin">
                <ImageWithBasePath
                  src="assets/img/icons/log-out.svg"
                  alt="img"
                  className="me-2"
                />
                Logout
              </Link>
            </div>
          </div>
        </li>
      </ul>
      {/* /Header Menu */}

      {/* Mobile Menu */}
      <div className="dropdown mobile-user-menu">
        <Link
          to="#"
          className="nav-link dropdown-toggle"
          data-bs-toggle="dropdown"
          aria-expanded="false"
        >
          <i className="fa fa-ellipsis-v" />
        </Link>
        <div className="dropdown-menu dropdown-menu-right">
          <Link className="dropdown-item" to="profile">
            My Profile
          </Link>
          <Link className="dropdown-item" to="generalsettings">
            Settings
          </Link>
          <Link className="dropdown-item" to="signin">
            Logout
          </Link>
        </div>
      </div>
      {/* /Mobile Menu */}
    </div>
  </>
);
};

export default Header;