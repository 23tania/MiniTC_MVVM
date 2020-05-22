using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniTC.Model
{
    class Model
    {
        // Zwraca wszystkie dostępne dyski
        public string[] GetDrives()
        {
            return Directory.GetLogicalDrives();
        }

        // Zwraca wszystkie pliki w ścieżce
        public string[] GetFiles(string path)
        {
            List<string> AllFiles = new List<string>();

            try
            {
                // Dodawanie dwukropka gdy istnieje folder wyżej
                if (Directory.GetParent(path) != null)
                {
                    AllFiles.Add("...");
                }

                // Dodawanie wszystkich folderów i plików z ścieżki
                string[] Directories = Directory.GetDirectories(path);
                string[] Files = Directory.GetFiles(path);

                for (int i=0; i<Directories.Length; i++)
                {
                    AllFiles.Add("<D>" + Path.GetFileName(Directories[i]));
                }

                for (int j = 0; j < Files.Length; j++)
                {
                    AllFiles.Add(Path.GetFileName(Files[j]));
                }

            }
            catch {}

            return AllFiles.ToArray();
        }

        // Zmienia ścieżkę po kliknięciu dwukrotnie
        public string ChangePath(string path, string selectedFile)
        {
            // Sprawdzenie czy jest to folder
            if (selectedFile != null && selectedFile.Substring(0, 3) == "<D>" 
                && selectedFile != "..." )
            {
                selectedFile = selectedFile.Replace("<D>", "");
                string newPath = Path.Combine(path, selectedFile);
                return newPath;
            }

            // Pójście o katalog w górę
            else if (selectedFile == "...")
            {
                path = GetParentOfFile(path);
            }

            return path;
        }

        // Pójście o ścieżkę w górę
        public static string GetParentOfFile(string path)
        {
            return Directory.GetParent(path).FullName;
        }

        // Kopiowanie pliku
        public void CopyFile(string source, string destination)
        {
            try
            {
                File.Copy(source, destination, true);
            }

            catch {}
        }
    }
}
