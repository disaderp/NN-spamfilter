using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\EMAILS\";
            //string netPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory));
           
            Dictionary<List<double>, bool> parsed = new Dictionary<List<double>, bool>();
            foreach (string folder in Constants.cleanpaths)
            {
                foreach (string file in Directory.EnumerateFiles(filePath + folder, "*.*"))
                {
                    FileReader fr = new FileReader(file);
                    EMail eMail = fr.getEmail();
                    Parser parser = new Parser(eMail);
                    parser.parseContent();
                    var netInputs = parser.getParsedEMail();
                    parsed.Add(netInputs, false);
                }
            }
            foreach (string folder in Constants.spampaths)
            {
                foreach (string file in Directory.EnumerateFiles(filePath + folder, "*.*"))
                {
                    FileReader fr = new FileReader(file);
                    EMail eMail = fr.getEmail();
                    Parser parser = new Parser(eMail);
                    parser.parseContent();
                    if (parser.error) continue;
                    var netInputs = parser.getParsedEMail();
                    parsed.Add(netInputs, true);
                }
            }

            Normalizer n = new Normalizer(parsed);
            List<List<double>> newData = new List<List<double>>();
            Dictionary<List<double>, bool> parseTemp = new Dictionary<List<double>, bool>();
            Dictionary<List<double>, bool> validationData = new Dictionary<List<double>, bool>();
            Dictionary<List<double>, bool> learningData = new Dictionary<List<double>, bool>();

            newData = n.normalizeData();
            for(int i = 0; i< parsed.Count; i++)
            {
                parseTemp.Add(newData.ElementAt(i), parsed.ElementAt(i).Value);
            }
            parsed = parseTemp;
            parseTemp = null;
            Random rand = new Random();
            parsed = parsed.OrderBy(x => rand.Next()).ToDictionary(item => item.Key, item => item.Value);
          
            int separator = 0;
            foreach (var item in parsed) // split data into 2 groups: learning and validation
            {
                if (separator % 3 == 0)
                    validationData.Add(item.Key, item.Value);

                else 
                    learningData.Add(item.Key, item.Value);

                ++separator;
            }

            string json = JsonConvert.SerializeObject(validationData.Keys);
            json += "X" + JsonConvert.SerializeObject(validationData.Values);
            
            File.WriteAllText("validation.txt", json);

            json= JsonConvert.SerializeObject(learningData.Keys);
            json += "X" + JsonConvert.SerializeObject(learningData.Values);
            
            File.WriteAllText("learning.txt", json);
            
 
     
            System.Console.WriteLine("done");
            System.Console.ReadKey();

        }
        }     
}
    

