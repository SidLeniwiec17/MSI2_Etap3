using Encog.Neural.Activation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MSI_Etap3.Content
{
    public class InputClass
    {
        public int hiddenLayers { get; set; }
        public int[] hiddenNeurons { get; set; }
        public IActivationFunction activationFunction { get; set; }
        public bool bias { get; set; }
        public int iterations { get; set; }
        public double learningFactor { get; set; }
        public double momentum { get; set; }
        public string learningError { get; set; }
        public string testingError { get; set; }
        public string timeElapsed { get; set; }

        public InputClass()
        {

        }
        public static int[] ParseLayerNeurons(string txt, int layers)
        {
            int[] neuronPerLayer = new int[layers];
            string[] strings = txt.Split(',');
            if (strings.Length != neuronPerLayer.Length)
            {
                return new int[0];
            }
            else
            {
                for (int i = 0; i < strings.Length; i++)
                {
                    if (!int.TryParse(strings[i], out neuronPerLayer[i]))
                    {
                        return new int[0];
                    }
                    else if (neuronPerLayer[i] < 1)
                    {
                        return new int[0];
                    }
                }
            }
            return neuronPerLayer;
        }
        public bool ValidateInput(string TBLayers, string TBNeuronsInLayer, IActivationFunction _activationFunction,
            string TBIteracje, string TBWspUczenia, string TBWspBezwladnosci)
        {
            bool isCorrect = true;
            int _hiddenLayers = 0;
            int[] _hiddenNeurons;
            int _iterations = 0;
            double _learningFactor = 0.0;
            double _momentum = 0.0;

            bool int1 = int.TryParse(TBLayers, out _hiddenLayers);
            if (int1 == false || _hiddenLayers < 1)
            {
                MessageBox.Show("Error ! Network need at least one hidden layer.");
                return false;
            }
            _hiddenNeurons = ParseLayerNeurons(TBNeuronsInLayer, _hiddenLayers);
            if (_hiddenNeurons.Length < 1)
            {
                MessageBox.Show("Error ! Network need at least one neuron in hidden layer.");
                return false;
            }
            int1 = int.TryParse(TBIteracje, out _iterations);
            if (int1 == false || _iterations < 1)
            {
                MessageBox.Show("Error ! Network need at least one iteration.");
                return false;
            }
            int1 = double.TryParse(TBWspUczenia, out _learningFactor);
            if (int1 == false || _learningFactor < 0 || _learningFactor > 1)
            {
                MessageBox.Show("Error ! Learning factor has to be from range [0; 1]");
                return false;
            }
            int1 = double.TryParse(TBWspBezwladnosci, out _momentum);
            if (int1 == false || _momentum < 0 || _momentum > 0.5)
            {
                MessageBox.Show("Error ! Momentm has to be from range [0; 0,5]");
                return false;
            }
            hiddenLayers = _hiddenLayers;
            hiddenNeurons = _hiddenNeurons;
            activationFunction = _activationFunction;
            bias = true;
            iterations = _iterations;
            learningFactor = _learningFactor;
            momentum = _momentum;
            return isCorrect;
        }
        public string ToString()
        {
            string descripction = "";
            descripction += "Hidden Layers : " + hiddenLayers + Environment.NewLine;
            descripction += "Hidden Neurons : ";
            for (int i = 0; i < hiddenNeurons.Length; i++)
            {
                descripction += hiddenNeurons[i];
                if (i < hiddenNeurons.Length - 1)
                    descripction += ",";
            }
            descripction += Environment.NewLine;
            descripction += "Iterations : " + iterations + Environment.NewLine;
            descripction += "Learning Factor : " + learningFactor + Environment.NewLine;
            descripction += "Momentum : " + momentum + Environment.NewLine;

            return descripction;
        }
    }
}
