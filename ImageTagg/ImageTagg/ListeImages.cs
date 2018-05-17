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
                    pic.Name = f.File;
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
                if (this.listChemin.Contains(namePi) == false)
                {
                    this.listChemin.Add(namePi);
                    PictureBox pic = new PictureBox();
                    pic.Image = Image.FromFile(namePi);
                    pic.Name = namePi;
                    this.listPhoto.Add(pic);
                    this.listPhotoCharge.Add(pic);
                }
            }

        }

      
       
        public void updateTag(String fileName, String operation, String tag,String nvTag)
        {
            if (operation == "Ajouter")
            {

                string newChemin = "";
                string[] liste = fileName.Split('\\');
                if (fileName.Contains("images"))
                {
                    for (int i = 0; i < liste.Length - 1; i++)
                        newChemin += liste[i] + "\\";
                }
                else
                {
                    string[] name = fileName.Split('\\');
                    newChemin = ".\\images" + "\\";
                }
                newChemin += tag;
                if (!Directory.Exists(newChemin))
                {
                    Directory.CreateDirectory(newChemin);
                }
                Console.WriteLine(fileName);
                newChemin += "\\" + liste[liste.Length - 1];

                File.Copy(fileName, newChemin, true);
                //File.Delete(fileName);
                int indice = listChemin.IndexOf(fileName);
                listChemin[indice] = newChemin;
                this.listPhoto[indice] = null;
                PictureBox pic = new PictureBox();
                pic.Image = Image.FromFile(newChemin);
                this.listPhoto[indice] = pic;

            }
            else if (operation == "Supprimer")
            {

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
                newChemin += "\\" + liste[liste.Length - 1];
                File.Copy(fileName, newChemin, true);
                File.Delete(fileName);
                int indice = listChemin.IndexOf(fileName);
                listChemin[indice] = newChemin;
                this.listPhoto[indice] = null;
                PictureBox pic = new PictureBox();
                pic.Image = Image.FromFile(newChemin);
                this.listPhoto[indice] = pic;

            }
            else if (operation == "Modifier")
            {

                string newChemin = "";
                string[] liste = fileName.Split('\\');
                if (fileName.Contains(tag))
                {
                    int i = 0;
                    while (i < liste.Length -1)
                    {
                        if (liste[i].CompareTo(tag) != 0)
                            newChemin += liste[i];
                        else
                            newChemin += nvTag;
                        i++;
                    }
                    if (!Directory.Exists(newChemin))
                    {
                        Directory.CreateDirectory(newChemin);
                    }
                    newChemin += "\\" + liste[liste.Length - 1];
                    File.Copy(fileName, newChemin, true);
                    //File.Delete(fileName);
                    int indice = listChemin.IndexOf(fileName);
                    listChemin[indice] = newChemin;
                    this.listPhoto[indice] = null;
                    PictureBox pic = new PictureBox();
                    pic.Image = Image.FromFile(newChemin);
                    this.listPhoto[indice] = pic;
                }
            }
        }

        public int indice(string[] liste,string chaine)
        {
            int ind = 0;
            while (ind < liste.Length && chaine.CompareTo(liste[ind]) != 0)
                ind++;
            return ind;
        }

        public List<string> ComboxTag(List<string> ListeSelect)
        {
            List<string> listeC = new List<string>();
            foreach(String name in ListeSelect)
            {
                String[] tag = name.Split('\\');
                for (int i = 0; i< tag.Length - 2;i++)
                {
                    if (listeC.Contains(tag[i]) == false )
                        listeC.Add(tag[i]);
                }
            }
            return listeC;
        }
    }
}
