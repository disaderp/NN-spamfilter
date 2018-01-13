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
        
        public void normalizeData()
        {
            List<List<double>> newData = new List<List<double>>();
            string maxFile = dir + Constants.paths[9];
            string minFile = dir + Constants.paths[10];
            
            int count = normalizedData.ElementAt(0).Count;
            normalizedData.ToArray();
            double[] max = new double[count];
            double[] min = new double[count];

            for (int j = 0; j < count ; ++j)
            {
                max[j] = Enumerable.Range(0, count).Max(i => (normalizedData.ElementAt(i)).ElementAt(j));
                min[j] = Enumerable.Range(0, count).Min(i => (normalizedData.ElementAt(i)).ElementAt(j));
            }

            for (int i = 0; i < normalizedData.Count; ++i )
                for (int j = 0; j < count; j++)
                {
                    List<double> temp = new List<double>();
                    temp.Add(2 * ((double)(normalizedData.ElementAt(i).ElementAt(j) - min[j]) / (max[j] - min[j]) - 1));
                    File.AppendAllLines(maxFile, temp.Select(d => d.ToString()));
                    //newData.Add(temp);
                }

            //return newData;
            
            

        }

        public void loadData()
        {
            for (int i = 0; i < 9; ++i)
            {
                string dir = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\EMAILS\" + Constants.paths[i] + @"\";
                foreach (string file in Directory.EnumerateFiles(dir, "*.*"))
                {
                    FileReader fr = new FileReader(file);
                    EMail eMail = fr.getEmail();
                    Parser parser = new Parser(eMail);
                    parser.parseContent();
                    normalizedData.Add(parser.getParsedEMail());

                }
            }

        }
        }  
}
