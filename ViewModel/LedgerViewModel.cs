using CRMInventory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMInventory.ViewModel
{
    public class LedgerViewModel
    {
        public LedgerViewModel()
        {
            underCollection = GetUnderCollection();
            creditDebitTypeCollection = GetCreditDebitTypeCollection();
            interestCollection = GetInterestCollection();
        }
        public List<UnderViewModel> underCollection { get; set; }
        public List<CreditDebitType> creditDebitTypeCollection { get; set; }

        public List<Interest> interestCollection { get; set; }
        public static List<UnderViewModel> GetUnderCollection()
        {
            List<UnderViewModel> unders = new List<UnderViewModel>();

            using (invetoryEntities db = new invetoryEntities())
            {
                if (db.under_master.ToList().Count > 0)
                {
                    foreach (var item in db.under_master.ToList())
                    {
                        unders.Add(new UnderViewModel
                        {
                            UnderId = item.id.ToString(),
                            UnderName = item.name
                        });
                    }
                    return unders;
                }
                else
                {
                    return unders;
                }
            }
        }
        public static List<CreditDebitType> GetCreditDebitTypeCollection()
        {
            List<CreditDebitType> creditdebittypes = new List<CreditDebitType>();
            creditdebittypes.Add(new CreditDebitType { id="1",name="CR"});
            creditdebittypes.Add(new CreditDebitType { id = "2", name = "DR" });
            return creditdebittypes;
        }

        public static List<Interest> GetInterestCollection()
        {
            List<Interest> interest = new List<Interest>();
            interest.Add(new Interest { id = 1, name = "Yes" });
            interest.Add(new Interest { id = 0, name = "No" });
            return interest;
        }
    }
    public class CreditDebitType
    {
        public string id { get; set; }
        public string name { get; set; }
    }
    public class Interest
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}
