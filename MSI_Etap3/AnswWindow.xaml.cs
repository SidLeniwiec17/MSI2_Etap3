using Encog.Neural.Networks.Training;
using MSI_Etap3.Content;
using MSI_Etap3.Helper;
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
using System.Windows.Shapes;

namespace MSI_Etap3
{
    /// <summary>
    /// Interaction logic for AnswWindow.xaml
    /// </summary>
    public partial class AnswWindow : Window
    {
        public List<Face> faces;
        int peopleNumber;
        public ITrain network;

        public AnswWindow()
        {
            InitializeComponent();
            faces = new List<Face>();
        }

        private void LoadPic_Click(object sender, RoutedEventArgs e)
        {
            faces.Clear();
            faces = InputHelper.LoadBinary();
            if (faces.Count >= 1)
            {
                int peopleCounter = 0;
                peopleCounter = faces[faces.Count - 1].ClassIndex + 1;
                peopleNumber = peopleCounter;
                Console.WriteLine("wczytano z binarki " + faces.Count + " danych");
            }
        }

        private void LoadNeural_Click(object sender, RoutedEventArgs e)
        {
            network = InputHelper.LoadNetwork();
            if (network != null)
            {
                Console.WriteLine("wczytano z binarki " + faces.Count + " danych");
            }
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            if (faces.Count < 1 || network == null)
            {
                MessageBox.Show("Nie wgrano ktoregos pliku !");
                return;
            }

            List<int> idealNumber = new List<int>();
            List<int> calculatedNumber = new List<int>();
            createGrid();

        }

        public void createGrid()
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                dispGrid.ColumnDefinitions.Clear();
                dispGrid.RowDefinitions.Clear();
                dispGrid.Children.Clear();

                for (int i = 0; i < 2; i++)
                {
                    dispGrid.ColumnDefinitions.Add(
                                  new ColumnDefinition());
                }
                for (int i = 0; i < faces.Count; i++)
                {
                    dispGrid.RowDefinitions.Add(
                                  new RowDefinition());
                }

                for (int i = 0; i < faces.Count; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        Rectangle rect = new Rectangle();
                        Grid.SetColumn(rect, j);
                        Grid.SetRow(rect, i);
                        rect.StrokeThickness = 1;
                        rect.Stroke = Brushes.Red;
                        rect.Fill = Brushes.White;

                        dispGrid.Children.Add(rect);
                    }
                }
            }));
        }
    }
}


