using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace TibiantisHelper.Tabs.LoginAlert
{
	public partial class Control_TrackedPlayerGroup : UserControl
	{

		public TrackedPlayerGroup Group;

		int displayedSprite = 207;
		int baseHeight;


		public event EventHandler GroupRemove;
		public event EventHandler GroupChanged;
		public event EventHandler GroupSelected;

		public Control_TrackedPlayerGroup(Tabs.LoginAlert.TrackedPlayerGroup group)
		{
			this.Group = group;

			InitializeComponent();

			button_settings.FlatAppearance.BorderSize = 0;

			label_name.Text = Group.Name;
			displayedSprite = Group.Icon;

			DrawSprite();

			baseHeight = this.Height;

			objectListView1.LabelEdit = true;

			OLVColumn playerName = new OLVColumn();
			playerName.Text = "Player";
			playerName.FillsFreeSpace = true;
			playerName.AspectGetter = delegate (object x)
			{
				return (string)x;
			};

			objectListView1.Columns.Add(playerName);

			objectListView1.Objects = Group.Players;



		}

		private void Control_TrackedPlayerGroup_Load(object sender, EventArgs e)
		{

			checkBox_minimize.Checked = Group.Minimized;
			checkBox_trayNotif.Checked = Group.TrayNotif;
			checkBox_soundEnabled.Checked = Group.SoundEnabled;
			textBox_soundPath.Text = Group.SoundPath;

			ResizeList();
		}


		public void ColorItem(string player, Color color)
		{
			foreach (ListViewItem i in objectListView1.Items)
			{
				if (i.Text == player)
                {
					i.BackColor = color;
					break;
				}
			}
		}





		public void Save(string path)
		{

			XmlWriterSettings settings = new XmlWriterSettings();
			settings.Indent = true;

			XmlWriter xmlWriter = XmlWriter.Create(path, settings);

			xmlWriter.WriteStartDocument();
			xmlWriter.WriteStartElement("Groups");
			xmlWriter.WriteStartElement("Group");

			xmlWriter.WriteStartElement("Name");
			xmlWriter.WriteString(Group.Name);
			xmlWriter.WriteEndElement();

			xmlWriter.WriteStartElement("Icon");
			xmlWriter.WriteString(Group.Icon.ToString());
			xmlWriter.WriteEndElement();

			if (Group.Players.Count != 0)
			{

				xmlWriter.WriteStartElement("Players");

				foreach (var player in Group.Players)
				{
					xmlWriter.WriteStartElement("Name");
					xmlWriter.WriteString(player);
					xmlWriter.WriteEndElement();
				}

				xmlWriter.WriteEndElement();
			}

			xmlWriter.WriteEndElement();
			xmlWriter.WriteEndElement();
			xmlWriter.WriteEndDocument();

			xmlWriter.Close();
		}

		private void ResizeList()
		{
			if (Group.Minimized)
			{
				panel_main.Visible = panel_main.Enabled = false;
				this.Size = new Size(this.Width, 42);
				return;
			}
			else
				panel_main.Visible = panel_main.Enabled = true;

			if (Group.Players.Count > 2)
				this.Size = new Size(this.Width, baseHeight + ( (1 + objectListView1.RowHeightEffective) * (Group.Players.Count - 2)));
			else
				this.Size = new Size(this.Width, baseHeight);

		}
		public void RefreshList()
        {

			objectListView1.Objects = Group.Players;
			objectListView1.SelectedIndex = objectListView1.Items.Count - 1;
			GroupChanged?.Invoke(this, EventArgs.Empty);
			ResizeList();
		}

		private void DrawSprite()
		{

			Bitmap bmp = new Bitmap(64, 64);

			var sprite = DataReader.ReadSprite(displayedSprite);

			using (Graphics g = Graphics.FromImage(bmp))
			{
				g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
				g.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.Half;

				g.DrawImage(sprite, new Rectangle(Point.Empty, new Size(64, 64)));
			}

			button_icon.Image = bmp;

		}

		private void button_icon_MouseClick(object sender, MouseEventArgs e)
		{

			Form_IconSelect form = new Form_IconSelect();

			//form.Owner = this;

			form.SetDesktopLocation(Cursor.Position.X, Cursor.Position.Y);
			form.Deactivate += (s, ee) => {
				this.displayedSprite = form.SelectedIcon;
				DrawSprite();
				GroupChanged?.Invoke(this, EventArgs.Empty);
			};
			form.Show();
		}



		#region Header Controls

		private void checkBox_minimized_CheckedChanged(object sender, EventArgs e)
		{
			Group.Minimized = checkBox_minimize.Checked;
			ResizeList();
			GroupChanged?.Invoke(this, EventArgs.Empty);
		}


		private void toolStripMenuItem_saveAs_Click(object sender, EventArgs e)
		{
			SaveFileDialog saveFileDialog1 = new SaveFileDialog();

			saveFileDialog1.Filter = "THG file (*.thg)|*.thg";
			saveFileDialog1.RestoreDirectory = true;

			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				try
				{
					Save(saveFileDialog1.FileName);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
				}
			}
		}
		private void btoolStripMenuItem_editName_Click(object sender, EventArgs e)
		{
			Form_GroupEdit diag = new Form_GroupEdit(Group.Name);
			if (diag.ShowDialog() == DialogResult.OK)
			{
				if (!string.IsNullOrEmpty(diag.GroupName))
				{
					Group.Name = diag.GroupName;
					label_name.Text = Group.Name;
					GroupChanged?.Invoke(this, EventArgs.Empty);
				}
			}
		}

		private void button_deleteGroup_Click(object sender, EventArgs e)
		{
			var result = MessageBox.Show("Remove group \"" + Group.Name + "\"?", "Remove Group", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

			if (result == DialogResult.Yes)
			{
				GroupRemove?.Invoke(this, EventArgs.Empty);
				// Uhhh throw event I guess???
			}
		}

		#endregion

		#region Bottom Controls

		private void checkBox_trayNotif_CheckedChanged(object sender, EventArgs e)
		{
			Group.TrayNotif = checkBox_trayNotif.Checked;
			GroupChanged?.Invoke(this, EventArgs.Empty);
		}
		private void checkBox_soundEnabled_CheckedChanged(object sender, EventArgs e)
		{
			Group.SoundEnabled = checkBox_soundEnabled.Checked;
			GroupChanged?.Invoke(this, EventArgs.Empty);
		}
		
		private void button_browseSound_Click(object sender, EventArgs e)
		{
			var diag = new OpenFileDialog();
			diag.Filter = "WAV file (*.wav)|*.wav";
			diag.RestoreDirectory = true;

			if (diag.ShowDialog() == DialogResult.OK)
			{
				try
				{
					Group.SoundPath = diag.FileName;
					textBox_soundPath.Text = Group.SoundPath;
					GroupChanged?.Invoke(this, EventArgs.Empty);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message, "Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Warning);
				}
			}
		}
		private void textBox_soundPath_TextChanged(object sender, EventArgs e)
		{
			textBox_soundPath.Text = System.IO.Path.GetFileName(textBox_soundPath.Text);
		}

		
		#endregion

		#region Player Controls

		private void button_addPlayer_Click(object sender, EventArgs e)
		{
			Form_AddPlayerDialog diag = new Form_AddPlayerDialog();

			if (diag.ShowDialog() == DialogResult.OK)
			{
				Group.Players.Add(diag.PlayerName);
				RefreshList();
			}
		}

		private void button_deletePlayer_Click(object sender, EventArgs e)
		{
			if (objectListView1.SelectedItems.Count != 0)
			{
				if (objectListView1.SelectedItems.Count == 1)
				{
					string player = objectListView1.SelectedItems[0].Text;
					var result = MessageBox.Show("Remove \""+ player +"\"?", "Remove Player", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

					if (result == DialogResult.Yes)
					{
						List<string> copy = new List<string>();
						copy.AddRange(Group.Players);
						Group.Players.Remove(player);
					}
				}
				else
				{
					var result = MessageBox.Show("Remove " + objectListView1.SelectedItems.Count + " selected players?", "Remove Player", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);


					if (result == DialogResult.Yes)
					{
						List<string> copy = new List<string>();

						foreach (var p in Group.Players)
						{
							bool matched = false;
							foreach (ListViewItem m in objectListView1.SelectedItems)
								if (p == m.Text)
									matched = true;

							if (!matched)
								copy.Add(p);

						}

						Group.Players = copy;
					}
				}


				objectListView1.Objects = Group.Players;
				ResizeList();
				GroupChanged?.Invoke(this, EventArgs.Empty);
			}
		}

		#endregion



		private void objectListView1_CellEditValidating(object sender, CellEditEventArgs e)
		{
			Console.WriteLine((string)e.Value);
			Console.WriteLine((string)e.NewValue);
		}

		private void toolStripMenuItem_import_Click(object sender, EventArgs e)
		{
			var diag = new OpenFileDialog();
			diag.Filter = "THG file (*.thg)|*.thg";
			diag.RestoreDirectory = true;

			if (diag.ShowDialog() == DialogResult.OK)
			{
				var group = Tab_LoginAlert.LoadGroups(diag.FileName)[0];
				Form_Main.Form.tab_LoginAlert1.Import(group, Group);
			}
		}

        private void button_settings_MouseClick(object sender, MouseEventArgs e)
        {
			//button_settings.ContextMenu.Show(e.Location.X, e.Location.Y);
        }

        private void button_settings_Click(object sender, EventArgs e)
		{
			contextMenuStrip1.Show(Cursor.Position);


		}

        private void panel_header_MouseDown(object sender, MouseEventArgs e)
        {
			this.BackColor = SystemColors.Control;
			GroupSelected?.Invoke(this, EventArgs.Empty);
		}

        private void panel_header_MouseUp(object sender, MouseEventArgs e)
		{
			this.BackColor = Color.White;
		}
    }
}
