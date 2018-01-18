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
            int hidden = 4;//dla 3 nie osiaga najwyzszego wyniku, 6 duzo dluzej sie uczy
            Shared.initialize();
            Console.WriteLine("NeuralNetwork - hiddenLayer=" + hidden + "; LearningRate=" + Neuron.LEARNINGRATE + "; epochDecrease=" + Network.epochdecrease);
            Dictionary<List<double>, bool> emails = new Dictionary<List<double>, bool>();
            List<List<double>> templist;
            bool[] boollist;
            string[] json = File.ReadAllText("validation.txt").Split('X');
            templist = JsonConvert.DeserializeObject<List<List<double>>>(json[0]);
            boollist = JsonConvert.DeserializeObject<bool[]>(json[1]);
            for (int i = 0; i< boollist.Count(); i++)
            {
                emails.Add(templist[i], boollist[i]);
            }
            templist = null;
            boollist = null;

            Network net = new NeuralNetwork.Network(emails.ElementAt(0).Key.Count(), hidden);
            int ok = 0;
            for (int j = 0; j < 100; j++)
            {
                ok = 0;
                for (int i = 0; i < emails.Count(); i++)
                {
                    net.updateInputs(emails.ElementAt(i).Key);
                    bool det = false; if (net.getOutput() > 0.5) det = true;
                    if (det == emails.ElementAt(i).Value) ok++;
                    net.calcErr(boolToDouble(emails.ElementAt(i).Value));
                }
                Console.WriteLine("Epoch " + j + " Detection ratio: " + (float)(ok * 100 / emails.Count()) + "%");
                net.nextEpoch();
            }
            ok = 0;
            for(int i= 0;i < emails.Count(); i++)
            {
                net.updateInputs(emails.ElementAt(i).Key);
                bool det = false; if (net.getOutput() > 0.5) det = true;
                if (det == emails.ElementAt(i).Value) ok++;
            }
            Console.WriteLine("Detection ratio: " + (float)(ok * 100 / emails.Count()) + "%");
            Console.ReadKey();
        }
        static double boolToDouble(bool val)
        {
            if (val) return 1.0;
            return 0.0;
        }
    }
}
