import React, { useState, useEffect } from "react";
import PropTypes from 'prop-types';
import { getDebtorContacts, addDebtorContact, updateDebtorContact } from "../../services/debtors/contacts";
import { getAllDialingCode } from "../../services/entityData/dialingCode";
import { getAllContactType } from "../../services/entityData/contactType";
import DebtorContactForm from "../../core/modals/debtors/debtorContactModel";

import { Button, Modal } from "react-bootstrap";  // ✅ Modal imported
import { Link } from "react-router-dom";
import { PlusCircle } from "react-feather";

const DebtorContact = ({
    showContactModel,
    handleContactClose,
    debtorId
}) => {
    const [contactList, setContactList] = useState([]);
    const [searchTerm, setSearchTerm] = useState("");
    const [dialogCodeList, setDialogCodeList] = useState([]);
    const [contactTypeList, setContactTypeList] = useState([]);
    const [showModel, setModelShow] = useState(false);
    const [selectedContact, setSelectedContact] = useState(null);
    const [selectedDebtor, setSelectedDebtor] = useState(debtorId);

    useEffect(() => {
        fetchAddresses();
    }, []);

    const fetchAddresses = async () => {
        try {
            const data = await getDebtorContacts(debtorId);
            debugger;
            setContactList(data);
            const dialogCodeData = await getAllDialingCode();
            setDialogCodeList(dialogCodeData);
            const contactTypeData = await getAllContactType();
            setContactTypeList(contactTypeData);
        } catch (err) {
            console.error("Failed to load addresses:", err.message);
        }
    };

    const filteredData = contactList.filter((item) =>
        Object.values(item).some(
            (value) =>
                typeof value === "string" &&
                value.toLowerCase().includes(searchTerm.toLowerCase())
        )
    );

    const refetchData = async () => {
        const data = await getDebtorContacts(selectedDebtor);
        setContactList(data);
    };

    const handleShow = () => {
        setSelectedContact(null); // Clear previous data
        setModelShow(true);
    };
    const handleClose = () => setModelShow(false);
    const handleAddContact = async (data) => {
        try {
            if (data.ContactID) {
                await updateDebtorContact(data);
            }
            else {
                await addDebtorContact(data);
            }
            await refetchData();
            setModelShow(false);
        } catch (err) {
            console.error("Error creating user:", err.message);
        }
    };

    const handleEditContact = (record) => {
        record.FK_DebtorID = selectedDebtor;
        setSelectedContact(record);
        setModelShow(true);
    };

    return (
        <Modal show={showContactModel} onHide={handleContactClose} centered size="xl" dialogClassName="custom-modal-two">
            <Modal.Header closeButton className="custom-modal-header border-0">
                <Modal.Title>Location Contacts</Modal.Title>
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
                            Add New Contact
                        </Button>
                    </div>
                </div>
                <div className="row">
                    <div className="col-12">
                        <div className="table-responsive">
                            <table className="table table-bordered table-striped">
                                <thead>
                                    <tr>
                                        <th>Contact Value</th>
                                        <th>Contact Type</th>
                                        <th>Dialing Code</th>
                                        <th>Is Primary</th>
                                        <th>Is Marketing</th>
                                        <th>Is Emergency</th>
                                        <th>Preferred Contact Time</th>
                                        <th>Preferred Language Code</th>
                                        <th>ValidFrom</th>
                                        <th>ValidTo</th>
                                        <th>Is Verified</th>
                                        <th>Notes</th>
                                        <th>Actions</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    {filteredData.length > 0 ? (
                                        filteredData.map((item, index) => (
                                            <tr key={index}>
                                                <td>{item.ContactValue}</td>
                                                <td>{item.ContactType}</td>
                                                <td>{item.DialingCode}</td>
                                                <td>{item.IsPrimary ? 'Yes' : 'No'}</td>
                                                <td>{item.IsMarketing ? 'Yes' : 'No'}</td>
                                                <td>{item.IsEmergency ? 'Yes' : 'No'}</td>
                                                <td>{item.PreferredContactTime}</td>
                                                <td>{item.PreferredLanguageCode}</td>
                                                <td>{item.ValidFrom}</td>
                                                <td>{item.ValidTo}</td>
                                                <td>{item.IsVerified ? 'Yes' : 'No'}</td>
                                                <td>{item.Notes}</td>
                                                <td>
                                                    <button type='button'
                                                        onClick={() => handleEditContact(item)}
                                                        className="btn btn-sm btn-primary me-2">
                                                        <i className="feather-edit"></i>
                                                    </button>
                                                </td>
                                            </tr>
                                        ))
                                    ) : (
                                        <tr>
                                            <td colSpan="13" className="text-center">
                                                No records found
                                            </td>
                                        </tr>
                                    )}
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
                {showModel &&
                    <DebtorContactForm
                        onSubmit={handleAddContact}
                        showModel={showModel}
                        handleClose={handleClose}
                        data={selectedContact}
                        contactTypeList={contactTypeList}
                        dialogCodeList={dialogCodeList}
                        debtorId={debtorId}
                    />
                }
            </Modal.Body>
            <Modal.Footer className="modal-footer-btn d-flex justify-content-end">
                <button
                    type="button"
                    className="btn btn-cancel me-2"
                    onClick={handleContactClose}
                >
                    Cancel
                </button>
            </Modal.Footer>
        </Modal>

    );
};


export default DebtorContact;


DebtorContact.propTypes = {
    showContactModel: PropTypes.bool.isRequired,
    handleContactClose: PropTypes.func.isRequired,
    debtorId: PropTypes.oneOfType([PropTypes.string, PropTypes.number]).isRequired,
};
