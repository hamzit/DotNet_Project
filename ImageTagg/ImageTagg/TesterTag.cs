﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ImageTagg
{
    class TesterTag
    {
        private StreamReader sr;
        public TesterTag()
        {
        }
        public bool ComparerTag(string tag)
        {
            bool reslt = false;
            try
            {
                sr = new StreamReader(".\\Tag.txt");
                string line = sr.ReadLine();
                
                while (reslt == false && line != null)
                {
                    reslt = tag.Equals(line);
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return reslt;
        }
        public string ajouterTag(string tag, string super)
        {
            string re = "";
            try
            {
                File.AppendAllText(".\\Tag.txt", tag+"\n");
                if (super != null)
                {
                    string ligne = LigneTag(super);
                    if (ligne != null)
                    {
                        string mot = ligne + "\\" + tag + "\n";
                        Console.WriteLine(mot);
                        re = ligne + "\\" + tag;
                        File.AppendAllText(".\\Dossier.txt", mot);
                        Directory.CreateDirectory(re);
                        return re;
                    }
                }
                else
                {
                     string mot = ".\\" + tag + "\n";
                    File.AppendAllText(".\\Dossier.txt", mot);
                    re = ".\\" + tag;
                    Directory.CreateDirectory(re);
                    return re;
                }
            }
            catch (DirectoryNotFoundException ex)
            {
                    Console.WriteLine("le fichier n'a pas etait trouver" + ex.Message);
            }
            return re;
        }
        public string LigneTag(string nom)
        {
            bool reslt = false;
            try
            {
                sr = new StreamReader(".\\Dossier.txt");
                string line = sr.ReadLine();

                while (reslt == false && line != null)
                {
                    string[] liste = line.Split('\\');
                    int i = 0;
                    while (reslt == false && i < liste.Length)
                    {
                        reslt = nom.Equals(liste[i]);
                        i++;
                    }
                    if (reslt == true)
                    {
                        sr.Close();
                        return line;
                    }
                    line = sr.ReadLine();
                }
                sr.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }
    }
}
