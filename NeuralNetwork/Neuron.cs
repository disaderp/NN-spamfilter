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
        private Dictionary<Neuron, double> top;
        private double delta;
        private double value;
        public Neuron(){
            
        }
        public double Val
        {
            get { return value; }
        }
        public void calculate()
        {
            double sum = bias;
            foreach (KeyValuePair <Neuron, double> parent in top)
            {
                sum += parent.Key.Val * parent.Value;
            }
            value = sigmoid(sum);
        }
        public double sigmoid(double x)
        {
            return 1 / (1 + Math.Exp(-x));
        }
    }
}
