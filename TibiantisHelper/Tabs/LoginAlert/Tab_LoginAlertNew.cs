using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TibiantisHelper.Tabs.LoginAlert
{
    public partial class Tab_LoginAlertNew : UserControl
    {

        static string file_loginAlert = "TrackedPlayers.xml";
        public List<TrackedPlayerGroup> PlayerGroups;


        public Tab_LoginAlertNew()
        {
            PlayerGroups = new List<TrackedPlayerGroup>();

            InitializeComponent();

            LoadGroups();
        }



        private void button_add_Click(object sender, EventArgs e)
        {

            var diag = new Form_AddPlayerGroupDialog();

            if (diag.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(diag.GroupName))
                    NewPlayerGroup(diag.GroupName);
            }

        }
    
    
        private void NewPlayerGroup(string name)
        {
            var group = new TrackedPlayerGroup() { Name = name };
            PlayerGroups.Add(group);
            Populate();
        }

        private void AddGroupControl(TrackedPlayerGroup group)
        {
            var groupControl = new Control_TrackedPlayerGroup(group);
            splitContainer1.Panel2.Controls.Add(groupControl);
            groupControl.Dock = DockStyle.Top;
        }

        private void Populate()
        {
            splitContainer1.Panel2.Controls.Clear();
            foreach (var group in PlayerGroups)
                AddGroupControl(group);
        }


        #region File I/O

        private void LoadGroups()
        {
            /*
             * 
            DataTable table = new DataTable();
            table.TableName = "Player";

            table.Columns.AddRange(new DataColumn[] {
                new DataColumn("Name"),
                new DataColumn("Alert")
            });

            try
            {
                table.ReadXml(file_loginAlert);
            }
            catch (FileNotFoundException ex)
            {
                return;
            }
            catch (Exception ex)
            {
                switch (MessageBox.Show(ex.Message, "Could not read Login Tracker data", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error))
                {
                    case DialogResult.Retry:
                        InitializeLoginAlert();
                        break;
                }
            }

            foreach (DataRow p in table.Rows)
            {
                string name, alert;
                name = alert = null;

                if (p.ItemArray[0].GetType() != typeof(DBNull)) name = (string)p.ItemArray[0];
                if (p.ItemArray[1].GetType() != typeof(DBNull)) alert = (string)p.ItemArray[1];

                var player = new TrackedPlayer(name, alert);
                _trackedPlayers.Add(player);


            }

            _trackedPlayers = _trackedPlayers.OrderBy(p => p.Name).ToList();

            loginAlert_dataGridView.DataSource = table;

             */
        }

        #endregion

    }

    public class TrackedPlayerGroup
    {
        public string Name;
        public int Icon = 0;
        public List<string> Players;
    }

}
