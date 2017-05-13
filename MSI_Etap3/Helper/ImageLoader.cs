using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSI_Etap3.Helper
{
    public class ImageLoader
    {
        public static List<List<String>> GetImages()
        {
            List<List<string>> imagesList = new List<List<string>>();
            int s = 4;
            for (int i = 0; i < s; i++)
            {
                imagesList.Add(new List<string>());
            }
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var path = folderBrowserDialog.SelectedPath;

                List<string> paths = Directory.GetDirectories(path).ToList();
                if (paths.Count() == 4)
                {
                    for (int i = 0; i < paths.Count(); i++)
                    {
                        switch (paths[i].Substring(paths[i].Length - 2))
                        {
                            case "BK":
                                foreach (var file in Directory.GetFiles(paths[i]))
                                {
                                    imagesList[0].Add(file);
                                }
                                break;
                            case "BM":
                                foreach (var file in Directory.GetFiles(paths[i]))
                                {
                                    imagesList[1].Add(file);
                                }
                                break;
                            case "LK":
                                foreach (var file in Directory.GetFiles(paths[i]))
                                {
                                    imagesList[2].Add(file);
                                }
                                break;
                            case "LM":
                                foreach (var file in Directory.GetFiles(paths[i]))
                                {
                                    imagesList[3].Add(file);
                                }
                                break;
                        }
                    }
                }
            }
            return imagesList;
        }
    }
}
