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
        


        public void countMaxMin()
        {
            string maxFile = dir + Constants.paths[9];
            string minFile = dir + Constants.paths[10];
            
            int count = normalizedData.ElementAt(0).Count;
            
            double[] max = new double[count];
            double[] min = new double[count];

            for (int j = 0; j < count ; ++j)
            {
                max[j] = Enumerable.Range(0, count).Max(i => (normalizedData.ElementAt(i)).ElementAt(j));
                min[j] = Enumerable.Range(0, count).Min(i => (normalizedData.ElementAt(i)).ElementAt(j));
            }

            File.WriteAllLines(maxFile, max.Select(d => d.ToString()));
            File.WriteAllLines(minFile, min.Select(d => d.ToString()));

        }

        public void loadData()
        {
            for (int i = 0; i<9; ++i)
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

        public List<double> normalizeData(List<double> parsedData)
        {
            List<double> newData = new List<double>();
            int mailCount = normalizedData.Count;
            int featureCount = normalizedData.ElementAt(0).Count;
            double [] max = new double[featureCount];
            double [] min = new double[mailCount];

            System.IO.StreamReader fileMax = new System.IO.StreamReader(dir+Constants.paths[9]);
            System.IO.StreamReader fileMin = new System.IO.StreamReader(dir+Constants.paths[10]);
            
            string line;
            int a = 0;
            int b = 0;
            
            while (fileMax.Peek() > 0)
            {
                line = fileMax.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    break;
                System.Console.WriteLine(max[a]);
                max[a++] = Convert.ToDouble(line);
               
            }

            while (fileMin.Peek() > 0)
            {
                line = fileMin.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    break;
                System.Console.WriteLine(max[b]);
                min[b++] = Convert.ToDouble(line);
                
            }

          
            for (int j = 0; j < featureCount; j++)
            {
               newData.Add(2 * ((double)(parsedData.ElementAt(j)) - min[j]) / (max[j] - min[j]) - 1);
            }
            
            return newData;
        }
           /* foreach (var sublist in normalizedData)
            {
                foreach (var obj in sublist)
                {
                    Console.WriteLine(obj);
                }
            }*/
        }
    
}
