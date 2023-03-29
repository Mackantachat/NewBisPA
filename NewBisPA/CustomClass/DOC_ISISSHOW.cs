using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NewBisPA.CustomClass
{
    public class DOC_ISISSHOW
    {
        private string _PAGES; 
        public string PAGES
        {
            get { return _PAGES; }
            set { _PAGES = value; }
        }

        private string _IDDOC;
        public string IDDOC
        {
            get { return _IDDOC; }
            set { _IDDOC = value; }
        }

        private string _RECEIVE_ISIS_DT;
        public string RECEIVE_ISIS_DT
        {
            get { return _RECEIVE_ISIS_DT; }
            set { _RECEIVE_ISIS_DT = value; }
        }
    }

    public class DOC_ISISSHOW_Collection : List<DOC_ISISSHOW> { }
}
