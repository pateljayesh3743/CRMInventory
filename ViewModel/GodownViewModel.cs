using CRMInventory.Infrastructure;
using Microsoft.VisualStudio.PlatformUI;
using System;

namespace CRMInventory.ViewModel
{
    public class GodownViewModel : ObservableObject 
    {
        public PagingCollectionView pagingCollectionView { get; set; }
        
        private string _godownname;
        public string GodownName
        {
            get { return _godownname; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Godown Name is required");
                }
                else
                {
                    _godownname = value;
                }
            }
        }

        private string _address;
        public string Address
        {
            get { return _address; }
            set
            {
                _address = value;
            }
        }
    }
}
