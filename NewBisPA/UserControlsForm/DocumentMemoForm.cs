using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NewBisPA.UserControlsForm
{
    public partial class DocumentMemoForm : UserControl
    {
        public DocumentMemoForm()
        {
            InitializeComponent();
        }
        public CheckBox ckbMoDocument
        {
            get { return ckbMemoDocument; }
            set { ckbMemoDocument = value; }
        }
        public TextBox txtDocCode
        {
            get { return txtDocumentCode; }
            set { txtDocumentCode = value; }
        }
    }
}
