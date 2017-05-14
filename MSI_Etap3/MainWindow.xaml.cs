using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Luxand;
using Microsoft.Win32;
using MSI_Etap3.Content;
using MSI_Etap3.Helper;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Encog.Neural.NeuralData;
using Encog.Neural.Data.Basic;
using Encog.Neural.Activation;
using Encog.Neural.Networks.Training;



namespace MSI_Etap3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IActivationFunction ActivationFunction { get; set; }
        public InputClass inputData;
        public int Image;
        public List<Face> learningFaces;
        public List<Face> testingFaces;
        public int peopleNumber;
        public ITrain learnedNetwork;
        public MainWindow()
        {
            FSDK.ActivateLibrary("GwUHaCIMjj0EMCrjclY4umU1p3AIhphyxoTISrKyWZfaDU7bChTUgkK0adDOzouQSoJ2cSGoeoMvv/p1WUJaY4dWrcvpqxJJeOHMDykV2Qh7M95YPJyuGl+S9AMguxlhZbOzn3HvHMST6EGpOSJMqEFCEafIOawugRQ/lkp5d30=");
            FSDK.InitializeLibrary();
            InitializeComponent();
            learningFaces = new List<Face>();
            testingFaces = new List<Face>();
            ActivationFunction = new ActivationSigmoid();
        }

        private async void Load_Pic_Click(object sender, RoutedEventArgs e)
        {
            BlakWait.Visibility = Visibility.Visible;
            learningFaces.Clear();

            List<List<string>> imageList = ImageLoader.GetImages();
            int folderIndex = 0;
            await Task.Run(() =>
            {
                for (int i = 0; i < imageList.Count; i++)
                {
                    folderIndex = i;
                    List<string> pictures = imageList[i];
                    string folderName = imageList[i][0];
                    var folderNames = folderName.Split('\\').ToArray();
                    folderName = folderNames[folderNames.Count() - 2];
                    for (int j = 0; j < pictures.Count; j++)
                    {
                        addSingleFace(pictures[j], pictures[j].Substring(pictures[j].LastIndexOf('\\') + 1), folderName, folderIndex, false);
                    }
                }
                peopleNumber = imageList.Count;
            });
            Console.WriteLine("DONE" + learningFaces.Count);
            BlakWait.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Metoda dodaje jedna twarz do listy. 1 jak sie uda
        /// -1 jak sie nie uda
        /// </summary>
        private int addSingleFace(String picDir, String name, String folderName, int folderIndex, bool testing)
        {
            Face twarz = new Face();
            twarz = InputHelper.FacePreparation(picDir, name, folderName, folderIndex, twarz);

            if (testing)
                testingFaces.Add(twarz);
            else
                learningFaces.Add(twarz);
            return 1;
        }

        /// <summary>
        /// Obsluga eventow z guzikow
        /// </summary>
        private void Save_Pic_Data_Click(object sender, RoutedEventArgs e)
        {
            if (InputHelper.SaveBinary(learningFaces) == 1)
                Console.WriteLine("zapisano do binarki");
        }

        private void Load_Pic_Data_Click(object sender, RoutedEventArgs e)
        {
            learningFaces.Clear();
            learningFaces = InputHelper.LoadBinary();
            if (learningFaces.Count >= 1)
            {
                int peopleCounter = 0;
                peopleCounter = learningFaces[learningFaces.Count - 1].ClassIndex + 1;
                peopleNumber = peopleCounter;
                Console.WriteLine("wczytano z binarki " + learningFaces.Count + " danych");
            }
        }

        private async void Ucz_Siec_Click(object sender, RoutedEventArgs e)
        {
            BlakWait.Visibility = Visibility.Visible;
            inputData = new InputClass();
            if (learningFaces.Count < 1 || learningFaces == null)
            {
                MessageBox.Show("Error ! No data is loaded");
                BlakWait.Visibility = Visibility.Collapsed;
                return;
            }
            if (inputData.ValidateInput(TBLayers.Text, TBNeuronsInLayer.Text, ActivationFunction,
                TBIteracje.Text, TBWspUczenia.Text, TBWspBezwladnosci.Text) == false)
            {
                BlakWait.Visibility = Visibility.Collapsed;
                return;
            }
            else
            {
                LEnumber.Content = "---";
                TEnumber.Content = "---";
                Tnumber.Content = "---";
                await PerformCalculation();
                LEnumber.Content = inputData.learningError.ToString();
                TEnumber.Content = inputData.testingError.ToString();
                Tnumber.Content = inputData.timeElapsed.ToString();
            }
            BlakWait.Visibility = Visibility.Collapsed;
        }

        public async Task PerformCalculation()
        {
            await Task.Run(() =>
            {
                Console.WriteLine("Szykuje dane zbioru uczacego");
                double[][] neuralLearningInput = NetworkHelper.CreateLearningInputDataSet(learningFaces);
                double[][] neuralLearningOutput = NetworkHelper.CreateLearningOutputDataSet(learningFaces, 4);
                double[][] neuralTestingInput = NetworkHelper.CreateLearningInputDataSet(testingFaces);
                double[][] neuralTestingOutput = NetworkHelper.CreateLearningOutputDataSet(testingFaces, 4);
                INeuralDataSet learningSet, testingSet;

                learningSet = NetworkHelper.NormaliseDataSet(neuralLearningInput, neuralLearningOutput);
                testingSet = NetworkHelper.NormaliseDataSet(neuralTestingInput, neuralTestingOutput);

                ITrain network = NetworkHelper.LearnNetwork(learningSet, testingSet, learningFaces[0].Features.Count, inputData, testingFaces.Count);
                learnedNetwork = network;
            });

        }

        private async void LoadTest_Pic_Click(object sender, RoutedEventArgs e)
        {
            BlakWait.Visibility = Visibility.Visible;
            testingFaces.Clear();

            List<List<string>> imageList = ImageLoader.GetImages();
            int folderIndex = 0;
            await Task.Run(() =>
            {
                for (int i = 0; i < imageList.Count; i++)
                {
                    folderIndex = i;
                    List<string> pictures = imageList[i];
                    string folderName = imageList[i][0];
                    var folderNames = folderName.Split('\\').ToArray();
                    folderName = folderNames[folderNames.Count() - 2];
                    for (int j = 0; j < pictures.Count; j++)
                    {
                        addSingleFace(pictures[j], pictures[j].Substring(pictures[j].LastIndexOf('\\') + 1), folderName, folderIndex, true);
                    }
                }
                peopleNumber = imageList.Count;
            });
            Console.WriteLine("DONE" + testingFaces.Count);
            BlakWait.Visibility = Visibility.Collapsed;

        }

        private void SaveTest_Pic_Data_Click(object sender, RoutedEventArgs e)
        {
            if (InputHelper.SaveBinary(testingFaces) == 1)
                Console.WriteLine("zapisano do binarki");
        }

        private void LoadTest_Pic_Data_Click(object sender, RoutedEventArgs e)
        {
            testingFaces.Clear();
            testingFaces = InputHelper.LoadBinary();
            if (learningFaces.Count >= 1)
            {
                int peopleCounter = 0;
                peopleCounter = testingFaces[testingFaces.Count - 1].ClassIndex + 1;
                peopleNumber = peopleCounter;
                Console.WriteLine("wczytano z binarki " + testingFaces.Count + " danych");
            }
        }
    }
}
