import React, { useState, useEffect } from "react";
import PropTypes from 'prop-types';
import { getDebtorAddress, getDebtorAddressType, addDebtorAddress, updateDebtorAddress } from "../../services/debtors/address";
import { getAllDebtors } from "../../services/debtors/debtors";
import { getAllCountry } from "../../services/entityData/country";
import { getAllCountryProvince } from "../../services/entityData/countryProvince";
import { getAllAddressRegion } from "../../services/entityData/addressRegion";
import { getAllAddressType } from "../../services/entityData/addressType";

import DebtorAddressForm from "../../core/modals/debtors/debtorAddressModel";

import { Button, Modal } from "react-bootstrap";  // ✅ Modal imported
import { Link } from "react-router-dom";
import { PlusCircle } from "react-feather";

const DebtorAddress = ({
    showAddressModel,
    handleAddressClose,
    debtorId
}) => {
    const [debtorList, setDebtorList] = useState([]);
    const [addressList, setAddressList] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [countryList, setCountryList] = useState([]);
    const [provinceList, setProvinceList] = useState([]);
    const [regionList, setRegionList] = useState([]);
    const [addressTypeList, setAddressTypeList] = useState([]);
    const [showModel, setModelShow] = useState(false);
    const [selectedAddress, setSelectedAddress] = useState(null);
    const [selectedDebtor, setSelectedDebtor] = useState(debtorId);

    useEffect(() => {
        fetchAddresses();
    }, []);

    const fetchAddresses = async () => {
        try {
            const data = await getDebtorAddress(debtorId);
            setAddressList(data);
            const debtorData = await getAllDebtors();
            setDebtorList(debtorData);
            const countryData = await getAllCountry();
            setCountryList(countryData);
            const provinceData = await getAllCountryProvince();
            setProvinceList(provinceData);
            const regionData = await getAllAddressRegion();
            setRegionList(regionData);
            const addressTypeData = await getAllAddressType();
            setAddressTypeList(addressTypeData);
        } catch (err) {
            console.error("Failed to load addresses:", err.message);
        }
    };

    const filteredData = addressList.filter((item) =>
        Object.values(item).some(
            (value) =>
                typeof value === "string" &&
                value.toLowerCase().includes(searchTerm.toLowerCase())
        )
    );

    const refetchData = async () => {
        const data = await getDebtorAddress(selectedDebtor);
        setAddressList(data);
    };

    const handleShow = () => {
        setSelectedAddress(null); // Clear previous data
        setModelShow(true);
    };
    const handleClose = () => setModelShow(false);
    const handleAddAddress = async (data) => {
        console.log("Submitted Data:", data);
        try {
            if (data.AddressID) {
                await updateDebtorAddress(data);
            }
            else {
                await addDebtorAddress(data);
            }
            await refetchData();
            setModelShow(false);
        } catch (err) {
            console.error("Error creating user:", err.message);
        }
    };

    const handleEditDebtor = (record) => {
        record.FK_DebtorID = selectedDebtor;
        setSelectedAddress(record);
        setModelShow(true);
    };

    return (
        <Modal show={showAddressModel} onHide={handleAddressClose} centered size="xl" dialogClassName="custom-modal-two">
            <Modal.Header closeButton className="custom-modal-header border-0">
                <Modal.Title>Location Address</Modal.Title>
            </Modal.Header>
            <Modal.Body className="custom-modal-body">
                <div className="row mb-2">
                    <div className="col-6">
                        <div className="search-set">
                            <div className="search-input">
                                <input
                                    type="text"
                                    placeholder="Search"
                                    className="form-control"
                                    value={searchTerm}
                                    onChange={(e) => setSearchTerm(e.target.value)}
                                />
                                <Link to="#" className="btn btn-searchset">
                                    <i data-feather="search" className="feather-search" />
                                </Link>
                            </div>
                        </div>
                    </div>
                    <div className="col-6 text-end">
                        <Button variant="none" className="btn btn-primary" onClick={handleShow}>
                            <PlusCircle className="me-2" />
                            Add New Address
                        </Button>
                    </div>
                </div>
                <div className="row">
                    <div className="col-12">
                        <div className="table-responsive">
                            <table className="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th>Address Type</th>
                                        <th>Is Primary</th>
                                        <th>Valid From</th>
                                        <th>Valid To</th>
                                        <th>Country</th>
                                        <th>Province</th>
                                        <th>Address Region</th>
                                        <th>Street Address</th>
                                        <th>Locality</th>
                                        <th>Postal Code</th>
                                        <th>Landmark</th>
                                        <th>Latitude</th>
                                        <th>Longitude</th>
                                        <th>Notes</th>
                                        <th>Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {filteredData.length > 0 ? (
                                        filteredData.map((item, index) => (
                                            <tr key={index}>
                                                <td>{item.AddressType}</td>
                                                <td>{item.IsPrimary ? 'Yes' : 'No'}</td>
                                                <td>{item.ValidFrom}</td>
                                                <td>{item.ValidTo}</td>
                                                <td>{item.Country}</td>
                                                <td>{item.Province}</td>
                                                <td>{item.AddressRegion}</td>
                                                <td>{item.StreetAddress}</td>
                                                <td>{item.Locality}</td>
                                                <td>{item.PostalCode}</td>
                                                <td>{item.Landmark}</td>
                                                <td>{item.Latitude}</td>
                                                <td>{item.Longitude}</td>
                                                <td>{item.Notes}</td>
                                                <td>
                                                    <button type='button'
                                                        onClick={() => handleEditDebtor(item)}
                                                        className="btn btn-sm btn-primary me-2">
                                                        <i className="feather-edit"></i>
                                                    </button>
                                                </td>
                                            </tr>
                                        ))
                                    ) : (
                                        <tr>
                                            <td colSpan="16" className="text-center">
                                                No records found - Please select any debtor for records
                                            </td>
                                        </tr>
                                    )}
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                {showModel &&
                    <DebtorAddressForm
                        onSubmit={handleAddAddress}
                        showModel={showModel}
                        handleClose={handleClose}
                        data={selectedAddress}
                        countryList={countryList}
                        provinceList={provinceList}
                        regionList={regionList}
                        addressTypeList={addressTypeList}
                        debtorId={debtorId}
                    />
                }
            </Modal.Body>
            <Modal.Footer className="modal-footer-btn d-flex justify-content-end">
                <button
                    type="button"
                    className="btn btn-cancel me-2"
                    onClick={handleAddressClose}
                >
                    Cancel
                </button>
            </Modal.Footer>
        </Modal>

    );
};


export default DebtorAddress;


DebtorAddress.propTypes = {
    showAddressModel: PropTypes.bool.isRequired,
    handleAddressClose: PropTypes.func.isRequired,
    debtorId: PropTypes.oneOfType([PropTypes.string, PropTypes.number]).isRequired,
};
