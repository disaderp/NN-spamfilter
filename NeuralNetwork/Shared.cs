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
        static public void initialize()
        {
            rand = new Random();
        }
        static public double Rand()
        {
            return rand.NextDouble();
        }
    }
}
