using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ImageTagg
{
    class Repertoire
    {
        public Repertoire()
        {

        }
        public List<string> repertoireMere()
        {
            
            try
            {
                string dirPath = ".\\";

                List<string>  dirs = new List<string>(Directory.EnumerateDirectories(dirPath));
                return dirs;

            }
            catch (UnauthorizedAccessException UAEx)
            {
                Console.WriteLine(UAEx.Message);
            }
            catch (PathTooLongException PathEx)
            {
                Console.WriteLine(PathEx.Message);
            }
            return null;
                 
        }

        public List<string> fichierRepertoires()
        {
            List<string> fic;
            try
            {
                fic = new List<string>();
                var files = from file in Directory.EnumerateFiles(".\\retete\\vision", "*.jpg", SearchOption.AllDirectories)
                            select new
                            {
                                File = file
                            };
                foreach (var f in files)
                {
                    fic.Add(f.File);
                }
                return fic;
            }
            catch (UnauthorizedAccessException UAEx)
            {
                Console.WriteLine(UAEx.Message);
            }
            catch (PathTooLongException PathEx)
            {
                Console.WriteLine(PathEx.Message);
            }
            return null;
        }
    
        public List<string> fichiersRepertoire(string rep)
        {
            List<string> fichier = new List<string>();
            try
            {
                 var files = from file in Directory.EnumerateFiles(rep, "*.jpg", SearchOption.TopDirectoryOnly)
                             select new
                             {
                                 File = file
                             };
                 foreach (var f in files)
                 {
                     fichier.Add(f.File);
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
            return fichier;
        }

    }
}
