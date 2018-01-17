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
            Shared.initialize();
            Dictionary<List<double>, bool> emails = new Dictionary<List<double>, bool>();
            List<List<double>> templist;
            bool[] boollist;
            string[] json = File.ReadAllText("parsed.txt").Split('X');
            templist = JsonConvert.DeserializeObject<List<List<double>>>(json[0]);
            boollist = JsonConvert.DeserializeObject<bool[]>(json[1]);
            for (int i = 0; i< boollist.Count(); i++)
            {
                emails.Add(templist[i], boollist[i]);
            }
            templist = null;
            boollist = null;

            Network net = new NeuralNetwork.Network(emails.ElementAt(0).Key.Count(), 4);
            for (int j = 0; j < 10; j++)
            {
                for (int i = 0; i < emails.Count(); i++)
                {
                    net.updateInputs(emails.ElementAt(i).Key);
                    net.getOutput();
                    net.calcErr(boolToDouble(emails.ElementAt(i).Value));

                }
                net.nextEpoch();
            }
            int all = 0;
            int ok = 0;
            for(int i= 0;i < emails.Count(); i++)
            {
                net.updateInputs(emails.ElementAt(i).Key);
                bool det = false; if (net.getOutput() > 0.5) det = true;
                if (det == emails.ElementAt(i).Value) ok++;
                all++;
            }
            Console.WriteLine("Wspolczynnik wykrycia" + (float)ok*100 / all);
        }
        static double boolToDouble(bool val)
        {
            if (val) return 1.0;
            return 0.0;
        }
    }
}
