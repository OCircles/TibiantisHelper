using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TibiantisHelper.Properties;
using static TibiantisHelper.Form_Main;

namespace TibiantisHelper.Tabs
{
    public partial class Tab_LoginAlert : UserControl
    {

        static string file_loginAlert = "TrackedPlayers.xml";

        public static List<TrackedPlayer> _trackedPlayers;

        public Tab_LoginAlert()
        {
            _trackedPlayers = new List<TrackedPlayer>();

            InitializeComponent();
        }


        private void Tab_LoginAlert_Load(object sender, EventArgs e)
        {
            if (DesignMode)
                return;

            if (!string.IsNullOrEmpty(Settings.Default.LoginAlertNotifSoundPath))
                loginAlert_alertTextBox.Text = Path.GetFileName(Settings.Default.LoginAlertNotifSoundPath);

            InitializeLoginAlert();
        }




        private void InitializeLoginAlert()
        {
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

        }


        private void AddPlayerDialog()
        {
            var diag = new Form_LoginAlertAddDialog();

            if (diag.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(diag.PlayerName))
                    AddPlayer(diag.PlayerName, diag.Alert);
            }
        }

        public void AddPlayer(string player, string alert = null)
        {
            TrackedPlayer p = new TrackedPlayer(player, alert);
            _trackedPlayers.Add(p);
            _trackedPlayers = _trackedPlayers.OrderBy(pp => pp.Name).ToList();
            LoginTrackerUpdate();
            SaveData();
        }


