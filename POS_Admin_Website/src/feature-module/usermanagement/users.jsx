import React, { useState, useEffect } from "react";
import { OverlayTrigger, Tooltip, Button, Toast } from "react-bootstrap";
import { Link } from "react-router-dom";
import ImageWithBasePath from "../../core/img/imagewithbasebath";
import { ChevronUp, RotateCcw } from "feather-icons-react/build/IconComponents";
import { setToogleHeader } from "../../core/redux/action";
import { useDispatch, useSelector } from "react-redux";
import {
  Filter,
  PlusCircle,
  Sliders,
  StopCircle,
  User,
  Zap,
} from "react-feather";
import Select from "react-select";
import Table from "../../core/pagination/datatable";
import AddUsers from "../../core/modals/usermanagement/addusers";
import EditUser from "../../core/modals/usermanagement/edituser";
import AssignRoleToUser from "../../core/modals/usermanagement/assignRoleToUser";

import { getAllUsers, newUser, getUserRole, assignNewRole, deleteUserRole } from "../../services/usermanagement/userService";
import { getAllRoles } from "../../services/usermanagement/roleService";

const Users = () => {
  const [usersList, setUsersList] = useState([]);
  const [roleList, setRoleList] = useState([]);
  const [userAssignRole, setUserAssignRole] = useState([]);
  const [showModel, setModelShow] = useState(false);
  const [showRoleModel, setRoleModelShow] = useState(false);
  const [showDangerToast, setShowDangerToast] = useState(false);
  const dispatch = useDispatch();
  const data = useSelector((state) => state.toggle_header);
  // const dataSource = useSelector((state) => state.userlist_data);
  const [isFilterVisible, setIsFilterVisible] = useState(false);
  const [selectedUser, setSelectedUser] = useState(null);
  const [selectedUserForRole, setSelectedUserForRole] = useState(null);
  const toggleFilterVisibility = () => {
    setIsFilterVisible((prevVisibility) => !prevVisibility);
  };

  useEffect(() => {
    fetchUsers();
    fetchUserRoles();
  }, []);

  const fetchUsers = async () => {
    try {
      const users = await getAllUsers();
      setUsersList(users); // or dispatch to Redux if needed
    } catch (err) {
      console.error("Failed to load users:", err.message);
    }
  };

  const fetchUserRoles = async () => {
    try {
      const role = await getAllRoles();
      setRoleList(role); // or dispatch to Redux if needed
    } catch (err) {
      console.error("Failed to load users:", err.message);
    }
  };

  const handleShow = () => setModelShow(true);
  const handleClose = () => setModelShow(false);
  const handleRoleClose = () => setRoleModelShow(false);

  const renderTooltip = (props) => (
    <Tooltip id="pdf-tooltip" {...props}>
      Pdf
    </Tooltip>
  );
  const renderExcelTooltip = (props) => (
    <Tooltip id="excel-tooltip" {...props}>
      Excel
    </Tooltip>
  );
  const renderPrinterTooltip = (props) => (
    <Tooltip id="printer-tooltip" {...props}>
      Printer
    </Tooltip>
  );
  const renderRefreshTooltip = (props) => (
    <Tooltip id="refresh-tooltip" {...props}>
      Refresh
    </Tooltip>
  );
  const renderCollapseTooltip = (props) => (
    <Tooltip id="refresh-tooltip" {...props}>
      Collapse
    </Tooltip>
  );

  useEffect(() => {
    const timeoutId = setTimeout(() => {
      setShowDangerToast(false);
    }, 6000);
    return () => clearTimeout(timeoutId);
  }, [showDangerToast]);

  const handleAddUser = async (userData) => {
    try {
      const result = await newUser(userData);
      if (!result.Success) {
        setShowDangerToast(true);
        return;
      }
      fetchUsers();
      setModelShow(false);
    } catch (err) {
      console.error("Error creating user:", err.message);
    }
  };

  const handleDangerToastClose = () => {
    setShowDangerToast(false);
  };

  const columns = [
    {
      title: "User ID",
      dataIndex: "UserID",
      sorter: (a, b) => a.UserID - b.UserID,
    },
    {
      title: "Name",
      render: (text, record) => (
        <span>{`${record.FirstName || ""} ${record.LastName || ""}`.trim()}</span>
      ),
      sorter: (a, b) => {
        const nameA = `${a.FirstName || ""} ${a.LastName || ""}`.toLowerCase();
        const nameB = `${b.FirstName || ""} ${b.LastName || ""}`.toLowerCase();
        return nameA.localeCompare(nameB);
      },
    },
    {
      title: "Email",
      dataIndex: "Email",
      sorter: (a, b) => a.Email.localeCompare(b.Email),
    },
    {
      title: "Username",
      dataIndex: "Username",
      sorter: (a, b) => a.Username.localeCompare(b.Username),
    },
    {
      title: "Status",
      dataIndex: "IsActive",
      render: (isActive) => (
        <span
          className={`badge ${isActive ? "badge-linesuccess" : "badge-linedanger"
            }`}
        >
          {isActive ? "Active" : "Inactive"}
        </span>
      ),
      sorter: (a, b) => Number(b.IsActive) - Number(a.IsActive),
    },
    {
      title: "Actions",
      dataIndex: "actions",
      key: "actions",
      render: (_, record) => (
        <div className="action-table-data">
          <div className="edit-delete-action">
            <Link to="#" onClick={() => handleRole(record.UserID)} className="me-2 p-2">
              <i data-feather="settings" className="feather feather-settings shield"></i>
            </Link>
            <Link
              className="me-2 p-2"
              to="#"
              onClick={() => handleEditRole(record)} // 🧠 Use the method
            >
              <i className="feather-edit"></i>
            </Link>
          </div>
        </div>
      ),
    }
  ];

  const handleEditRole = (record) => {
    console.log("User Data", record);
    setSelectedUser(record);
    setModelShow(true);
  };

  const handleRole = async (Id) => {
    const userRole = await getUserRole(Id);
    console.log('User Role:', userRole);
    setSelectedUserForRole(Id);
    setUserAssignRole(userRole.Data)
    setRoleModelShow(true);
  };

  const AssignRole = async (roleId) => {
    console.log("Role Id", roleId);
    var data = {
      FK_UserID: selectedUserForRole,
      FK_RoleID: roleId
    }

    const result = await assignNewRole(data);
    if (result.Success) {
      const userRole = await getUserRole(selectedUserForRole);
      setUserAssignRole(userRole.Data)
    }
  };

  const DeleteRole = async (userRoleId) => {
    const result = await deleteUserRole(userRoleId);
    if (result.Success) {
      const userRole = await getUserRole(selectedUserForRole);
      setUserAssignRole(userRole.Data)
    }
  };

  return (
    <div>
      <div className="page-wrapper">
        <div className="content">
          <div className="page-header">
            <div className="add-item d-flex">
              <div className="page-title">
                <h4>User List</h4>
                <h6>Manage Your Users</h6>
              </div>
            </div>
            <ul className="table-top-head">
              <li>
                <OverlayTrigger placement="top" overlay={renderTooltip}>
                  <Link>
                    <ImageWithBasePath
                      src="assets/img/icons/pdf.svg"
                      alt="img"
                    />
                  </Link>
                </OverlayTrigger>
              </li>
              <li>
                <OverlayTrigger placement="top" overlay={renderExcelTooltip}>
                  <Link data-bs-toggle="tooltip" data-bs-placement="top">
                    <ImageWithBasePath
                      src="assets/img/icons/excel.svg"
                      alt="img"
                    />
                  </Link>
                </OverlayTrigger>
              </li>
              <li>
                <OverlayTrigger placement="top" overlay={renderPrinterTooltip}>
                  <Link data-bs-toggle="tooltip" data-bs-placement="top">
                    <i data-feather="printer" className="feather-printer" />
                  </Link>
                </OverlayTrigger>
              </li>
              <li>
                <OverlayTrigger placement="top" overlay={renderRefreshTooltip}>
                  <Link data-bs-toggle="tooltip" data-bs-placement="top">
                    <RotateCcw />
                  </Link>
                </OverlayTrigger>
              </li>
              <li>
                <OverlayTrigger placement="top" overlay={renderCollapseTooltip}>
                  <Link
                    data-bs-toggle="tooltip"
                    data-bs-placement="top"
                    id="collapse-header"
                    className={data ? "active" : ""}
                    onClick={() => {
                      dispatch(setToogleHeader(!data));
                    }}
                  >
                    <ChevronUp />
                  </Link>
                </OverlayTrigger>
              </li>
            </ul>
            <div className="page-btn">
              <Button variant="none" className="btn btn-added" onClick={handleShow}>
                <PlusCircle className="me-2" />
                Add New User
              </Button>
            </div>
          </div>
          {/* /product list */}
          <div className="card table-list-card">
            <div className="card-body">
              <div className="table-top">
                <div className="search-set">
                  <div className="search-input">
                    <input
                      type="text"
                      placeholder="Search"
                      className="form-control form-control-sm formsearch"
                    />
                    <Link to className="btn btn-searchset">
                      <i data-feather="search" className="feather-search" />
                    </Link>
                  </div>
                </div>
                <div className="search-path">
                  <Link
                    className={`btn btn-filter ${isFilterVisible ? "setclose" : ""
                      }`}
                    id="filter_search"
                  >
                    <Filter
                      className="filter-icon"
                      onClick={toggleFilterVisibility}
                    />
                    <span onClick={toggleFilterVisibility}>
                      <ImageWithBasePath
                        src="assets/img/icons/closes.svg"
                        alt="img"
                      />
                    </span>
                  </Link>
                </div>
                <div className="form-sort">
                  <Sliders className="info-img" />
                  <Select
                    className="img-select"
                    classNamePrefix="react-select"
                    placeholder="Newest"
                  />
                </div>
              </div>
              {/* /Filter */}
              <div
                className={`card${isFilterVisible ? " visible" : ""}`}
                id="filter_inputs"
                style={{ display: isFilterVisible ? "block" : "none" }}
              >
                <div className="card-body pb-0">
                  <div className="row">
                    <div className="col-lg-3 col-sm-6 col-12">
                      <div className="input-blocks">
                        <User className="info-img" />
                        <Select
                          className="img-select"
                          classNamePrefix="react-select"
                          options={usersList}
                          placeholder="Newest"
                        />
                      </div>
                    </div>
                    <div className="col-lg-3 col-sm-6 col-12">
                      <div className="input-blocks">
                        <StopCircle className="info-img" />

                        <Select
                          className="img-select"
                          classNamePrefix="react-select"
                          options={status}
                          placeholder="Choose Status"
                        />
                      </div>
                    </div>
                    <div className="col-lg-3 col-sm-6 col-12">
                      <div className="input-blocks">
                        <Zap className="info-img" />

                        <Select
                          className="img-select"
                          classNamePrefix="react-select"
                          placeholder="Choose Role"
                        />
                      </div>
                    </div>
                    <div className="col-lg-3 col-sm-6 col-12">
                      <div className="input-blocks">
                        <a className="btn btn-filters ms-auto">
                          {" "}
                          <i
                            data-feather="search"
                            className="feather-search"
                          />{" "}
                          Search{" "}
                        </a>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
              {/* /Filter */}
              <div className="table-responsive">
                <Table columns={columns} dataSource={usersList} />
              </div>
            </div>
          </div>
          {/* /product list */}
        </div>
        <div className="toast-container position-fixed top-0 end-0 p-3">
          <Toast
            show={showDangerToast}
            onClose={handleDangerToastClose}
            id="dangerToast"
            className="colored-toast bg-danger-transparent"
            role="alert"
            aria-live="assertive"
            aria-atomic="true"
          >
            <Toast.Header closeButton className="bg-danger text-fixed-white">
              <strong className="me-auto">Error</strong>
              <Button
                variant="close"
                onClick={handleDangerToastClose}
                aria-label="Close"
              />
            </Toast.Header>
            <Toast.Body>
              {/* Add your toast content here */}
              Username already in use.
            </Toast.Body>
          </Toast>
        </div>

      </div>

      <AddUsers roleList={roleList} onSubmitUser={handleAddUser} showModel={showModel} handleClose={handleClose} userData={selectedUser} />
      <AssignRoleToUser
        roleList={userAssignRole}
        show={showRoleModel}
        onHide={handleRoleClose}
        onSave={AssignRole}
        onDeleteRole={DeleteRole}
      />
      <EditUser />
    </div>
  );
};

export default Users;
