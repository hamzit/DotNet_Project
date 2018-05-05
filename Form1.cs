using System;

using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
namespace ImageTagg
{
    public partial class Form1 : Form
    {
        private OpenFileDialog open;
        //List<Panel> listePanel = new List<Panel>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            TesterTag tet = new TesterTag();
            if (textBox3.Text != null && tet.ComparerTag(textBox3.Text) == false)
            {
                if (comboBox2.SelectedItem != null)
                {
                    tet.ajouterTag(textBox3.Text, comboBox2.Text);
                }
                else
                {
                    tet.ajouterTag(textBox3.Text, null);
                }
                MessageBox.Show("Tag enregistré");
                Form1_Load(sender, e);
            }
            else if (textBox3.Text != null)
                MessageBox.Show("Tag déja enregisté");
            else
                MessageBox.Show("Vous n'avez pas ajouter de Tag");
            textBox3.Text = null;
            comboBox2.Text = null;
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
             
                
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }
        private void SetProperty(ref PropertyItem prop, int iId, string sTxt)
        {
            int iLen = sTxt.Length + 1;
            byte[] bTxt = new Byte[iLen];
            for (int i = 0; i < iLen - 1; i++)
                bTxt[i] = (byte)sTxt[i];
            bTxt[iLen - 1] = 0x00;
            prop.Id = iId;
            prop.Type = 2;
            prop.Value = bTxt;
            prop.Len = iLen;
            //garder la fonction pour tagger in the picture
            if (open.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (open.CheckFileExists)
                {
                    Image dummy = Image.FromFile(open.FileName);
                    PropertyItem item = dummy.PropertyItems[0];
                    SetProperty(ref item, 33432, "Copyright Information");
                    dummy.SetPropertyItem(item);
                    item = dummy.PropertyItems[0];
                    SetProperty(ref item, 315, "Artist...");
                    dummy.SetPropertyItem(item);

                    item = dummy.PropertyItems[0];
                    SetProperty(ref item, 270, "Title...");
                    dummy.SetPropertyItem(item);

                    item = dummy.PropertyItems[0];
                    SetProperty(ref item, 272, "Software...");
                    dummy.SetPropertyItem(item);

                    dummy.Save("C:\\temp\\pics\\mypic_modified.JPG");
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            panel2.Visible = true;
            try
            {
                StreamReader sr = new StreamReader(".\\Tag.txt");
                string line = sr.ReadLine();
                comboBox2.Items.Clear();
                comboBox3.Items.Clear();
                comboBox4.Items.Clear();
                while (line != null)
                {
                    comboBox2.Items.Add(line);
                    comboBox3.Items.Add(line);
                    comboBox4.Items.Add(line);
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void tagToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void chargerDossierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            open.InitialDirectory = "C:\\";
            open.Filter = "Image Files (*.jpg)|*.jpg|All Files(*.*)|*.*";
            open.FilterIndex = 1;
            if (open.ShowDialog() == DialogResult.OK)
            {
                pictureBox1.ImageLocation = open.FileName;
                string[] chemin = open.FileName.Split('\\');
                string nom = chemin[chemin.Length - 1];
                textBox1.Text = nom;
                panel2.Visible = true;
            }
        }

        private void ajoutDeTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = true;
        }

        private void modificationDeTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = true;

        }

        private void suppressionDeTagToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel3.Visible = false; panel2.Visible = false;panel4.Visible = true;
            
        }

        private void chargerListePhotToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel1.Controls.Clear();
            pictureBox1.Visible = false;
            OpenFileDialog files = new OpenFileDialog();
            files.Title = "Please select multiply images";
            files.Multiselect = true;
            files.Filter = "JPG|*.jpg|JPEG|*.jpeg";
            DialogResult dr = files.ShowDialog();
            if(dr == DialogResult.OK)
            {
                string[] fic = files.FileNames;
                int x = 20;
                int y = 40;
                int maxHeight = -1;
                foreach(string img in fic)
                {
                    PictureBox pic = new PictureBox();
                    pic.Image = Image.FromFile(img);
                    pic.Width = 100;
                    pic.Height = 100;                
                    pic.SizeMode = PictureBoxSizeMode.StretchImage;
                    pic.Location = new Point(x, y);
                    x += pic.Width + 10;

                    maxHeight = Math.Max(pic.Height, maxHeight);
                    if(x > this.panel1.Width - 100 )
                    {
                        x = 20;
                        y += maxHeight + 10;
                    }
                    this.panel1.Controls.Add(pic);
                }
            }
        }
    }
  
}
