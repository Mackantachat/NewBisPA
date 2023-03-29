using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NewBisPA
{
    public partial class ExceptionHandleForm : Form
    {
        public ExceptionHandleForm()
        {
            InitializeComponent();
        }
        public ExceptionHandleForm(string exceptionMessage)
        {
            InitializeComponent();
            label1.Text = exceptionMessage;
        }
    }
}
