using CRMInventory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMInventory.ViewModel
{
    public class ProductViewModel
    {
        public ProductViewModel()
        {
            unitCollection = GetUnitCollection();
            itemGroupCollection = GetItemGroupCollection();
        }

        public List<UnitCollection> GetUnitCollection()
        {
            List<UnitCollection> units = new List<UnitCollection>();

            using (invetoryEntities db = new invetoryEntities())
            {
                if (db.unit_master.ToList().Count > 0)
                {
                    foreach (var item in db.unit_master.ToList())
                    {
                        units.Add(new UnitCollection
                        {
                            UnitId = item.id.ToString(),
                            UnitName = item.name
                        });
                    }
                    return units;
                }
                else {
                    return units;
                }
            }
        }

        public static List<ItemGroupCollection> GetItemGroupCollection()
        {
            List<ItemGroupCollection> itemgroups = new List<ItemGroupCollection>();

            using (invetoryEntities db = new invetoryEntities())
            {
                if (db.stock_group_master.ToList().Count > 0)
                {
                    foreach (var item in db.stock_group_master.ToList())
                    {
                        itemgroups.Add(new ItemGroupCollection
                        {
                            ItemGroupId = item.id.ToString(),
                            ItemGroupName = item.name
                        });
                    }
                    return itemgroups;
                }
                else
                {
                    return itemgroups;
                }
            }
        }
        public List<UnitCollection> unitCollection { get;set;}
        public List<ItemGroupCollection> itemGroupCollection { get; set; }
        private string _productname;
        public string ProductName
        {
            get { return _productname; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Item Name is required");
                }
                else
                {
                    _productname = value;
                }
            }
        }

        private string _category;
        public string Category
        {
            get { return _category; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Category is required");
                }
                else
                {
                    _category = value;
                }
            }
        }

        private string _itemgroup;
        public string ItemGroup
        {
            get { return _itemgroup; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Item Group is required");
                }
                else
                {
                    _itemgroup = value;
                }
            }
        }

        private string _unit;
        public string Unit
        {
            get { return _unit; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Unit is required");
                }
                else
                {
                    _unit = value;
                }
            }
        }

        private string _alternetunit;
        public string AlternetUnit
        {
            get { return _alternetunit; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Alternet Unit is required");
                }
                else
                {
                    _alternetunit = value;
                }
            }
        }

        private string _mrp;
        public string MRP
        {
            get { return _mrp; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("MRP is required");
                }
                else
                {
                    _mrp = value;
                }
            }
        }

        private string _rateon;
        public string RateOn
        {
            get { return _rateon; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Rate on is required");
                }
                else
                {
                    _rateon = value;
                }
            }
        }

        private string _taxtype;
        public string TaxType
        {
            get { return _taxtype; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Tax Type required");
                }
                else
                {
                    _taxtype = value;
                }
            }
        }

        private string _salerateon;
        public string SaleRateOn
        {
            get { return _salerateon; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Sale Rate on required");
                }
                else
                {
                    _salerateon = value;
                }
            }
        }
        private string _rate;
        public string Rate
        {
            get { return _rate; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Rate is required");
                }
                else
                {
                    _rate = value;
                }
            }
        }

        private string _disconsumer;
        public string DisConsumer
        {
            get { return _disconsumer; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Dis. Consumer is required");
                }
                else
                {
                    _disconsumer = value;
                }
            }
        }

        private string _disother;
        public string DisOther
        {
            get { return _disother; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Dis. on Other is required");
                }
                else
                {
                    _disother = value;
                }
            }
        }

        private string _disonqty;
        public string DisOnQty
        {
            get { return _disonqty; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Dis. on Qty is required");
                }
                else
                {
                    _disonqty = value;
                }
            }
        }

        private string _brokerage;
        public string Brokerge
        {
            get { return _brokerage; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Brokerage is required");
                }
                else
                {
                    _brokerage = value;
                }
            }
        }

        private string _brokerageper;
        public string BrokergePer
        {
            get { return _brokerageper; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Brokerage Per is required");
                }
                else
                {
                    _brokerageper = value;
                }
            }
        }

        private string _minqty;
        public string MinQty
        {
            get { return _minqty; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Min Qty is required");
                }
                else
                {
                    _minqty = value;
                }
            }
        }

        private string _orderqty;
        public string OrderQty
        {
            get { return _orderqty; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Order Qty is required");
                }
                else
                {
                    _orderqty = value;
                }
            }
        }

        private string _adhat;
        public string Adhat
        {
            get { return _adhat; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Adhat is required");
                }
                else
                {
                    _adhat = value;
                }
            }
        }

        private string _adhatper;
        public string AdhatPer
        {
            get { return _adhatper; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Per is required");
                }
                else
                {
                    _adhatper = value;
                }
            }
        }

        private string _taulai;
        public string Taulai
        {
            get { return _taulai; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Taulai is required");
                }
                else
                {
                    _taulai = value;
                }
            }
        }

        private string _taulaiper;
        public string TaulaiPer
        {
            get { return _taulaiper; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Per is required");
                }
                else
                {
                    _taulaiper = value;
                }
            }
        }

        private string _majoori;
        public string Majoori
        {
            get { return _majoori; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Majoori is required");
                }
                else
                {
                    _majoori = value;
                }
            }
        }


        private string _majooriper;
        public string MajooriPer
        {
            get { return _majooriper; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Per is required");
                }
                else
                {
                    _majooriper = value;
                }
            }
        }

        private string _packing;
        public string Packing
        {
            get { return _packing; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Packing is required");
                }
                else
                {
                    _packing = value;
                }
            }
        }

        private string _packingper;
        public string PackingPer
        {
            get { return _packingper; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Per is required");
                }
                else
                {
                    _packingper = value;
                }
            }
        }

        private string _mundisulk;
        public string MundiSulk
        {
            get { return _mundisulk; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Mundi Sulk is required");
                }
                else
                {
                    _mundisulk = value;
                }
            }
        }

        private string _mundisulkper;
        public string MundiSulkPer
        {
            get { return _mundisulkper; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Per is required");
                }
                else
                {
                    _mundisulkper = value;
                }
            }
        }

        private string _vikashsesh;
        public string VikashSesh
        {
            get { return _vikashsesh; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Vikash Sesh is required");
                }
                else
                {
                    _vikashsesh = value;
                }
            }
        }

        private string _vikashseshper;
        public string VikashSeshPer
        {
            get { return _vikashseshper; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Per is required");
                }
                else
                {
                    _vikashseshper = value;
                }
            }
        }

        private string _packingless;
        public string PackingLess
        {
            get { return _packingless; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Packing Less is required");
                }
                else
                {
                    _packingless = value;
                }
            }
        }

        private string _packinglessper;
        public string PackingLessPer
        {
            get { return _packinglessper; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Per is required");
                }
                else
                {
                    _packinglessper = value;
                }
            }
        }
    }

    public class UnitCollection
    {
        public string UnitId { get; set; }
        public string UnitName { get; set; }
    }


    public class ItemGroupCollection
    {
        public string ItemGroupId { get; set; }
        public string ItemGroupName { get; set; }
    }
}
