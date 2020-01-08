using CRMInventory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRMInventory.ViewModel
{
    public class StockViewModel
    {
        public StockViewModel()
        {
            transactionTypeCollections = GetTransactionTypeCollection();
            productCollections = GetProductCollection();
        }
        public int stock_detail_id { get; set; }
        
        private string _product_id;
        public string product_id
        {
            get { return _product_id; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Product is required");
                }
                else
                {
                    _product_id = value;
                }
            }
        }
        public string product_name { get; set; }

        
        private string _price;
        public string price
        {
            get { return _price; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Price is required");
                }
                else
                {
                    _price = value;
                }
            }
        }

        private string _quantity;
        public string quantity
        {
            get { return _quantity; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Quantity is required");
                }
                else
                {
                    _quantity = value;
                }
            }
        }
        private string _transaction_type;
        public string transaction_type
        {
            get { return _transaction_type; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Transaction Type is required");
                }
                else
                {
                    _transaction_type = value;
                }
            }
        }
        

        public List<TransactionTypeCollection> transactionTypeCollections { get; set; }

        public List<ProductCollection> productCollections { get; set; }

        public List<TransactionTypeCollection> GetTransactionTypeCollection()
        {
            List<TransactionTypeCollection> transactionTypes = new List<TransactionTypeCollection>();

            transactionTypes.Add(new TransactionTypeCollection
            {
                TransactionTypeId = 1,
                TransactionTypeName = "Purchase"
            });

            transactionTypes.Add(new TransactionTypeCollection
            {
                TransactionTypeId = 2,
                TransactionTypeName = "Sales"
            });

            return transactionTypes;
        }

        public List<ProductCollection> GetProductCollection()
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
    public class TransactionTypeCollection
    {
        public int TransactionTypeId { get; set; }
        public string TransactionTypeName { get; set; }
    }

    public class ProductCollection
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
