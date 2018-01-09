using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace NeuralNetwork
{
    class Program
    {
        static void Main(string[] args)
        {
            Shared.initialize();
            Network net = new NeuralNetwork.Network(2, 2);
            List<double> inputs = new List<double>();
            inputs.Add(1.0);
            inputs.Add(0.0);
            List<double> inputs2 = new List<double>();
            inputs2.Add(-1.0);
            inputs2.Add(0.0);
            net.updateInputs(inputs);
            net.getOutput();
            net.calcErr(1.0);
            for (int i = 0; i < 100; i++)
            {
                net.updateInputs(inputs);
                Console.WriteLine(net.getOutput());
                net.calcErr(1.0);
                net.updateInputs(inputs2);
                Console.WriteLine(net.getOutput());
                net.calcErr(0.0);
            }
        }
    }
}
