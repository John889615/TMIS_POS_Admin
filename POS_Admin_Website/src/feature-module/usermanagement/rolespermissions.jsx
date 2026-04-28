import React, { useState, useEffect } from "react";
import { OverlayTrigger, Tooltip } from "react-bootstrap";
import { useDispatch, useSelector } from "react-redux";
import ImageWithBasePath from "../../core/img/imagewithbasebath";
import { Link } from "react-router-dom";
import { ChevronUp, RotateCcw } from "feather-icons-react/build/IconComponents";
import { setToogleHeader } from "../../core/redux/action";
import { Calendar, Filter, PlusCircle, Sliders, Zap } from "react-feather";
import Select from "react-select";
import { DatePicker } from "antd";
import withReactContent from "sweetalert2-react-content";
import Swal from "sweetalert2";
import Table from "../../core/pagination/datatable";
import AddRole from "../../core/modals/usermanagement/addrole";
import EditRole from "../../core/modals/usermanagement/editrole";
import { all_routes } from "../../Router/all_routes";
import { getAllRoles, newRole, updateRole } from "../../services/usermanagement/roleService";


const RolesPermissions = () => {
  const route = all_routes;
  const [showInlineForm, setShowInlineForm] = useState(false);
  const [newRoleName, setNewRoleName] = useState('');
  const data = useSelector((state) => state.toggle_header);
  // const dataSource = useSelector((state) => state.rolesandpermission_data);
  const [selectedRole, setSelectedRole] = useState(null);
  const [showModel, setModelShow] = useState(false);
  const dispatch = useDispatch();
  const [isFilterVisible, setIsFilterVisible] = useState(false);
  const toggleFilterVisibility = () => {
    setIsFilterVisible((prevVisibility) => !prevVisibility);
  };
  const oldandlatestvalue = [
    { value: "date", label: "Sort by Date" },
    { value: "newest", label: "Newest" },
    { value: "oldest", label: "Oldest" },
  ];
  const role = [
    { value: "Choose Role", label: "ose Role" },
    { value: "AcStore ", label: "AcStore" },
    { value: "Admin", label: "Admin" },
  ];

  const [roleList, setRoleList] = useState([]);


  const [selectedDate, setSelectedDate] = useState(new Date());
  const handleDateChange = (date) => {
    setSelectedDate(date);
  };

  useEffect(() => {
    fetchRoles();
  }, []);

  const fetchRoles = async () => {
    try {
      const users = await getAllRoles();
      console.log("user: ", users);
      setRoleList(users); // or dispatch to Redux if needed
    } catch (err) {
      console.error("Failed to load users:", err.message);
    }
  };

  const handleClose = () => setModelShow(false);

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

  const handleEditRole = (record) => {
    setSelectedRole(record);
    setModelShow(true);
  };

  const columns = [
    {
      title: "Role Name",
      dataIndex: "Role",
      sorter: (a, b) => a.Role.length - b.Role.length,
    },
    {
      title: "Description",
      dataIndex: "Description",
      sorter: (a, b) => a.Description.length - b.Description.length,
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
            <Link
              className="me-2 p-2"
              to="#"
              onClick={() => handleEditRole(record)} // 🧠 Use the method
            >
              <i className="feather-edit"></i>
            </Link>
            <Link className="me-2 p-2" to={route.permissions}>
              <i
                data-feather="sheild"
                className="feather feather-shield shield"
              ></i>
            </Link>
            <Link className="confirm-text p-2" to="#">
              <i
                data-feather="trash-2"
                className="feather-trash-2"
                onClick={showConfirmationAlert}
              ></i>
            </Link>
          </div>
        </div>
      ),
    },
  ];

  const MySwal = withReactContent(Swal);

  const showConfirmationAlert = () => {
    MySwal.fire({
      title: "Are you sure?",
      text: "You won't be able to revert this!",
      showCancelButton: true,
      confirmButtonColor: "#00ff00",
      confirmButtonText: "Yes, delete it!",
      cancelButtonColor: "#ff0000",
      cancelButtonText: "Cancel",
    }).then((result) => {
      if (result.isConfirmed) {
        MySwal.fire({
          title: "Deleted!",
          text: "Your file has been deleted.",
          className: "btn btn-success",
          confirmButtonText: "OK",
          customClass: {
            confirmButton: "btn btn-success",
          },
        });
      } else {
        MySwal.close();
      }
    });
  };

  const handleQuickSaveRole = async () => {
    if (!newRoleName.trim()) return;

    try {
      await newRole({ Role: newRoleName.trim() });

      setShowInlineForm(false);
      setNewRoleName('');
      fetchRoles();
    } catch (error) {
      console.error("Error saving role:", error);
    }
  };

  const UpdateRole = async (roleData) => {
    console.log("Update Role vales", roleData);
    await updateRole(roleData);
    fetchRoles();
    setModelShow(false);
  };


  return (
    <div>
      <div className="page-wrapper">
        <div className="content">
          <div className="page-header">
            <div className="add-item d-flex">
              <div className="page-title">
                <h4>Roles &amp; Permission</h4>
                <h6>Manage your roles</h6>
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
              <li className="d-flex align-items-center gap-2">
                {showInlineForm && (
                  <form
                    className="d-flex align-items-center gap-2"
                    onSubmit={(e) => {
                      e.preventDefault();
                      handleQuickSaveRole();
                    }}
                  >
                    <input
                      type="text"
                      className="form-control"
                      placeholder="Enter Role Name"
                      value={newRoleName}
                      onChange={(e) => setNewRoleName(e.target.value)}
                      style={{ maxWidth: 300 }}
                      required
                    />
                    <button type="submit" className="btn btn-submit">
                      Save
                    </button>
                    <button
                      type="button"
                      className="btn btn-cancel"
                      onClick={() => {
                        setShowInlineForm(false);
                        setNewRoleName('');
                      }}
                    >
                      Cancel
                    </button>
                  </form>
                )}

              </li>

            </ul>
            <div className="page-btn">
              <button
                type="button"
                className="btn btn-added"
                onClick={() => setShowInlineForm(true)}
              >
                <PlusCircle className="me-2" />
                Add New Role
              </button>
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
                  <Select className="img-select"
                    classNamePrefix="react-select"
                    options={oldandlatestvalue}
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
                        <Zap className="info-img" />
                        <Select className="img-select"
                          classNamePrefix="react-select"
                          options={role}
                          placeholder="Choose Role"
                        />
                      </div>
                    </div>
                    <div className="col-lg-3 col-sm-6 col-12">
                      <div className="input-blocks">
                        <Calendar className="info-img" />
                        <div className="input-groupicon">
                          <DatePicker
                            selected={selectedDate}
                            onChange={handleDateChange}
                            type="date"
                            className="filterdatepicker"
                            dateFormat="dd-MM-yyyy"
                            placeholder="Choose Date"
                          />
                        </div>
                      </div>
                    </div>
                    <div className="col-lg-3 col-sm-6 col-12 ms-auto">
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
                <Table columns={columns} dataSource={roleList} />
              </div>
            </div>
          </div>
          {/* /product list */}
        </div>
      </div>
      <AddRole />
      <EditRole show={showModel} onHide={handleClose} roleData={selectedRole} onSave={UpdateRole} />
    </div>
  );
};

export default RolesPermissions;
