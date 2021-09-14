using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using TibiantisHelper.Properties;
using static TibiantisHelper.Utility;

namespace TibiantisHelper.Tabs
{
    public partial class Tab_Accounts : UserControl
    {

        public static List<Account> _accounts;
        static string file_accounts = "Accounts.xml";

        public Tab_Accounts()
        {
            InitializeComponent();
            _accounts = ReadAccounts(file_accounts);

            AccountsPopulate();
            if (_accounts.Count != 0)
                AccountsDisplayInfo(_accounts[0]);
        }


        private void AccountAddDialog()
        {
            var diag = new Form_AccountAddDialog();

            if (diag.ShowDialog() == DialogResult.OK)
            {
                var acc = new Account();
                acc.name = diag.AccountName;
                acc.login = diag.AccountLogin;
                acc.premium = diag.AccountPremium;
                acc.house = diag.AccountHouse;

                _accounts.Add(acc);
                AccountsPopulate();
                AccountsSave(file_accounts);
            }
        }
        private void AccountsPopulate()
        {

            DataTable table = new DataTable();

            table.TableName = "Player";

            table.Columns.AddRange(new DataColumn[] {
                new DataColumn("Name"),
                new DataColumn("Account"),
                new DataColumn("Premium Expiry"),
                new DataColumn("House Payment")
            });

            foreach (var a in _accounts)
            {
                var login = a.login;
                if (checkBox_hideAcc.Checked)
                    login = ProtectedString(a.login);

                table.Rows.Add(a.name, login, a.premium, a.house);
            }

            dataGridView.DataSource = table;
        }
        private void AccountsDisplayInfo(Account acc)
        {
            if (acc != null)
            {
                textBox10.Text = $"Name: {acc.name}{Environment.NewLine}Account: {acc.login}";

                DateTime blank = new DateTime();

                if (acc.premium != blank || acc.house != blank) textBox10.Text += $"{Environment.NewLine}{Environment.NewLine}";

                if (acc.premium != blank) textBox10.Text += $"Premium ends in {(acc.premium - DateTime.Today).Days} days " +
                        $"({acc.premium.ToString("d", CultureInfo.CreateSpecificCulture("en-US"))}){Environment.NewLine}{Environment.NewLine}";


                if (acc.house != blank) textBox10.Text += $"House payment in {(acc.house - DateTime.Today).Days} days " +
                        $"({acc.house.ToString("d", CultureInfo.CreateSpecificCulture("en-US"))})";
            }

        }
        private void AccountsRemoveSelected()
        {
            if (dataGridView.SelectedCells.Count != 0)
            {
                var index = dataGridView.SelectedCells[0].RowIndex;
                var selectedName = (string)dataGridView.Rows[index].Cells[0].Value;

                var acc = _accounts.Where(a => a.name == (string)dataGridView.Rows[index].Cells[0].Value).FirstOrDefault();

                if (acc != null)
                {
                    if (MessageBox.Show(
                        $"Are you sure you want to remove {acc.name} from accounts?",
                        "Remove Account",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                    {
                        _accounts.Remove(acc);
                        AccountsPopulate();
                        AccountsSave(file_accounts);
                    }
                }
            }
        }
        public void UpdateSelectedInfo()
        {
            // Called just to update already displayed information when un-minimizing/switching tab
            var acc = _accounts.Where(a => a.name == accounts_label_selected.Text).FirstOrDefault();
            AccountsDisplayInfo(acc);
        }

        #region Sidebar
        private void button_addAccount_Click(object sender, EventArgs e)
        {
            AccountAddDialog();
        }

        private void button_removeAccount_Click(object sender, EventArgs e)
        {
            AccountsRemoveSelected();
        }
        
        private void checkBox_hideAcc_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.AccountsHideLogin = checkBox_hideAcc.Checked;
            Settings.Default.Save();

            AccountsPopulate();
        }

        #endregion

        #region Datagrid Events
        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 2 || e.ColumnIndex == 3)
            {
                DateTime blank = new DateTime();
                DateTime actual = DateTime.Parse(e.Value.ToString());

                if (actual == blank)
                {
                    e.Value = "N/A";
                    dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                }
                else
                {
                    e.Value = actual.ToString("d", CultureInfo.CreateSpecificCulture("en-US"));

                    var diff = (actual - DateTime.Today).Days;

                    if (diff <= 3)
                        dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.PaleVioletRed;
                    else if (diff <= 7)
                        dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.LightYellow;
                    else
                        dataGridView.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.BackColor = Color.White;
                }
            }
        }

