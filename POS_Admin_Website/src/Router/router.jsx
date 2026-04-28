import React from "react";
import { Route, Routes, Outlet } from "react-router-dom";
import Header from "../InitialPage/Sidebar/Header";
import Sidebar from "../InitialPage/Sidebar/Sidebar";
import { pagesRoute, posRoutes, publicRoutes } from "./router.link";
import { useSelector } from "react-redux";
// import ThemeSettings from "../InitialPage/themeSettings";

import RequireAuth from "../core/auth/RequireAuth";

const AllRoutes = () => {
  const data = useSelector((state) => state.toggle_header);

  const HeaderLayout = () => (
    <div className={`main-wrapper ${data ? "header-collapse" : ""}`}>
      <Header />
      <Sidebar />
      <Outlet />
      {/* <ThemeSettings /> */}
    </div>
  );

  const Authpages = () => (
    <div className={data ? "header-collapse" : ""}>
      <Outlet />
    </div>
  );

  const Pospages = () => (
    <div>
      <Header />
      <Outlet />
    </div>
  );

  return (
    <div>
      <Routes>
        {/* POS routes */}
        <Route path="/pos" element={<Pospages />}>
          {posRoutes.map((route, id) => (
            <Route path={route.path} element={route.element} key={id} />
          ))}
        </Route>

        {/* ✅ PROTECTED APP AREA */}
        <Route element={<RequireAuth />}>
          <Route path="/" element={<HeaderLayout />}>
            {publicRoutes.map((route, id) => (
              <Route path={route.path} element={route.element} key={id} />
            ))}
          </Route>
        </Route>

        {/* ✅ PUBLIC AUTH PAGES */}
        <Route path="/" element={<Authpages />}>
          {pagesRoute.map((route, id) => (
            <Route path={route.path} element={route.element} key={id} />
          ))}
        </Route>
      </Routes>
    </div>
  );
};

export default AllRoutes;