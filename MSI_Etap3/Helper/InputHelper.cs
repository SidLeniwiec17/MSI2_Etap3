using Encog.Neural.Networks.Training;
using MSI_Etap3.Content;
using Luxand;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Encog.Persist;
using Encog.Neural.Networks.Training.Propagation.Back;

namespace MSI_Etap3.Helper
{
    /// <summary>
    /// Klasa ktora oblsuguje wszystkie wazne metody
    /// zwiazane z obsluga i analiza zdjec
    /// </summary>
    public class InputHelper
    {
        public static Face FacePreparation(String picDir, String _name, String _folderName, int _index, Face twarz, int _folderIndex)
        {
            int Image = 0;
            if (FSDK.LoadImageFromFile(ref Image, picDir) != FSDK.FSDKE_OK)
            {
                Console.WriteLine("addSingleFace error ! ###############################################");
            }
            FSDK.TFacePosition FacePosition = new FSDK.TFacePosition();

            if (FSDK.DetectFace(Image, ref FacePosition) != FSDK.FSDKE_OK)
            {
                Console.WriteLine("addSingleFace error ! ###############################################");
            }

            FSDK.TPoint[] FacialFeatures;

            if (FSDK.DetectFacialFeatures(Image, out FacialFeatures) == FSDK.FSDKE_OK)
            {
                twarz.Name = _name;
                twarz.FolderName = _folderName;
                Console.WriteLine(twarz.Name);
                twarz.ClassIndex = _index;
                twarz.NetworkIndex = _folderIndex;
                List<float> faceFeatures = FeatureConverter.GetFeatures(FacialFeatures);
                twarz.Features = faceFeatures;

                if (twarz.ValidateFace() == 1)
                    Console.WriteLine("Wygenerowano dane twarzy");
                else
                    Console.WriteLine("Blad twarzy " + twarz.Name + " ###############################################");
            }
            else
            {
                Console.WriteLine("addSingleFace error ! ###############################################");
            }
            return twarz;
        }

        public static int SaveBinary(List<Face> faces)
        {
            if (faces.Count < 1)
            {
                MessageBox.Show("Nie ma danych do zapisania");
                return -1;
            }

            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "ZdjeciaInput"; // Default file name
            save.DefaultExt = ".bin"; // Default file extension
            save.Title = "Save As...";
            save.Filter = "Binary File (*.bin)|*.bin";
            save.RestoreDirectory = true;
            save.InitialDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location;

            Nullable<bool> result = save.ShowDialog();
            if (result == true)
            {
                string filename = save.FileName;
                FileStream fs = new FileStream(filename, FileMode.Create);
                BinaryFormatter bf = new BinaryFormatter();
                bf.Serialize(fs, faces);
                BinaryWriter w = new BinaryWriter(fs);
                w.Close();
                fs.Close();
            }
            return 1;
        }

        public static List<Face> LoadBinary()
        {
            List<Face> faces = new List<Face>();
            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Open File...";
            open.Filter = "Binary File (*.bin)|*.bin";
            if (open.ShowDialog() == true)
            {
                FileStream fs = new FileStream(open.FileName, FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                BinaryReader br = new BinaryReader(fs);

                faces = (List<Face>)bf.Deserialize(fs);

                fs.Close();
                br.Close();
            }
            else MessageBox.Show("Nie wybrano pliku !");
            return faces;
        }
        public static ITrain LoadNetwork()
        {
            ITrain network = null;

            OpenFileDialog open = new OpenFileDialog();
            open.Title = "Open File...";
            open.Filter = "Binary File (*.bin)|*.bin";
            if (open.ShowDialog() == true)
            {
                FileStream fs = new FileStream(open.FileName, FileMode.Open);
                BinaryFormatter bf = new BinaryFormatter();
                BinaryReader br = new BinaryReader(fs);

                var _varNetw = (ITrain)bf.Deserialize(fs);
                network = _varNetw;

                fs.Close();
                br.Close();
            }
            else MessageBox.Show("Nie wybrano pliku !");
            return network;
        }

        public static int SaveNetwork(ITrain learnedNetwork)
        {
            if (learnedNetwork == null)
            {
                MessageBox.Show("Nie ma sieci neuronowej do zapisania");
                return -1;
            }


            SaveFileDialog save = new SaveFileDialog();
            save.FileName = "NeuralNetwork"; // Default file name
            save.DefaultExt = ".ser"; // Default file extension
            save.Title = "Save As...";
            save.Filter = "Serialized File (*.ser)|*.ser";
            save.RestoreDirectory = true;
            save.InitialDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location;

            Nullable<bool> result = save.ShowDialog();
            if (result == true)
            {
                string filename = save.FileName;
                Encog.Util.SerializeObject.Save(filename, learnedNetwork);
                //FileStream fs = new FileStream(filename, FileMode.Create);
                //BinaryFormatter bf = new BinaryFormatter();

                //bf.Serialize(fs, learnedNetwork);
                //BinaryWriter w = new BinaryWriter(fs);
                //w.Close();
                //fs.Close();
            }
            return 1;
        }

    }
}
