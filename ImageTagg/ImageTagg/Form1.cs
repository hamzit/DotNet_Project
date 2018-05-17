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
        private ListeImages listeImages = new ListeImages();
        private OpenFileDialog open;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<String> listeSec = listeImages.getListeSelectionnee();
            if (listeSec.Count >= 1)
            {
                if(textBox3.Text.CompareTo("") != 0  && comboBox2.SelectedItem == null)
                {
                    foreach (string val in listeSec) {
                        if (val.Contains(textBox3.Text) == false)
                            listeImages.updateTag(val, "Ajouter", textBox3.Text,null);
                    }
                }
                else if(textBox3.Text.CompareTo("") == 0 && comboBox2.SelectedItem != null){
                    foreach (string val in listeSec)
                    {
                        if (val.Contains(textBox3.Text) == false)
                            listeImages.updateTag(val, "Ajouter", comboBox2.Text,null);
                    }
                }
            }
        }

       
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(".\\images"))
            {
                Directory.CreateDirectory(".\\images");
            }

            this.afficherImages(listeImages.getAllImages());
            loadCombox();
        }


        private void chargerDossierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            open = new OpenFileDialog();
            open.Multiselect = true;
            open.InitialDirectory = "C:\\";
            open.Filter = "Image Files (*.jpg)|*.jpg|All Files(*.*)|*.*";
            open.FilterIndex = 1;
            if (open.ShowDialog() == DialogResult.OK)
            {           
                    
                this.listeImages.LoadPicture(open.FileNames);
                this.afficherImages(listeImages.getListePhotoCharge());
            }
        }

       

        private void afficherImages(List<PictureBox> liste)
        {
            List<PictureBox> allImages = liste;
            int x = 20;
            int y = 40;
            int maxHeight = -1;
            panel1.Visible = true;
            panel1.Controls.Clear();
            foreach (PictureBox imagge in allImages)
            {
                Panel pan = new Panel();
                pan.Width = 100; pan.Height = 100; pan.Location = new Point(x, y);
                imagge.Width = 95;
                imagge.Height = 95;
                imagge.SizeMode = PictureBoxSizeMode.StretchImage;
                x += imagge.Width + 10;
                maxHeight = Math.Max(imagge.Height, maxHeight);
                if (x > this.panel1.Width - 100)
                {
                    x = 20;
                    y += maxHeight + 10;
                }
                pan.BackColor = Color.Gray;
                imagge.Click += Pic_Click1;
                pan.Controls.Add(imagge);               
                this.panel1.Controls.Add(pan);
            }
        }

       

        private void toutAfficherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Repertoire rep = new Repertoire();
            List<string> liste = rep.fichierRepertoires();
        }

        

        private void button4_Click_1(object sender, EventArgs e)
        {
            if (textBox5.Text.CompareTo("") != 0)
            {
                List<PictureBox> rechercher = new List<PictureBox>();
                foreach (String nom in this.listeImages.getAllTags())
                {
                    if (nom.Contains(textBox5.Text))
                    {
                        int ind = this.listeImages.getAllTags().IndexOf(nom);
                        rechercher.Add(this.listeImages.getAllImages()[ind]);
                    }
                }
                afficherImages(rechercher);
                textBox5.Text = "";
            }
            else
                afficherImages(this.listeImages.getAllImages());
        }
        public void Pic_Click1(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            if (listeImages.getListeSelectionnee().Contains(pic.Name))
            {
                pic.BorderStyle = BorderStyle.FixedSingle;
                listeImages.getListeSelectionnee().Remove(pic.Name);
                loadCombox();
            }
            else
            {
                pic.BorderStyle = BorderStyle.Fixed3D;
                listeImages.getListeSelectionnee().Add(pic.Name);
                PictureBox picte = new PictureBox();
                picte.Image = Image.FromFile(pic.Name);
                picte.SizeMode = PictureBoxSizeMode.Zoom;
                picte.Width = panel6.Width - 10;
                picte.Height = panel6.Height;
                this.panel6.Controls.Clear();
                panel6.Controls.Add(picte);
                loadCombox();

            }
        }
        public void loadCombox()
        {
            comboBox2.Items.Clear();
            comboBox3.Items.Clear();
            comboBox4.Items.Clear();
            List<String> liste  = listeImages.ComboxTag(listeImages.getListeSelectionnee());
            foreach (String name in liste)
            {
                comboBox2.Items.Add(name);
                comboBox3.Items.Add(name);
                comboBox4.Items.Add(name);
            }
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            List<String> listeSec = listeImages.getListeSelectionnee();
            if (listeSec.Count >= 1)
            {
                if (textBox3.Text.CompareTo("") != 0 && comboBox3.SelectedItem != null)
                {
                    foreach (string val in listeSec)
                    {
                        if (val.Contains(textBox3.Text) == false)
                            listeImages.updateTag(val, "Modifier", comboBox3.Text, textBox3.Text);
                    }
                }
                
            }
        }
    }
  
}
