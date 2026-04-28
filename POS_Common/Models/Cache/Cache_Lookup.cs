using POS_Common.Models.Debtors.Debtors;
using POS_Common.Models.Debtors.POS_CostCenters;
using POS_Common.Models.Debtors.POS_Locations;
using POS_Common.Models.EntityData.AddressRegions;
using POS_Common.Models.EntityData.AddressTypes;
using POS_Common.Models.EntityData.ContactTypes;
using POS_Common.Models.EntityData.Countries;
using POS_Common.Models.EntityData.CountryProvinces;
using POS_Common.Models.EntityData.Currencies;
using POS_Common.Models.EntityData.DialingCodes;
using POS_Common.Models.EntityData.GlobalSettings;
using POS_Common.Models.EntityData.POS_PaymentTypeIcons;
using POS_Common.Models.EntityData.POS_SlipTypes;
using POS_Common.Models.EntityData.POS_TaxTypes;
using POS_Common.Models.EntityData.Users;
using POS_Common.Models.Inventory.POS_ProductExtraCategories;
using POS_Common.Models.Inventory.POS_ProductPreparation;
using POS_Common.Models.Inventory.POS_ProductPreparationMethods;
using POS_Common.Models.Inventory.POS_Products;
using POS_Common.Models.Stock.POS_PriceCodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS_Common.Models.Cache
{
    public class Cache_Lookup
    {
        #region Properties

        public List<Country> Countries { get; set; }

        public List<CountryProvince> Provinces { get; set; }

        public List<AddressRegion> AddressRegions { get; set; }

        public List<DialingCode> DialingCodes { get; set; }

        public List<AddressType> AddressTypes { get; set; }

        public List<ContactType> ContactTypes { get; set; }

        public List<CostCenter> CostCenter { get; set; }

        public List<Debtor> Debtor { get; set; }

        public List<Location> Location { get; set; }

        public List<TaxType> Tax { get; set; }

        public List<Currency> Currencies { get; set; }

        public List<User> User { get; set; }

        public List<Product> Products { get; set; }

        public List<ProductExtraCategory> ProductCategories { get; set; }

        public List<ProductPreparationMethod> ProductPreparationMethods { get; set; }

        public List<PaymentTypeIcon> PaymentTypeIcon { get; set; }

        public List<PriceCodes> PriceCodes { get; set; }

        public List<GlobalSettings> GlobalSettings { get; set; }

        public List<SlipType> SlipTypes { get; set; }

        public DateTime LastUpdated { get; set; } = DateTime.Now;
        #endregion
    }
}
