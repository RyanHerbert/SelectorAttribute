using System.Collections.Generic;
using System.Web.Mvc;

namespace SelectorAttribute
{
    public class Selector
    {
        public IEnumerable<SelectListItem> Items { get; set; }

        public string OptionLabel { get; set; }

        public bool AllowMultipleSelection { get; set; }

        public int BulkSelectionThreshold { get; set; }        
    }
}
