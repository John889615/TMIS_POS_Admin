import React, { useEffect, useRef, useState } from 'react'
import PropTypes, { string } from 'prop-types';
import { Modal } from "react-bootstrap";

const DebtorAddressForm = ({
    onSubmit,
    showModel,
    handleClose,
    data,
    countryList,
    provinceList,
    regionList,
    addressTypeList,
    debtorId
}) => {
    const formRef = useRef(null);
    const [selectedCountry, setSelectedCountry] = useState(data?.FK_CountryID || "");
    const [selectedProvince, setSelectedProvince] = useState(data?.FK_ProvinceID || "");
    const [filteredProvinces, setFilteredProvinces] = useState([]);
    useEffect(() => {
        if (showModel && formRef.current) {
            formRef.current.reset();
        }
    }, [showModel]);

    useEffect(() => {
        // Filter provinces based on selected country
        setFilteredProvinces(
            provinceList.filter(p => String(p.FK_CountryID) === String(selectedCountry))
        );
    }, [selectedCountry, provinceList]);

    const handleCountryChange = (e) => {
        setSelectedCountry(e.target.value);
    };

    const handleProvinceChange = (e) => {
        setSelectedProvince(e.target.value);
    };

    const handleSubmit = (e) => {
        e.preventDefault();
        const form = e.target;
        const addressData = {
            FK_CountryID: Number(form.FK_CountryID.value),
            FK_ProvinceID: Number(form.FK_ProvinceID.value),
            FK_AddressRegionID: Number(form.FK_AddressRegionID.value),
            StreetAddress: form.StreetAddress.value.trim(),
            Locality: form.Locality.value.trim(),
            PostalCode: form.PostalCode.value.trim(),
            Landmark: form.Landmark.value.trim(),
            Notes: form.Notes.value.trim(),
            FK_AddressTypeID: Number(form.FK_AddressTypeID.value),
            IsPrimary: form.IsPrimary.checked,
            ValidFrom: form.ValidFrom.value,
            ValidTo: form.ValidTo.value
        };

        if (data?.AddressID) {
            addressData.AddressID = data.AddressID;
            addressData.DebtorID = debtorId;
            addressData.Latitude = String(form.Latitude.value);
            addressData.Longitude = String(form.Longitude.value);
        }
        else {
            addressData.FK_DebtorID = debtorId;
            addressData.Latitude = Number(form.Latitude.value);
            addressData.Longitude = Number(form.Longitude.value);
        }
        debugger;
        if (onSubmit) {
            onSubmit(addressData);
        }
    };


    return (
        <Modal show={showModel} onHide={handleClose} centered dialogClassName="custom-modal-two">
            <form onSubmit={handleSubmit} ref={formRef}>
                <Modal.Header closeButton className="custom-modal-header border-0">
                    <Modal.Title>Supplier Address Location</Modal.Title>
                </Modal.Header>
                <Modal.Body className="custom-modal-body">
                    <div className="row">
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Country</label>
                                <select
                                    name="FK_CountryID"
                                    required
                                    className="form-select"
                                    value={selectedCountry}
                                    onChange={handleCountryChange}
                                >
                                    <option value="">Select Country</option>
                                    {countryList.map(c => (
                                        <option key={c.CountryID} value={c.CountryID}>{c.CountryName}</option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Province</label>
                                <select
                                    name="FK_ProvinceID"
                                    required
                                    className="form-select"
                                    value={selectedProvince}
                                    onChange={handleProvinceChange}
                                >
                                    <option value="">Select Province</option>
                                    {filteredProvinces.map(p => (
                                        <option key={p.CountryProvinceID} value={p.CountryProvinceID}>{p.ProvinceName}</option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Region</label>
                                <select name="FK_AddressRegionID" required className="form-select" defaultValue={data?.FK_AddressRegionID || ""}>
                                    <option value="">Select Region</option>
                                    {regionList.map(r => (
                                        <option key={r.AddressRegionID} value={r.AddressRegionID}>{r.RegionName}</option>
                                    ))}
                                </select>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Address Type</label>
                                <select name="FK_AddressTypeID" required className="form-select" defaultValue={data?.FK_AddressTypeID || ""}>
                                    <option value="">Select Type</option>
                                    {addressTypeList
                                        .filter(a => a.FK_EntityID === 8)
                                        .map(a => (
                                            <option key={a.AddressTypeID} value={a.AddressTypeID}>{a.Type}</option>
                                        ))}
                                </select>
                            </div>
                        </div>
                        <div className="col-lg-12">
                            <div className="input-blocks">
                                <label>Street Address</label>
                                <input name="StreetAddress" required type="text" defaultValue={data?.StreetAddress} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Locality</label>
                                <input name="Locality" type="text" defaultValue={data?.Locality} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Postal Code</label>
                                <input name="PostalCode" type="text" defaultValue={data?.PostalCode} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Landmark</label>
                                <input name="Landmark" type="text" defaultValue={data?.Landmark} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-3">
                            <div className="input-blocks">
                                <label>Latitude</label>
                                <input name="Latitude" type="number" step="any" defaultValue={data?.Latitude} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-3">
                            <div className="input-blocks">
                                <label>Longitude</label>
                                <input name="Longitude" type="number" step="any" defaultValue={data?.Longitude} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-12">
                            <div className="input-blocks">
                                <label>Notes</label>
                                <textarea name="Notes" defaultValue={data?.Notes} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="form-check mb-3 mt-4">
                                <input
                                    type="checkbox"
                                    className="form-check-input"
                                    id="IsPrimary"
                                    name="IsPrimary"
                                    defaultChecked={data?.IsPrimary || false}
                                />
                                <label className="form-check-label" htmlFor="IsPrimary">
                                    Is Primary
                                </label>
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Valid From</label>
                                <input name="ValidFrom" type="datetime-local" defaultValue={data?.ValidFrom ? data.ValidFrom.substring(0, 16) : ""} className="form-control" />
                            </div>
                        </div>
                        <div className="col-lg-6">
                            <div className="input-blocks">
                                <label>Valid To</label>
                                <input name="ValidTo" type="datetime-local" defaultValue={data?.ValidTo ? data.ValidTo.substring(0, 16) : ""} className="form-control" />
                            </div>
                        </div>
                    </div>
                </Modal.Body>
                <Modal.Footer className="modal-footer-btn d-flex justify-content-end">
                    <button
                        type="button"
                        className="btn btn-cancel me-2"
                        onClick={handleClose}
                    >
                        Cancel
                    </button>
                    <button type="submit" className="btn btn-submit">
                        Submit
                    </button>
                </Modal.Footer>
            </form>
        </Modal>
    );
}

export default DebtorAddressForm;


DebtorAddressForm.propTypes = {
    data: PropTypes.object,
    onSubmit: PropTypes.func.isRequired,
    showModel: PropTypes.bool.isRequired,
    handleClose: PropTypes.func.isRequired,
    countryList: PropTypes.array.isRequired,
    provinceList: PropTypes.array.isRequired,
    regionList: PropTypes.array.isRequired,
    addressTypeList: PropTypes.array.isRequired,
    debtorId: PropTypes.oneOfType([PropTypes.string, PropTypes.number]).isRequired,
};
