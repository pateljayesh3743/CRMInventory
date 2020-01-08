using CRMInventory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMInventory.ViewModel
{
    public class PurchaseViewModel
    {
        public PurchaseViewModel()
        {
            productCollection = GetProductCollection();
            partyCollection = GetPartyCollection();
        }

        public List<PartyViewModel> partyCollection { get; set; }
        public static List<PartyViewModel> GetPartyCollection()
        {
            List<PartyViewModel> parties = new List<PartyViewModel>();

            using (invetoryEntities db = new invetoryEntities())
            {
                if (db.ledger_master.ToList().Count > 0)
                {
                    foreach (var item in db.ledger_master.ToList())
                    {
                        parties.Add(new PartyViewModel
                        {
                            party_id = item.id,
                            party_name = item.name
                        });
                    }
                    return parties;
                }
                else
                {
                    return parties;
                }
            }
        }
        public List<ProductCollection> productCollection { get; set; }
        public static List<ProductCollection> GetProductCollection()
        {
            List<ProductCollection> products = new List<ProductCollection>();

            using (invetoryEntities db = new invetoryEntities())
            {
                if (db.product_master.ToList().Count > 0)
                {
                    foreach (var item in db.product_master.ToList())
                    {
                        products.Add(new ProductCollection
                        {
                            ProductId = item.id,
                            ProductName = item.product
                        });
                    }
                    return products;
                }
                else
                {
                    return products;
                }
            }
        }
    }
}
