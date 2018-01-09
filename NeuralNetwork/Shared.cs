using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuralNetwork
{
    static class Shared
    {
        static private Random rand;
        static public double Rand()
        {
            rand = new Random();
            return rand.NextDouble();
        }
    }
}
