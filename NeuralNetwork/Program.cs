using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace NeuralNetwork
{
    class Program
    {  
        static void Main(string[] args)
        {
            int hidden = 4;
            Shared.initialize();
            Console.WriteLine("NeuralNetwork - hiddenLayer=" + hidden + "; LearningRate=" + Neuron.LEARNINGRATE + "; epochDecrease=" + Network.epochdecrease);
            Dictionary<List<double>, bool> train = new Dictionary<List<double>, bool>();
            Dictionary<List<double>, bool> validate = new Dictionary<List<double>, bool>();
            List<List<double>> templist;
            bool[] boollist;
            string[] json = { };
            try
            {
                json = File.ReadAllText("learning.txt").Split('X');
            }catch(Exception x)
            {
                Console.WriteLine("No file found. Run Parser.exe first");
                Console.ReadLine();
                Environment.Exit(1);
            }
            templist = JsonConvert.DeserializeObject<List<List<double>>>(json[0]);
            boollist = JsonConvert.DeserializeObject<bool[]>(json[1]);
            for (int i = 0; i< boollist.Count(); i++)
            {
                train.Add(templist[i], boollist[i]);
            }

            json = File.ReadAllText("validation.txt").Split('X');
            templist = JsonConvert.DeserializeObject<List<List<double>>>(json[0]);
            boollist = JsonConvert.DeserializeObject<bool[]>(json[1]);
            for (int i = 0; i < boollist.Count(); i++)
            {
                validate.Add(templist[i], boollist[i]);
            }
            templist = null;
            boollist = null;

            Network net = new NeuralNetwork.Network(train.ElementAt(0).Key.Count(), hidden);
            int ok = 0;
            for (int j = 0; j < 30; j++)
            {
                ok = 0;
                for (int i = 0; i < train.Count(); i++)
                {
                    net.updateInputs(train.ElementAt(i).Key);
                    bool det = false; if (net.getOutput() > 0.5) det = true;
                    if (det == train.ElementAt(i).Value) ok++;
                    net.calcErr(boolToDouble(train.ElementAt(i).Value));
                }
                Console.WriteLine("Epoch " + j + " Training data detection ratio: " + (float)(ok * 100 / train.Count()) + "%");
                net.nextEpoch();
            }
            ok = 0;
            int falsepos = 0;
            int falseneg = 0;
            for(int i= 0;i < validate.Count(); i++)
            {
                net.updateInputs(validate.ElementAt(i).Key);
                bool det = false; if (net.getOutput() > 0.5) det = true;
                if (det == validate.ElementAt(i).Value) ok++;
                else if (det) falsepos++; else falseneg++;
            }
            Console.WriteLine("Validation data detection ratio: " + (float)(ok * 100 / validate.Count()) + "%");
            Console.WriteLine("Validation data false positive count: " + falsepos);
            Console.WriteLine("Validation data false negative count: " + falseneg);
            Console.WriteLine("Validation data count: " + validate.Count());
            Console.ReadLine();
        }
        static double boolToDouble(bool val)
        {
            if (val) return 1.0;
            return 0.0;
        }
    }
}
