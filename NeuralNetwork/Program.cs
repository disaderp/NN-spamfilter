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
            Network net = new NeuralNetwork.Network(2, 4);
            List<double> inputs = new List<double>();
            inputs.Add(1.0);
            inputs.Add(1.0);//1.0
            List<double> inputs2 = new List<double>();
            inputs2.Add(1.0);
            inputs2.Add(0.0);//0.0
            List<double> inputs3 = new List<double>();
            inputs3.Add(0.0);
            inputs3.Add(1.0);//1.0
            List<double> inputs4= new List<double>();
            inputs4.Add(0.0);
            inputs4.Add(0.0);//1.0

            for (int i = 0; i < 10000; i++)
            {
                net.updateInputs(inputs);
                Console.WriteLine(net.getOutput());
                net.calcErr(1.0);
                net.updateInputs(inputs2);
                Console.WriteLine(net.getOutput());
                net.calcErr(0.0);
                net.updateInputs(inputs3);
                Console.WriteLine(net.getOutput());
                net.calcErr(1.0);
                net.updateInputs(inputs4);
                Console.WriteLine(net.getOutput());
                net.calcErr(0.0);
                net.nextEpoch();
            }
        }
    }
}
