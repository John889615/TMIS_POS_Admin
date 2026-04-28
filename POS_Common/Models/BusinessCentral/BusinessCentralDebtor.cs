using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace POS_Common.Models.BusinessCentral
{
    public class BusinessCentralDebtorList
    {
        [JsonPropertyName("value")]
        public List<BusinessCentralDebtor> Value { get; set; }
    }

    public class BusinessCentralDebtor
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

        [JsonPropertyName("salespersonCode")]
        public string SalespersonCode { get; set; }

        [JsonPropertyName("balanceDue")]
        public decimal? BalanceDue { get; set; }

        [JsonPropertyName("creditLimit")]
        public decimal? CreditLimit { get; set; }

        [JsonPropertyName("taxLiable")]
        public bool? TaxLiable { get; set; }

        [JsonPropertyName("taxAreaId")]
        public string TaxAreaId { get; set; }

        [JsonPropertyName("taxAreaDisplayName")]
        public string TaxAreaDisplayName { get; set; }

        [JsonPropertyName("taxRegistrationNumber")]
        public string TaxRegistrationNumber { get; set; }

        [JsonPropertyName("currencyId")]
        public string CurrencyId { get; set; }

        [JsonPropertyName("currencyCode")]
        public string CurrencyCode { get; set; }

        [JsonPropertyName("paymentTermsId")]
        public string PaymentTermsId { get; set; }

        [JsonPropertyName("shipmentMethodId")]
        public string ShipmentMethodId { get; set; }

        [JsonPropertyName("paymentMethodId")]
        public string PaymentMethodId { get; set; }

        [JsonPropertyName("blocked")]
        public string Blocked { get; set; }

        [JsonPropertyName("lastModifiedDateTime")]
        public DateTime? LastModifiedDateTime { get; set; }
    }
}
