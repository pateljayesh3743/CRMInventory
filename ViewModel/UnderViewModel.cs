using CRMInventory.Infrastructure;
using Microsoft.VisualStudio.PlatformUI;
using System;

namespace CRMInventory.ViewModel
{
    public class UnderViewModel : ObservableObject 
    {
        public PagingCollectionView pagingCollectionView { get; set; }
        public string UnderId { get; set; }
        private string _undername;
        public string UnderName
        {
            get { return _undername; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Under Name is required");
                }
                else
                {
                    _undername = value;
                }
            }
        }
    }
}
