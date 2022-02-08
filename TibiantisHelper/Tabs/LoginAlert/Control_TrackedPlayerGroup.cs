using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TibiantisHelper
{
    public partial class Control_TrackedPlayerGroup : UserControl
    {
        public Control_TrackedPlayerGroup(Tabs.LoginAlert.TrackedPlayerGroup group)
        {
            InitializeComponent();

            button_edit.FlatAppearance.BorderSize = 0;

            label_name.Text = group.Name;
        }
    }
}
