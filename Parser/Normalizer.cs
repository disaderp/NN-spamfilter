using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Parser
{
    class Normalizer
    {

        private List<List<double>> normalizedData = new List<List<double>>();
        private string dir = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));

        public Normalizer(Dictionary<List<double>, bool> parsed)
        {
            foreach (KeyValuePair<List<double>, bool> val in parsed)
            {
                normalizedData.Add(val.Key);
            }
        }

        public List<List<double>> normalizeData()
        {
            List<List<double>> newData = new List<List<double>>();
            
            int count = normalizedData.ElementAt(0).Count; //get feature count
            double[] max = new double[count];
            double[] min = new double[count];

            for (int i = 0; i < count ; ++i) //count max and min value for each feature
            {
                double maximum = normalizedData.ElementAt(0).ElementAt(i);
                double minimum = normalizedData.ElementAt(0).ElementAt(i);
                for ( int j = 0; j < normalizedData.Count; ++j)
                {
                    if (normalizedData.ElementAt(j).ElementAt(i) > maximum)
                        maximum = normalizedData.ElementAt(j).ElementAt(i);

                    if (normalizedData.ElementAt(j).ElementAt(i) < minimum)
                        minimum = normalizedData.ElementAt(j).ElementAt(i);

                }
                max[i] = maximum;
                min[i] = minimum;
            }
              
            

            for (int i = 0; i < normalizedData.Count; ++i)
            {
                List<double> temp = new List<double>();
                for (int j = 0; j < count; j++)
                {
                    if((max[j] - min[j]) == 0)
                    {
                        temp.Add(0);
                        continue;
                    }
                    temp.Add((2) / (max[j] - min[j]) * (normalizedData.ElementAt(i).ElementAt(j) - max[j]) + 1); //accual normalization
                }
                newData.Add(temp);
            }
            return newData;
            

        }

        
    }  
}
