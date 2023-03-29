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
    public partial class AppNameForm : UserControl
    {
        public AppNameForm()
        {
            InitializeComponent();
        }

        public TextBox txtAppNameQuestion
        {
            get { return txtQuestion; }
            set { txtQuestion = value; }
        }
        public ComboBox cmbAppNameAnswer
        {
            get { return cmbAnswer; }
            set { cmbAnswer = value; }
        }

    }
}