        public void CheckOnline()
        {
            DataTable table = (DataTable)loginAlert_dataGridView.DataSource;
            List<TrackedPlayer> alerts = new List<TrackedPlayer>();



            foreach (var p in _trackedPlayers)
            {
                bool needsUpdate = false;

                // Some stuff for when user has put the tracked players name in the wrong casing
                var onlinePlayer = _currentlyOnline.Where(s => s.Name.Equals(p.Name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                if (!onlinePlayer.Equals(default(Player)))
                {
                    if (!onlinePlayer.Name.Equals(p.Name, StringComparison.Ordinal))
                    {
                        // If wrong casing, fix it
                        p.Name = onlinePlayer.Name;
                        needsUpdate = true;
                    }

                    DataRow[] select = table.Select("Name='" + onlinePlayer.Name + "'");
                    if (select[0] != null)
                    {
                        // Tracked player is online and everything checks out. Do the stuff

                        if (!_lastOnline.Contains(p.Name))
                        {
                            // Wasn't online before so run the alert
                            _lastOnline.Add(p.Name);
                            alerts.Add(p);
                        }

                        // Color row green :)
                        var index = table.Rows.IndexOf(select[0]);
                        loginAlert_dataGridView.Rows[index].DefaultCellStyle.BackColor = Color.PaleGreen;

                        if (needsUpdate)
                            loginAlert_dataGridView.Rows[index].Cells[0].Value = onlinePlayer.Name; // Already updated name in _trackedPlayers, also update i

                    }
                }
                else
                {
                    // Not online
                    if (_lastOnline.Contains(p.Name))
                    {
                        // Was online before
                        _lastOnline.Remove(p.Name);

                        DataRow[] select = table.Select("Name='" + p.Name + "'");
                        if (select[0] != null)
                        {
                            DataRow row = (select[0]);
                            var index = table.Rows.IndexOf(row);
                            loginAlert_dataGridView.Rows[index].DefaultCellStyle.BackColor = Color.White;
                        }
                    }
                }

            }


            LoginAlert(alerts);

        }

        private void LoginAlert(List<TrackedPlayer> players)
        {
            if (players.Count != 0)
            {
                string alertSound = "";
                string alertString = "";

                int lastShownIndex = 0;

                if (players.Count == 2) lastShownIndex = 1;
                else if (players.Count >= 3) lastShownIndex = 2;

                for (int i = 0; i < players.Count; i++)
                {
                    if (alertSound == "")
                        if (players[i].Alert != null)
                        {
                            if (File.Exists(players[i].Alert))
                                alertSound = players[i].Alert;

                        }

                    if (i < 3)
                    {
                        alertString += players[i].Name;

                        if (lastShownIndex != 0)
                        {
                            if (i == lastShownIndex - 1) alertString += " and ";
                            else if (i != lastShownIndex) alertString += ", ";
                        }
                    }
                }

                if (players.Count == 1) alertString += " has ";
                else
                {
                    if (players.Count > 3) alertString += $" (plus {players.Count - 3} more) ";
                    alertString += " have ";
                }

                alertString += "logged in!";

                if (Settings.Default.LoginAlertNotifTray)
                    Form_Main.Tray_ShowBubble(TrayBubbleBehaviour.None, 5000, "Tibiantis Login", alertString, ToolTipIcon.Warning);

                if (alertSound == "" && Settings.Default.LoginAlertNotifSound && !string.IsNullOrEmpty(Settings.Default.LoginAlertNotifSoundPath))
                    alertSound = Settings.Default.LoginAlertNotifSoundPath;

                if (alertSound != "")
                {
                    try
                    {
                        SoundPlayer alertPlayer = new SoundPlayer(alertSound);
                        alertPlayer.Play();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Could not play alert sound", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        private void LoginAlertRemoveSelected()
        {
            if (loginAlert_dataGridView.SelectedCells.Count != 0)
            {
                var index = loginAlert_dataGridView.SelectedCells[0].RowIndex;
                var selectedName = (string)loginAlert_dataGridView.Rows[index].Cells[0].Value;

                var ooc = _trackedPlayers.Where(p => p.Name == (string)loginAlert_dataGridView.Rows[index].Cells[0].Value).FirstOrDefault();

                if (ooc != null)
                {
                    if (MessageBox.Show(
                        $"Are you sure you want to remove {ooc.Name} from login alert?",
                        "Login Alert Remove",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    == DialogResult.Yes)
                    {
                        _trackedPlayers.Remove(ooc);
                        LoginTrackerUpdate();
                        SaveData();
                    }
                }
            }
        }


        // For adding player via current online form. This should absolutely not exist but I'm lazy
        // Somehow, a more generalized function that takes a string to tabControl.SelectTab() didn't work
        public void OpenLoginAlertTab() { Form_Main.Form.tabControl1.SelectTab(Form_Main.Form.tabPage_loginAlert); }



        #region Sidebar

        private void button_add_Click(object sender, EventArgs e)
        {
            AddPlayerDialog();
        }

        private void button_remove_Click(object sender, EventArgs e)
        {
            LoginAlertRemoveSelected();
        }

        private void button_browseSound_Click(object sender, EventArgs e)
        {
            var diag = new OpenFileDialog();
            diag.RestoreDirectory = true;

            diag.Filter = "WAV files (*.wav)|*.wav";

            if (diag.ShowDialog() == DialogResult.OK)
            {
                loginAlert_alertTextBox.Text = Path.GetFileName(diag.FileName);
                Settings.Default.LoginAlertNotifSoundPath = diag.FileName;
                Settings.Default.Save();
            }
        }

        private void checkBox_notifSound_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.LoginAlertNotifSound = checkBox_notifSound.Checked;
            Settings.Default.Save();
        }

        private void checkBox_notifTray_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Default.LoginAlertNotifTray = checkBox_notifTray.Checked;
            Settings.Default.Save();
        }

        #endregion

        #region Login Alert Data
        private void loginAlert_dataGridView_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            DataGridView grid = (DataGridView)sender;

            grid.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            grid.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }
        public void SaveData()
        {
            if (loginAlert_dataGridView.DataSource != null)
            {
                DataTable t = (DataTable)loginAlert_dataGridView.DataSource;
                t.WriteXml(file_loginAlert);
            }
        }
        private void LoginTrackerUpdate()
        {
            DataTable table = new DataTable();

            table.TableName = "Player";

            table.Columns.AddRange(new DataColumn[] {
                new DataColumn("Name"),
                new DataColumn("Alert")
            });

            foreach (var p in _trackedPlayers)
            {
                table.Rows.Add(p.Name, p.Alert);
            }

            loginAlert_dataGridView.DataSource = table;


        }

        private void loginAlert_dataGridView_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            // Unfortunately need this because rows can't be colored before they have been shown
            // Alternatively you could change the tab to Login Alert and back on startup, which would be better performance-wise, but
            // you'd have to do it with this.Visible = true, so it ends up just looking kind of ugly..

            for (int i = e.RowIndex; i < e.RowIndex + e.RowCount; i++)
            {
                LoginAlertColorRow(i);

                if (loginAlert_dataGridView.Rows[i].Cells[1].Value.GetType() == typeof(DBNull))
                    loginAlert_dataGridView.Rows[i].Cells[1].Value = "Default";
            }
        }

        private void LoginAlertColorRow(int index)
        {
            if (!_currentlyOnline.FirstOrDefault(i => i.Name == (string)(loginAlert_dataGridView.Rows[index].Cells[0].Value)).Equals(default(Player)))
                loginAlert_dataGridView.Rows[index].DefaultCellStyle.BackColor = Color.PaleGreen;
            else
                loginAlert_dataGridView.Rows[index].DefaultCellStyle.BackColor = Color.White;
        }

        private void loginAlert_dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (loginAlert_dataGridView.SelectedCells.Count != 0)
            {
                var row = loginAlert_dataGridView.SelectedCells[0].RowIndex;
                label_selectedPlayer.Text = (string)loginAlert_dataGridView.Rows[row].Cells[0].Value;
            }
        }

        private void loginAlert_dataGridView_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DataGridView.HitTestInfo hit = loginAlert_dataGridView.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.Cell)
                {
                    loginAlert_dataGridView.ClearSelection();

                    var clickedCell = loginAlert_dataGridView.Rows[hit.RowIndex].Cells[hit.ColumnIndex];
                    clickedCell.Selected = true;

                    var player = _trackedPlayers.Where(p => p.Name == (string)loginAlert_dataGridView.Rows[clickedCell.RowIndex].Cells[0].Value).FirstOrDefault();

                    if (player != null)
                    {
                        ContextMenu menu = new ContextMenu();


                        var add = new MenuItem("Add", (s, ee) => { AddPlayerDialog(); });
                        var remove = new MenuItem("Remove", (s, ee) => {
                            _trackedPlayers.Remove(player);
                            LoginTrackerUpdate();
                            SaveData();
                        });
                        var alert = new MenuItem("Alert");
                        var alertChange = new MenuItem("Change", (s, ee) => {
                            LoginAlertChangeAlertDialog(player);
                            LoginTrackerUpdate();
                            SaveData();
                        });
                        var alertClear = new MenuItem("Clear", (s, ee) => {
                            player.Alert = null;
                            LoginTrackerUpdate();
                            SaveData();
                        });

                        alert.MenuItems.Add(alertChange);
                        alert.MenuItems.Add(alertClear);


                        menu.MenuItems.Add(add);
                        menu.MenuItems.Add(remove);
                        menu.MenuItems.Add(alert);

                        menu.Show((Control)sender, new Point(e.X, e.Y));

                    }
                }
            }
        }

        private void loginAlert_dataGridView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
                LoginAlertRemoveSelected();
        }

        private void loginAlert_dataGridView_ColumnAdded(object sender, DataGridViewColumnEventArgs e)
        {
            // Sorting was causing an issue where all the rows would get colored as if they were all online for some reason...
            e.Column.SortMode = DataGridViewColumnSortMode.NotSortable;
        }



        // Clicking cells stuff
        string beforeLoginAlertNameEdit = "";
        private void loginAlert_dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            loginAlert_dataGridView.BeginEdit(true);
        }

        private void loginAlert_dataGridView_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {
            if (e.ColumnIndex == 0)
            {
                beforeLoginAlertNameEdit = (string)loginAlert_dataGridView.Rows[e.RowIndex].Cells[0].Value;
            }

            if (e.ColumnIndex == 1)
            {
                e.Cancel = true;

                var player = _trackedPlayers.Where(p => p.Name == (string)loginAlert_dataGridView.Rows[e.RowIndex].Cells[0].Value).FirstOrDefault();

                if (player != null)
                    LoginAlertChangeAlertDialog(player);
            }
        }

        private void LoginAlertChangeAlertDialog(TrackedPlayer player)
        {
            var diag = new OpenFileDialog();
            diag.RestoreDirectory = true;

            diag.Filter = "WAV files (*.wav)|*.wav";

            if (diag.ShowDialog() == DialogResult.OK)
            {
                player.Alert = diag.FileName;
                LoginTrackerUpdate();
            }
        }

        private void loginAlert_dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                e.Value = Path.GetFileName((string)e.Value);
            }
        }

        private void loginAlert_dataGridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            var player = _trackedPlayers.Where(p => p.Name == beforeLoginAlertNameEdit).FirstOrDefault();

            if (player != null && player.Name != beforeLoginAlertNameEdit)
            {
                player.Name = (string)loginAlert_dataGridView.Rows[e.RowIndex].Cells[0].Value;
                LoginTrackerUpdate();
            }
        }
        #endregion

        public class TrackedPlayer
        {
            public string Name;
            public string Alert;        // Leaving this uninitialized is good for DataTable.WriteXml, avoids writing empty xml nodes

            public TrackedPlayer(string Name, string Alert = null)
            {
                this.Name = Name;
                this.Alert = Alert;
            }
        }
    }
}