        private void dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                AccountsRemoveSelected();
        }

        private void dataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView grid = (DataGridView)sender;

            grid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grid.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grid.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView.BeginEdit(true);
        }

        private void dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            e.Cancel = true;

            var acc = _accounts.Where(a => a.name == (string)dataGridView.Rows[e.RowIndex].Cells[0].Value).FirstOrDefault();

            if (acc != null)
            {
                var diag = new Form_AccountAddDialog(acc.name, acc.login, acc.premium, acc.house);

                if (diag.ShowDialog() == DialogResult.OK)
                {
                    acc.name = diag.AccountName;
                    acc.login = diag.AccountLogin;
                    acc.premium = diag.AccountPremium;
                    acc.house = diag.AccountHouse;

                    AccountsDisplayInfo(acc);

                    AccountsPopulate();
                    AccountsSave(file_accounts);
                }
            }

        }

        private void dataGridView_MouseDown(object sender, MouseEventArgs e)
        {

            DataGridView.HitTestInfo hit = dataGridView.HitTest(e.X, e.Y);
            if (hit.Type == DataGridViewHitTestType.Cell)
            {
                dataGridView.ClearSelection();

                var clickedCell = dataGridView.Rows[hit.RowIndex].Cells[hit.ColumnIndex];
                clickedCell.Selected = true;

                var accName = (string)dataGridView.Rows[clickedCell.RowIndex].Cells[0].Value;
                accounts_label_selected.Text = accName;

                var acc = _accounts.Where(a => a.name == accName).FirstOrDefault();

                if (acc != null)
                {

                    AccountsDisplayInfo(acc);

                    if (e.Button == MouseButtons.Right)
                    {

                        if (clickedCell.ColumnIndex == 1)
                            StringToClipboard(acc.login);
                        else
                        {
                            ContextMenu menu = new ContextMenu();

                            var add = new MenuItem("Add", (s, ee) => { AccountAddDialog(); });
                            var remove = new MenuItem("Remove", (s, ee) =>
                            {
                                _accounts.Remove(acc);
                                AccountsPopulate();
                                AccountsSave(file_accounts);
                            });

                            menu.MenuItems.Add(add);
                            menu.MenuItems.Add(remove);


                            DateTime blank = new DateTime();

                            if (acc.premium != blank || acc.house != blank)
                            {
                                var spacer = new MenuItem("-");
                                menu.MenuItems.Add(spacer);

                                if (acc.premium != blank)
                                {
                                    var premium = new MenuItem("Premium");
                                    var premiumAdd30 = new MenuItem("Add 30 days (piggybank)", (s, ee) =>
                                    {
                                        acc.premium = acc.premium.AddDays(30);
                                        AccountsPopulate();
                                        AccountsSave(file_accounts);
                                    });
                                    var premiumAdd33 = new MenuItem("Add 33 days (paypal)", (s, ee) =>
                                    {
                                        acc.premium = acc.premium.AddDays(33);
                                        AccountsPopulate();
                                        AccountsSave(file_accounts);
                                    });

                                    premium.MenuItems.Add(premiumAdd30);
                                    premium.MenuItems.Add(premiumAdd33);

                                    menu.MenuItems.Add(premium);
                                }

                                if (acc.house != blank)
                                {
                                    var house = new MenuItem("House");
                                    var houseAdd30 = new MenuItem("Add 30 days", (s, ee) =>
                                    {
                                        acc.house = acc.house.AddDays(30);
                                        AccountsPopulate();
                                        AccountsSave(file_accounts);
                                    });

                                    house.MenuItems.Add(houseAdd30);

                                    menu.MenuItems.Add(house);

                                }
                            }

                            menu.Show((Control)sender, new Point(e.X, e.Y));
                        }
                    }
                }
            }
        }
        #endregion

        #region File I/O
        public static void AccountsSave(string path)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter xmlWriter = XmlWriter.Create("Accounts.xml", settings);
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Accounts");

            foreach (var a in _accounts)
            {
                xmlWriter.WriteStartElement("Account");

                xmlWriter.WriteStartElement("Name");
                xmlWriter.WriteString(a.name);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("Login");
                xmlWriter.WriteString(a.login);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("Premium");
                xmlWriter.WriteString(a.premium.ToString());
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("HouseDate");
                xmlWriter.WriteString(a.house.ToString());
                xmlWriter.WriteEndElement();

                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();

            xmlWriter.Close();

        }

        private List<Account> ReadAccounts(string path)
        {
            List<Account> accounts = new List<Account>();

            if (File.Exists(path))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);

                foreach (XmlNode xmlNode in xmlDoc.DocumentElement)
                {

                    Account acc = new Account();
                    foreach (XmlNode accNodes in xmlNode.ChildNodes)
                    {
                        if (!String.IsNullOrEmpty(accNodes.InnerText))
                        {

                            switch (accNodes.Name)
                            {
                                case "Name":
                                    acc.name = accNodes.InnerText;
                                    break;
                                case "Account": // Legacy node name, need for migrating v0.96 and earlier xml:s
                                case "Login":
                                    acc.login = accNodes.InnerText;
                                    break;
                                case "Premium_x0020_Expiry": // Also legacy migration thing
                                case "Premium":
                                    acc.premium = DateTime.Parse(accNodes.InnerText);
                                    break;
                                case "House_x0020_Payment": // Also legacy migration thing
                                case "HouseDate":
                                    acc.house = DateTime.Parse(accNodes.InnerText);
                                    break;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(acc.name) && !string.IsNullOrEmpty(acc.login))
                        accounts.Add(acc);
                }
            }

            return accounts;
        }
        #endregion

        private string ProtectedString(string input)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder(input.Length);
            for (int i = 0; i < input.Length; i++)
                sb.Append('*');
            return sb.ToString();
        }

        public class Account
        {

            public string name;
            public string login;
            public DateTime premium;
            public DateTime house;

        }
    }
}
