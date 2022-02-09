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
using System.Xml;
using static TibiantisHelper.Form_Main;

namespace TibiantisHelper.Tabs.LoginAlert
{
    public partial class Tab_LoginAlert : UserControl
    {

        static string file_loginAlert = "TrackedPlayers.xml";
        public static List<TrackedPlayerGroup> PlayerGroups;
        public static List<string> LastOnline;


        public Tab_LoginAlert()
        {
            PlayerGroups = new List<TrackedPlayerGroup>();
            LastOnline = new List<string>();

            InitializeComponent();

            try
            {
                PlayerGroups = LoadGroups(file_loginAlert);
            } catch (Exception ex)
            {
                MessageBox.Show("Could not read " + file_loginAlert + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Populate();
        }



        private void button_add_Click(object sender, EventArgs e)
        {

            var diag = new Form_AddPlayerGroupDialog();

            if (diag.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(diag.GroupName))
                {
                    var grp = new TrackedPlayerGroup() { Name = diag.GroupName };
                    PlayerGroups.Add(grp);
                    AddGroupControl(grp);
                }
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

            group.Control = groupControl;

            groupControl.GroupChanged += (s, e) =>
            {
                SaveGroups(file_loginAlert);
            };

            groupControl.GroupRemove += (s, e) =>
            {
                PlayerGroups.Remove(group);
                SaveGroups(file_loginAlert);
                Populate();
            };

            splitContainer1.Panel2.Controls.Add(groupControl);
            groupControl.Dock = DockStyle.Top;
        }

        public void Populate()
        {
            splitContainer1.Panel2.Controls.Clear();
            foreach (var group in PlayerGroups)
                AddGroupControl(group);
        }



        public void Import(List<string> group, TrackedPlayerGroup destination = null)
        {
            Import(new TrackedPlayerGroup() { Name = "Uncategorized", Players = group }, destination);
        }

        public void Import(TrackedPlayerGroup group, TrackedPlayerGroup destination = null)
        {
            bool repop = false;
            var import = new Form_ImportDialog(group.Players, destination);

            if (import.ShowDialog() == DialogResult.OK)
            {
                TrackedPlayerGroup grp = import.ImportGroupDestination;
                if (grp == null)
                {
                    Console.WriteLine("New");
                    var ng = new TrackedPlayerGroup() { Name = import.ImportNewGroupName, Players = import.ImportPlayers };
                    PlayerGroups.Add(ng);
                    repop = true;
                }
                else
                {
                    foreach (var p in import.ImportPlayers)
                    {
                        if (!destination.Players.Exists(ss => ss == p))
                            destination.Players.Add(p);

                    }
                    destination.Control.RefreshList();
                }
                if (repop)
                    this.Populate();
            }
        }


        #region File I/O

        public void SaveGroups() { SaveGroups(file_loginAlert); }
        public void SaveGroups(string path)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;

            XmlWriter xmlWriter = XmlWriter.Create(path, settings);

            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Groups");

            foreach (var group in PlayerGroups)
            {
                xmlWriter.WriteStartElement("Group");

                xmlWriter.WriteStartElement("Name");
                xmlWriter.WriteString(group.Name);
                xmlWriter.WriteEndElement();

                xmlWriter.WriteStartElement("Icon");
                xmlWriter.WriteString(group.Icon.ToString());
                xmlWriter.WriteEndElement();

                if (group.Minimized)
                {
                    xmlWriter.WriteStartElement("Minimized");
                    xmlWriter.WriteEndElement();
                }

                if (group.TrayNotif)
                {
                    xmlWriter.WriteStartElement("TrayNotif");
                    xmlWriter.WriteEndElement();
                }

                if (group.SoundEnabled)
                {
                    xmlWriter.WriteStartElement("SoundEnabled");
                    xmlWriter.WriteEndElement();
                }

                if (!string.IsNullOrEmpty(group.SoundPath))
                {
                    xmlWriter.WriteStartElement("SoundPath");
                    xmlWriter.WriteString(group.SoundPath);
                    xmlWriter.WriteEndElement();
                }

                if (group.Players.Count != 0)
                {

                    xmlWriter.WriteStartElement("Players");

                    foreach (var player in group.Players)
                    {
                        xmlWriter.WriteStartElement("Name");
                        xmlWriter.WriteString(player);
                        xmlWriter.WriteEndElement();
                    }

                    xmlWriter.WriteEndElement();
                }
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();

            xmlWriter.Close();

        }
        public static List<TrackedPlayerGroup> LoadGroups(string path)
        {
            List<TrackedPlayerGroup> groups = new List<TrackedPlayerGroup>();

            if (File.Exists(path))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(path);

                TrackedPlayerGroup uncategorized = new TrackedPlayerGroup(); // For legacy 0.96 migration
                uncategorized.Name = "Uncategorized";

                foreach (XmlNode xmlNode in xmlDoc.DocumentElement)
                {
                    if (xmlNode.Name == "Player")
                    {
                        foreach (XmlNode pNode in xmlNode.ChildNodes)
                        {
                            if (pNode.Name == "Name")
                                uncategorized.Players.Add(pNode.InnerText);
                        }
                    }
                    else if (xmlNode.Name == "Group")
                    {
                        TrackedPlayerGroup group = new TrackedPlayerGroup();

                        foreach (XmlNode groupNode in xmlNode.ChildNodes)
                        {
                            switch(groupNode.Name)
                            {
                                case "Name":
                                    group.Name = groupNode.InnerText;
                                    break;
                                case "Icon":
                                    int icon = 0;
                                    bool success = int.TryParse(groupNode.InnerText, out icon);
                                    if (success)
                                        group.Icon = icon;
                                    break;
                                case "Minimized":
                                    group.Minimized = true;
                                    break;
                                case "TrayNotif":
                                    group.TrayNotif = true;
                                    break;
                                case "SoundEnabled":
                                    group.SoundEnabled = true;
                                    break;
                                case "SoundPath":
                                    group.SoundPath = groupNode.InnerText;
                                    break;
                                case "Players":
                                    foreach (XmlNode playerNode in groupNode.ChildNodes)
                                        if (playerNode.Name == "Name")
                                            group.Players.Add(playerNode.InnerText);
                                    break;
                            }
                        }
                        groups.Add(group);
                    }
                }


                if (uncategorized.Players.Count != 0)
                    groups.Add(uncategorized);
            }



            return groups;
        }
        
        #endregion



        public void CheckOnline()
        {
            List<string> alertPlayers = new List<string>();
            TrackedPlayerGroup alertGroup = null;
            List<KeyValuePair<string, TrackedPlayerGroup>> trackedPlayers = new List<KeyValuePair<string, TrackedPlayerGroup>>();

            foreach (var g in PlayerGroups)
                foreach (var p in g.Players)
                    trackedPlayers.Add(new KeyValuePair<string, TrackedPlayerGroup>(p, g));

            foreach (var p in trackedPlayers)
            {
                // Some stuff for when user has put the tracked players name in the wrong casing
                var onlinePlayer = Form_Main._currentlyOnline.Where(s => s.Name.Equals(p.Key, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

                if (!onlinePlayer.Equals(default(Form_Main.Player)))
                {
                    // Tracked player is online
                    p.Value.Control.ColorItem(p.Key, Color.PaleGreen);
                    
                    if (!LastOnline.Contains(p.Key))
                    {
                        // Wasn't online before so run the alert
                        LastOnline.Add(p.Key);
                        alertPlayers.Add(p.Key);
                        if (alertGroup == null)
                            if (p.Value.TrayNotif || p.Value.SoundEnabled)
                                alertGroup = p.Value; // Only set group once to first one
                    }
                }
                else
                {
                    // Not online
                    if (LastOnline.Contains(p.Key))
                    {
                        // Was online before
                        LastOnline.Remove(p.Key);
                        p.Value.Control.ColorItem(p.Key, Color.White);
                    }
                }

            }

            LoginAlert(alertPlayers, alertGroup);

        }

        private void LoginAlert(List<string> players, TrackedPlayerGroup group)
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
                    if (group.SoundEnabled && !string.IsNullOrEmpty(group.SoundPath))
                        if (File.Exists(group.SoundPath))
                            alertSound = group.SoundPath;

                    if (i < 3)
                    {
                        alertString += players[i];

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

                if (group.TrayNotif)
                    Form_Main.Tray_ShowBubble(TrayBubbleBehaviour.None, 5000, "Tibiantis Login", alertString, ToolTipIcon.Warning);

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









    }

    public class TrackedPlayerGroup
    {
        public Control_TrackedPlayerGroup Control;
        public string Name;
        public int Icon = 207;
        public bool Minimized = false;
        public bool TrayNotif = false;
        public bool SoundEnabled = false;
        public string SoundPath;
        public List<string> Players;

        public TrackedPlayerGroup()
        {
            Players = new List<string>();
        }
    }

}
