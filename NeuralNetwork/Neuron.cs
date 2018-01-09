using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    class Neuron
    {
        private double bias;
        private bool isInput;
        private double wInput;
        private double _input;
        private Dictionary<Neuron, double> top;
        private double delta;
        private double val;
        public Neuron(bool Input, List<Neuron> parent)
        {
            isInput = Input;
            bias = 0;
            wInput = Shared.Rand();
            if (!isInput)
            {
                top = new Dictionary<Neuron, double>();
                foreach (Neuron n in parent)
                {
                    top.Add(n, Shared.Rand());
                }
            }
        }
        public double Value
        {
            get { return val; }
        }
        public double Input
        {
            set { if (this.isInput) { _input = value; } }
        }
        public void calculate()
        {
            double sum = bias;
            if (isInput)
            {
                sum += wInput * _input;
            }
            else
            {
                foreach (KeyValuePair<Neuron, double> parent in top)
                {
                    sum += parent.Key.Value * parent.Value;
                }
            }
            val = sigmoid(sum);
        }
        public double sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }
    }
}
