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
            //string maxFile = dir + Constants.paths[9];
            //string minFile = dir + Constants.paths[10];
            
            int count = normalizedData.ElementAt(0).Count;
            //normalizedData.ToArray();
            double[] max = new double[count];
            double[] min = new double[count];

            for (int j = 0; j < count ; ++j)
            {
                max[j] = Enumerable.Range(0, count).Max(i => (normalizedData.ElementAt(i)).ElementAt(j));
                min[j] = Enumerable.Range(0, count).Min(i => (normalizedData.ElementAt(i)).ElementAt(j));
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
                    temp.Add((2) / (max[j] - min[j]) * (normalizedData.ElementAt(i).ElementAt(j) - max[j]) + 1);
                }
                newData.Add(temp);
            }
            return newData;
            

        }

        
        }  
}
