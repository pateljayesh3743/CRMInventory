using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.VisualStudio.PlatformUI;
using CRMInventory.Infrastructure;

namespace CRMInventory.ViewModel
{
    public class UnitViewModel : ObservableObject 
    {
        public PagingCollectionView pagingCollectionView { get; set; }
        
        private string _unitname;
        public string UnitName
        {
            get { return _unitname; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Unit Name is required");
                }
                else
                {
                    _unitname = value;
                }
            }
        }

        private string _shortcode;
        public string ShortCode
        {
            get { return _shortcode; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException("Unit Code is required");
                }
                else
                {
                    _shortcode = value;
                }
            }
        }

    }
}
