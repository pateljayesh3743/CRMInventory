using CRMInventory.Infrastructure;
using Microsoft.VisualStudio.PlatformUI;
using System;

namespace CRMInventory.ViewModel
{
    public class StockGroupViewModel : ObservableObject 
    {
        public PagingCollectionView pagingCollectionView { get; set; }
        
        private string _stockgroupname;
        public string StockGroupName
        {
            get { return _stockgroupname; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Stock Group Name is required");
                }
                else
                {
                    _stockgroupname = value;
                }
            }
        }
    }
}
