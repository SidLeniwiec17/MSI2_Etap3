using Encog.Neural.Data;
using Encog.Neural.Networks.Training;
using Encog.Neural.NeuralData;
using MSI_Etap3.Content;
using MSI_Etap3.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSI_Etap3.ReportHelper
{
    public class Report
    {
        public static List<Tuple<INeuralDataSet, INeuralDataSet>> GetCrossValidationData(List<Face> learningFaces, List<Face> testingFaces, int pairs)
        {
            Random rnd = new Random();
            List<Tuple<INeuralDataSet, INeuralDataSet>> crossValidation = new List<Tuple<INeuralDataSet, INeuralDataSet>>();
            List<Face> allData = new List<Face>();
            foreach (var f in learningFaces)
                allData.Add(f);
            foreach (var f in testingFaces)
                allData.Add(f);

            List<Face> tmpLearningFaces = new List<Face>();
            List<Face> tmpTestingFaces = new List<Face>();
            for (int i = 0; i < pairs; i++)
            {
                RandomizeSet(rnd, allData);
                for(int j = 0; j < allData.Count; j++)
                {
                    if(j<learningFaces.Count)
                    {
                        tmpLearningFaces.Add(allData[j]); 
                    }
                    else
                    {
                        tmpTestingFaces.Add(allData[j]); 
                    }
                }
                double[][] neuralLearningInput = NetworkHelper.CreateLearningInputDataSet(tmpLearningFaces);
                double[][] neuralLearningOutput = NetworkHelper.CreateLearningOutputDataSet(tmpLearningFaces, 4);
                INeuralDataSet learningSet = NetworkHelper.NormaliseDataSet(neuralLearningInput, neuralLearningOutput);
                double[][] neuralTestingInput = NetworkHelper.CreateLearningInputDataSet(tmpTestingFaces);
                double[][] neuralTestingOutput = NetworkHelper.CreateLearningOutputDataSet(tmpTestingFaces, 4);
                INeuralDataSet testingSet = NetworkHelper.NormaliseDataSet(neuralLearningInput, neuralLearningOutput);
                crossValidation.Add(new Tuple<INeuralDataSet, INeuralDataSet>(learningSet,testingSet));
            }

            return crossValidation;
        }
        public static void RandomizeSet(Random rnd, List<Face> data)
        {
            for (int i = 0; i < data.Count; i++)
            {
                int newPosition = NextPosition(rnd, data.Count);
                Face tmpFace = data[newPosition];
                data[newPosition] = data[i];
                data[i] = tmpFace;
            }
        }
        public static int NextPosition(Random rnd, int length)
        {
            int min = 0;
            int max = length - 1;
            int newPosition = rnd.Next(min, max);
            return newPosition;
        }
        public static Tuple<double, ConfusionMatrix> LearnNetwork(INeuralDataSet learningSet, INeuralDataSet testingSet, int inputSize, InputClass inputData, int testingSize)
        {
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
                Console.WriteLine("Epoch #" + iteracja + " Error:" + Network.Error);
                errors.Add(Network.Error);
                iteracja++;
            } while ((iteracja < iteracje) && (Network.Error > 0.0001) && (Network.Error < 10000));
            stopwatch.Stop();
            
            /// TUTAJ SIEC SIE TEORETYCZNIE NAUCZYLA
         
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
            double calculateError = NetworkHelper.CalculateFinalError(answers, testingSet);
           

            if ((errors[errors.Count - 1] * 100).ToString().Length > 4)
                inputData.learningError = (errors[errors.Count - 1] * 100).ToString().Substring(0, 4) + " %";
            else
                inputData.learningError = (errors[errors.Count - 1] * 100).ToString() + " %";
            if (calculateError.ToString().Length > 4)
                inputData.testingError = calculateError.ToString().Substring(0, 4) + " %";
            else
                inputData.testingError = calculateError.ToString() + " %";
            inputData.timeElapsed = stopwatch.Elapsed.Hours + "h " + stopwatch.Elapsed.Minutes + "min " + stopwatch.Elapsed.Seconds + "sec";

            ConfusionMatrix confusionMatrix = new ConfusionMatrix(4);
            CalculateConfusionMatrix(confusionMatrix, answers, testingSet);

            return new Tuple<double, ConfusionMatrix>(calculateError, confusionMatrix);
        }
        public static void CalculateConfusionMatrix(ConfusionMatrix confusionMatrix, int[] networkAnswers, INeuralDataSet expectedResults)
        {
            int properAnswer = 0;
            double[] neuralAnswers = new double[networkAnswers.Count()];
            int j = 0;
            foreach (INeuralDataPair pair in expectedResults)
            {
                for (int r = 0; r < 4; r++)
                {
                    if ((double)(pair.Ideal.Data[r]) >= 0.66)
                        neuralAnswers[j] = (double)r;
                }
                j++;
            }

            Console.WriteLine("test");
            int[] idealAnswers = NetworkHelper.DenormaliseAnswers(neuralAnswers);
            for (int i = 0; i < networkAnswers.Count(); i++)
            {
                if (idealAnswers[i] == networkAnswers[i])
                    properAnswer++;
            }
        }
    }
}
