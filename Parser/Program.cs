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
            string filePath1 = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\EMAILS\";
            Dictionary<List<double>, bool> parsed = new Dictionary<List<double>, bool>();
            foreach (string folder in Constants.cleanpaths)
            {
                foreach (string file in Directory.EnumerateFiles(filePath1 + folder, "*.*"))
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
                foreach (string file in Directory.EnumerateFiles(filePath1 + folder, "*.*"))
                {
                    FileReader fr = new FileReader(file);
                    EMail eMail = fr.getEmail();
                    Parser parser = new Parser(eMail);
                    parser.parseContent();
                    var netInputs = parser.getParsedEMail();
                    parsed.Add(netInputs, true);
                }
            }

            Normalizer n = new Normalizer(parsed);
            List<List<double>> newData = new List<List<double>>();
            Dictionary<List<double>, bool> parseTemp = new Dictionary<List<double>, bool>();
            newData = n.normalizeData();
            for(int i = 0; i< parsed.Count; i++)
            {
                parseTemp.Add(newData.ElementAt(i), parsed.ElementAt(i).Value);
            }
            parsed = parseTemp;
            parseTemp = null;

            string json = JsonConvert.SerializeObject(parsed.Keys);
            json += "NEXTDATA" + JsonConvert.SerializeObject(parsed.Values);

            //foreach (string line in eMail.getMetaData())
            //  System.Console.WriteLine(line);

 
     
            System.Console.WriteLine("done");
            System.Console.ReadKey();

        }
        }     
}
    

