using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ImageTagg
{
    class ListeImages
    {
        private List<String> listChemin;
        private List<PictureBox> listPhoto;
        private List<PictureBox> listPhotoCharge;
        private List<String> ListeSelectionnee;
        public ListeImages()
        {
            listChemin = new List<string>();
            listPhoto = new List<PictureBox>();
            listPhotoCharge = new List<PictureBox>();
            ListeSelectionnee = new List<string>();
            try
            {
                var files = from file in Directory.EnumerateFiles(".\\images", "*.jpg", SearchOption.AllDirectories)
                            select new
                            {
                                File = file
                            };
                foreach (var f in files)
                {
                    listChemin.Add(f.File);
                    PictureBox pic = new PictureBox();
                    pic.Image = Image.FromFile(f.File);
                    this.listPhoto.Add(pic);
                }
            }
            catch (UnauthorizedAccessException UAEx)
            {
                Console.WriteLine(UAEx.Message);
            }
            catch (PathTooLongException PathEx)
            {
                Console.WriteLine(PathEx.Message);
            }
        }

        public List<PictureBox> getAllImages()
        {
            return this.listPhoto;
        }
        public List<PictureBox> getListePhotoCharge()
        {
            return this.listPhotoCharge;
        }

        public List<String> getAllTags()
        {
            return this.listChemin;
        }
        public List<String> getListeSelectionnee()
        {
            return ListeSelectionnee;
        }
        public void LoadPicture(String[] name)
        {
            foreach (string namePi in name)
            {
                this.listChemin.Add(namePi);
                PictureBox pic = new PictureBox();
                pic.Image = Image.FromFile(namePi);
                pic.Name = namePi;
                this.listPhoto.Add(pic);
                this.listPhotoCharge.Add(pic);
                pic.Click += Pic_Click1;
            }

        }

      

        public void Pic_Click1(object sender, EventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            if (ListeSelectionnee.Contains(pic.Name))
            {
                pic.BorderStyle = BorderStyle.FixedSingle;
                ListeSelectionnee.Remove(pic.Name);
            }
            else
            {
                pic.BorderStyle = BorderStyle.Fixed3D;
                ListeSelectionnee.Add(pic.Name);
            }
        }

        public void PremierAjouter(String filename,String tag)
        {
            string[] name = filename.Split('\\');
            string newChemin = ".\\images"+"\\" + tag;
            Directory.CreateDirectory(newChemin);
            newChemin +=  "\\" + name[name.Length - 1];
            Console.WriteLine("avant copy");
            File.Copy(filename, newChemin, true);
            Console.WriteLine("apres copy");
            listChemin.Add( newChemin);
            PictureBox pic = new PictureBox();
            pic.Image = Image.FromFile(newChemin);
            this.listPhoto.Add(pic);
        }
        public void updateTag(String fileName, String operation, String tag)
        {
            if (operation == "Ajouter"){
                string[] liste = fileName.Split('\\');
                string newChemin = "";
                for (int i = 0; i < liste.Length - 2; i++)
                {
                    newChemin += liste[i] + "\\";
                }
                newChemin += tag ;
                if (!Directory.Exists(newChemin))
                {
                    Directory.CreateDirectory(newChemin);
                }
                newChemin += "\\"+liste[liste.Length - 1];
                File.Copy(fileName, newChemin, true);
                //File.Delete(fileName);
                int indice = listChemin.IndexOf(fileName);
                listChemin[indice] = newChemin;
                this.listPhoto[indice] = null;
                PictureBox pic = new PictureBox();
                pic.Image = Image.FromFile(newChemin);
                this.listPhoto[indice] = pic;

            } else if (operation == "Supprimer") {

                string[] liste = fileName.Split('\\');
                int id = this.indice(liste, tag);
                liste[id] = "";
                string newChemin = "";
                for (int i = 0; i < liste.Length - 2; i++)
                {
                    newChemin += liste[i] + "\\";
                }
                newChemin += tag;
                if (!Directory.Exists(newChemin))
                {
                    Directory.CreateDirectory(newChemin);
                }
                newChemin +="\\"+ liste[liste.Length - 1];
                File.Copy(fileName, newChemin, true);
                File.Delete(fileName);
                int indice = listChemin.IndexOf(fileName);
                listChemin[indice] = newChemin;
                this.listPhoto[indice] = null;
                PictureBox pic = new PictureBox();
                pic.Image = Image.FromFile(newChemin);
                this.listPhoto[indice] = pic;

            } else if (operation == "Modifier"){

            }
        }

        public int indice(string[] liste,string chaine)
        {
            int ind = 0;
            while (ind < liste.Length && chaine.CompareTo(liste[ind]) != 0)
                ind++;
            return ind;
        }

    }
}
