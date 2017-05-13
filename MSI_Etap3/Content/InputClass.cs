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
        public int hiddenNeurons { get; set; }
        public IActivationFunction activationFunction { get; set; }
        public bool bias { get; set; }
        public int iterations { get; set; }
        public double learningFactor { get; set; }
        public double momentum { get; set; }
        public string learningError { get; set; }
        public string testingError { get; set; }
        public string peopleNumber { get; set; }
        public string timeElapsed { get; set; }

        public InputClass()
        {

        }
        public bool ValidateInput(string TBLayers, string TBNeuronsInLayer, IActivationFunction _activationFunction,
            string TBIteracje, string TBWspUczenia, string TBWspBezwladnosci, int _peopleNumber)
        {
            bool isCorrect = true;
            int _hiddenLayers = 0;
            int _hiddenNeurons = 0;
            int _iterations = 0;
            double _learningFactor = 0.0;
            double _momentum = 0.0;

            bool int1 = int.TryParse(TBLayers, out _hiddenLayers);
            if (int1 == false || _hiddenLayers < 1)
            {
                MessageBox.Show("Error ! Network need at least one hidden layer.");
                return false;
            }
            int1 = int.TryParse(TBNeuronsInLayer, out _hiddenNeurons);
            if (int1 == false || _hiddenNeurons < 1)
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
            peopleNumber = _peopleNumber.ToString();
            return isCorrect;
        }
    }
}
