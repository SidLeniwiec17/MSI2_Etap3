using Encog.Neural.Activation;
using Encog.Neural.Data;
using Encog.Neural.Networks.Training;
using Encog.Neural.NeuralData;
using MSI_Etap3.Content;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSI_Etap3.Helper
{
    public class TestsHelper
    {
        public static int[] hiddenLayers_test = { 1, 2, 4 };
        public static List<int[]> hiddenNeurons_test;
        public static int[] iterations = { 5000, 25000 };
        public static float[] learningFactor = { 0.001f, 0.01f, 0.1f };

        public static void RunTests(INeuralDataSet learningSet, INeuralDataSet testingSet, int inputSize, int testingSize, List<Face> learningFaces, List<Face> testingFaces)
        {
            hiddenNeurons_test = initList();
            int counter = 0;
            for (int i = 0; i < iterations.Length; i++)
            {
                for(int h = 0; h < hiddenLayers_test.Length; h ++)
                 {
                     for (int n = 0; n < hiddenNeurons_test.Count; n++)
                     {
                         if (hiddenNeurons_test[n].Length == hiddenLayers_test[h])
                         {
                             for (int l = 0; l < learningFactor.Length; l++)
                             {
                                 counter++; InputClass configuration = new InputClass();
                                 configuration.hiddenLayers = hiddenLayers_test[h];
                                 configuration.hiddenNeurons = hiddenNeurons_test[n];
                                 configuration.activationFunction = new ActivationSigmoid();
                                 configuration.bias = true;
                                 configuration.iterations = iterations[i];
                                 configuration.learningFactor = (double)learningFactor[l];
                                 configuration.momentum = 0.4;

                                 Learn(learningSet, testingSet, learningFaces[0].Features.Count, configuration, testingFaces.Count, counter);
                             }
                         }
                     }
                 }
            }
        }
        public static List<int[]> initList()
        {
            List<int[]> list = new List<int[]>();
            list.Add(new int[] { 4 });
            list.Add(new int[] { 10 });
            list.Add(new int[] { 20 });
            list.Add(new int[] { 12, 6 });
            list.Add(new int[] { 12, 12 });
            list.Add(new int[] { 20, 6 });
            list.Add(new int[] { 24, 12, 8, 6 });
            list.Add(new int[] { 12, 12, 12, 12 });
            list.Add(new int[] { 24, 24, 24, 24 });
            return list;
        }
        public static async void Learn(INeuralDataSet learningSet, INeuralDataSet testingSet, int inputSize, InputClass inputData, int testingSize, int index)
        {
            string logDir = System.IO.Path.GetDirectoryName(Application.ExecutablePath);
            int iteracje = inputData.iterations;
            List<double> errors = new List<double>();
            Console.WriteLine("Tworze siec...");
            ITrain Network = NetworkHelper.CreateNeuronNetwork(learningSet, inputSize, inputData);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            int iteracja = 0;
            do
            {
                Network.Iteration();
                errors.Add(Network.Error);
                iteracja++;
            } while ((iteracja < iteracje) && (Network.Error > 0.0001) && (Network.Error < 10000));
            stopwatch.Stop();

            String timeStamp = NetworkHelper.GetTimestamp(DateTime.Now);
            /// TUTAJ SIEC SIE TEORETYCZNIE NAUCZYLA
            /// TERAZ ZBIOR TESTOWY, WYNIKI
            /// I WYKRES ERRORA
            /// 
            string errDir = logDir + "\\ERRORS\\err_" + timeStamp + "_" + ".r";
            string log_name = timeStamp + "_" + index;

            Logger(inputData.ToString(), logDir, log_name);

            double[] neuralAnswer = new double[testingSize];
            int i = 0;
            foreach (INeuralDataPair pair in testingSet)
            {
                INeuralData output = Network.Network.Compute(pair.Input);
                double small = 0.0;
                for (int r = 0; r < 4; r++)
                {
                    if ((double)(output[r]) >= small)
                    {
                        neuralAnswer[i] = (double)r;
                        small = (double)(output[r]);
                    }
                }

                i++;
            }
            int[] answers = NetworkHelper.DenormaliseAnswers(neuralAnswer);
            Logger("Neural Network Learning Result" + Environment.NewLine, logDir, log_name);
            Logger(errors[errors.Count - 1] + " %" + Environment.NewLine, logDir, log_name);
            Logger("Neural Network Testing Result" + Environment.NewLine, logDir, log_name);
            double calculateError = NetworkHelper.CalculateFinalError(answers, testingSet);
            Logger("Error: " + calculateError + " %" + Environment.NewLine, logDir, log_name);
            Logger(String.Format("Time elapsed: {0:hh\\:mm\\:ss}", stopwatch.Elapsed) + Environment.NewLine, logDir, log_name);
            Console.WriteLine("FINISH");

            NetworkHelper.CreateErrorFile(errors, errDir);
        }
        public static void Logger(String lines, string dir, string logname)
        {
            string fullPath = dir + "\\LOGS\\log_" + logname + ".txt";
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            System.IO.StreamWriter file = new System.IO.StreamWriter(fullPath, true);
            file.WriteLine(lines);
            file.Close();
        }
    }
}
