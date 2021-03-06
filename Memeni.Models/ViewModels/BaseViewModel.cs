using Memeni.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Memeni.Models.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class BaseViewModel
    {//Memeni: make note that this base class does not have to be, or should not be abstract. 
        // it is a perfectly good class to be used as a ViewModel 
        
        public bool IsLoggedIn { get; set; }

        private List<PageMetaTags> _pageTags = new List<PageMetaTags>();
        public List<PageMetaTags> PageTags {
            get { return _pageTags; }
            set { _pageTags = value; }
        }

        private List<Help> _helpList = new List<Help>();
        public List<Help> HelpList {
            get { return _helpList; }
            set { _helpList = value; }
        }
    }
}
