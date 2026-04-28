using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace POS_Common.Models.BusinessCentral
{
    public class BusinessCentralCreditorList
    {
        [JsonPropertyName("value")]
        public List<BusinessCentralCreditor> Value { get; set; }
    }

    public class BusinessCentralCreditor
    {
        [JsonPropertyName("@odata.etag")]
        public string ODataEtag { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("number")]
        public string Number { get; set; }

        [JsonPropertyName("displayName")]
        public string DisplayName { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("addressLine1")]
        public string AddressLine1 { get; set; }

        [JsonPropertyName("addressLine2")]
        public string AddressLine2 { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        [JsonPropertyName("country")]
        public string Country { get; set; }

        [JsonPropertyName("postalCode")]
        public string PostalCode { get; set; }

        [JsonPropertyName("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("website")]
        public string Website { get; set; }

        [JsonPropertyName("contact")]
        public string Contact { get; set; }

        [JsonPropertyName("balance")]
        public decimal? Balance { get; set; }

        [JsonPropertyName("lastModifiedDateTime")]
        public DateTime? LastModifiedDateTime { get; set; }

        [JsonPropertyName("currencyCode")]
        public string CurrencyCode { get; set; }

        [JsonPropertyName("currencyId")]
        public string CurrencyId { get; set; }

        [JsonPropertyName("paymentTermsId")]
        public string PaymentTermsId { get; set; }

        [JsonPropertyName("paymentMethodId")]
        public string PaymentMethodId { get; set; }

        [JsonPropertyName("blocked")]
        public string Blocked { get; set; }

        [JsonPropertyName("taxRegistrationNumber")]
        public string TaxRegistrationNumber { get; set; }

        [JsonPropertyName("vendorPostingGroupId")]
        public string VendorPostingGroupId { get; set; }

        [JsonPropertyName("vendorPostingGroupDisplayName")]
        public string VendorPostingGroupDisplayName { get; set; }

        [JsonPropertyName("genBusPostingGroupId")]
        public string GenBusPostingGroupId { get; set; }

        [JsonPropertyName("genBusPostingGroupDisplayName")]
        public string GenBusPostingGroupDisplayName { get; set; }

        [JsonPropertyName("vatBusPostingGroupId")]
        public string VatBusPostingGroupId { get; set; }

        [JsonPropertyName("vatBusPostingGroupDisplayName")]
        public string VatBusPostingGroupDisplayName { get; set; }

        [JsonPropertyName("locationCode")]
        public string LocationCode { get; set; }

        [JsonPropertyName("purchases")]
        public decimal? Purchases { get; set; }

        [JsonPropertyName("payments")]
        public decimal? Payments { get; set; }

        [JsonPropertyName("payToVendorNo")]
        public string PayToVendorNo { get; set; }

        [JsonPropertyName("taxLiable")]
        public bool? TaxLiable { get; set; }
    }
}
