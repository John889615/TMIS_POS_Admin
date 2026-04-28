using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Enums
{
    public enum AppErrorCode
    {
        [ErrorMessage("Product already exists.")]
        ProductExists,

        [ErrorMessage("Combination already exists.")]
        CombinationExists,

        [ErrorMessage("Purchase Order already exists.")]
        PurchaseOrderExists,

        [ErrorMessage("No Settings Yet.")]
        SettingsNotFound,

        [ErrorMessage("Menu already exists.")]
        MenuExists,

        [ErrorMessage("Menu Item already exists.")]
        MenuItemExists,

        [ErrorMessage("Menu Item Product already exists.")]
        MenuItemProductExists,

        [ErrorMessage("Stock Request already exists.")]
        StockRequestExists,

        [ErrorMessage("Failed to save the image.")]
        ImageUploadFailed,

        [ErrorMessage("Can't create new location, it already exists.")]
        LocationExists,

        [ErrorMessage("Can't create new creditor, it already exists.")]
        CreditorExists,

        [ErrorMessage("Can't create new debtor, it already exists.")]
        DebtorExists,

        [ErrorMessage("Can't create new category, it already exists.")]
        CategoryExists,

        [ErrorMessage("Can't create new Unit, it already exists.")]
        UnitExists,

        [ErrorMessage("Can't create new email, it already exists.")]
        EmailExists,

        [ErrorMessage("Product not found.")]
        ProductNotFound,

        [ErrorMessage("Purchase Order not found.")]
        PurchaseOrderNotFound,

        [ErrorMessage("Menu not found.")]
        MenuNotFound,

        [ErrorMessage("Debtor Menu not found.")]
        DebtorMenuNotFound,

        [ErrorMessage("Menu Item not found.")]
        MenuItemNotFound,

        [ErrorMessage("Menu Item Product not found.")]
        MenuItemProductNotFound,

        [ErrorMessage("Stock Request not found.")]
        StockRequestNotFound,

        [ErrorMessage("Purchase Order Line not found.")]
        PurchaseOrderLineNotFound,

        [ErrorMessage("Category not found.")]
        CategoryNotFound,

        [ErrorMessage("Category can not be linked to itself.")]
        CategoryMasterError,

        [ErrorMessage("Product type not found.")]
        ProductTypeNotFound,

        [ErrorMessage("Unit type not found.")]
        UnitNotFound,

        [ErrorMessage("Address not found.")]
        AddressNotFound,

        [ErrorMessage("Creditor not found.")]
        DebtorNotFound,

        [ErrorMessage("Debtor not found.")]
        CreditorNotFound,

        [ErrorMessage("Cost center not found.")]
        CostCenterNotFound,

        [ErrorMessage("Address region not found.")]
        AddressRegionNotFound,

        [ErrorMessage("Address type not found.")]
        AddressTypeNotFound,

        [ErrorMessage("Contact not found.")]
        ContactNotFound,

        [ErrorMessage("Contact type not found.")]
        ContactTypeNotFound,

        [ErrorMessage("Country not found.")]
        CountryNotFound,

        [ErrorMessage("Country Province not found.")]
        CountryProvinceNotFound,

        [ErrorMessage("Country Region not found.")]
        CountryRegionNotFound,

        [ErrorMessage("Country Subregion not found.")]
        CountrySubregionNotFound,

        [ErrorMessage("Currency not found.")]
        CurrencyNotFound,

        [ErrorMessage("Dialing Code not found.")]
        DialingCodeNotFound,

        [ErrorMessage("Entity address not found.")]
        EntityAddressNotFound,

        [ErrorMessage("Entity contact not found.")]
        EntityContactNotFound,

        [ErrorMessage("Time Zone not found.")]
        TimeZoneNotFound,

        [ErrorMessage("Payment Type not found.")]
        PaymentTypeNotFound,

        [ErrorMessage("Validation failed.")]
        ValidationError,

        [ErrorMessage("Unauthorized access.")]
        Unauthorized,

        [ErrorMessage("Unexpected server error.")]
        ServerError,

        [ErrorMessage("An unexpected database error has coccured, please contact support.")]
        DatabaseError,

        [ErrorMessage("Authentication response was null.")]
        UnknownError,

        [ErrorMessage("Served as Type Name combination already exists.")]
        ServedAsTypeName,


        [ErrorMessage("Served as Type Name Product combination already exists.")]
        ServedAsTypeNameProduct,

        [ErrorMessage("Resource not found.")]
        NotFound
    }
}
