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
        public static double LEARNINGRATE = 4;
        private double bias;
        private bool isInput;
        private double wInput;
        private double _input;
        private Dictionary<Neuron, double> top;
        private double _delta;
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
        public double Delta
        {
            set { _delta = value; }
            get { return _delta; }
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
            if (double.IsNaN(sum))
                return;
            val = sigmoid(sum);
        }
        public void error(double expected)
        {
            if (top == null) { return; }
            foreach(KeyValuePair <Neuron, double> dendrite in top)
            {
                dendrite.Key.Delta += (_delta * dendrite.Value) * derivative(dendrite.Key.Value);//backpropagation
                dendrite.Key.error(expected);
            }
        }
        public void updateWeight()
        {
            if(isInput)
            {
                wInput = wInput + LEARNINGRATE * _delta * _input;
                if (double.IsNaN(wInput))
                    return;
            }
            else
            {
                bias = bias + LEARNINGRATE * _delta;
                for (int i = 0; i<top.Count(); i++)
                {
                    KeyValuePair<Neuron, double> dendrite = top.ElementAt(i);
                    top[dendrite.Key] = dendrite.Value + LEARNINGRATE * _delta * dendrite.Key.Value;
                }
            }
        }
        public void clearErr()
        {
            _delta = 0;
        }
        public double sigmoid(double x)
        {
            if (double.IsNaN(1.0 / (1.0 + Math.Exp(-x))))
                return 0.0 ;
            return 1.0 / (1.0 + Math.Exp(-x));
        }
        public double derivative(double x)
        {
            return x * (1.0 - x);
        }
    }
}
