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

        public ListeImages()
        {
            listChemin = new List<string>();
            listPhoto = new List<PictureBox>();
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


        public List<String> getAllTags()
        {
            return this.listChemin;
        }

        public void LoadPicture(String name)
        {
            Console.WriteLine("zzzzzzzzzzzzzzzzz");
            this.listChemin.Add(name);
            PictureBox pic = new PictureBox();
            Console.WriteLine("pppppppp");
            pic.Image = Image.FromFile(name);
            pic.Name = name;
            Console.WriteLine("xxxxx");
            this.listPhoto.Add(pic);
            Console.WriteLine("tessst 2");
            pic.Click += Pic_Click1;

        }

        private void Pic_Click1(object sender, EventArgs e)
        {

            Console.WriteLine("ooooooooooooooooooooooooooooo");
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
                newChemin += tag + "\\" + liste[liste.Length - 1];
                File.Copy(fileName, newChemin, true);
                File.Delete(fileName);
                int indice = listChemin.IndexOf(fileName);
                listChemin[indice] = newChemin;
                this.listPhoto[indice] = null;
                PictureBox pic = new PictureBox();
                pic.Image = Image.FromFile(newChemin);
                this.listPhoto[indice] = pic;
            } else if (operation == "Supprimer") {

            } else if (operation == "Modifier"){

            }
        }


    }
}
