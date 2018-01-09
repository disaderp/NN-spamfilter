using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class Network
    {
        private Neuron outputn;
        private List<Neuron> listin;
        private List<Neuron> listhid;
        public Network(int inputs, int hidden)
        {
            listin = new List<Neuron>();
            for (int i = 0; i< inputs; i++)
            {
                Neuron n = new NeuralNetwork.Neuron(true, null);
                listin.Add(n);
            }
            listhid = new List<Neuron>();
            for (int i = 0; i < hidden; i++)
            {
                Neuron n = new NeuralNetwork.Neuron(false, listin);
                listin.Add(n);
            }

            outputn = new NeuralNetwork.Neuron(false, listhid);
        }
        public void updateInputs(List<double> inputs)
        {
            for(int i = 0; i < inputs.Count(); i++)
            {
                listin.ElementAt(i).Input = inputs.ElementAt(i);
            }
        }
        public double getOutput()
        {
            foreach(Neuron n in listin)
            {
                n.calculate();
            }
            foreach (Neuron n in listhid)
            {
                n.calculate();
            }
            outputn.calculate();
            return outputn.Value;
        }
        public void calcErr(double value)//real value
        {

        }
    }
}
