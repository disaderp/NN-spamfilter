using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            //string fileName = "00485.94b2cb3aa454e6f6701c42cb1fd35ffe";
            //string filePath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\EMAILS\spam4\" + fileName;
            string filePath1 = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\EMAILS\spam4\";
            Dictionary<List<double>, bool> parsed = new Dictionary<List<double>, bool>();
            foreach (string file in Directory.EnumerateFiles(filePath1, "*.*"))
            {
                FileReader fr = new FileReader(file);
                EMail eMail = fr.getEmail();
                Parser parser = new Parser(eMail);
                parser.parseContent();
                var netInputs = parser.getParsedEMail();
                parsed.Add(netInputs, true);//SPAM //@TODO: read folder
            }

            
            //parsuje wszystkie dane Normalizuje wszystkie ( 8 folderów) je  i zapisuje do pliku Parsemax.txt
            Normalizer n = new Normalizer(parsed);
            List<List<double>> newData = new List<List<double>>();
            Dictionary<List<double>, bool> parseTemp = new Dictionary<List<double>, bool>();
            newData = n.normalizeData();
            //System.Console.WriteLine("done");
            for(int i = 0; i< parsed.Count; i++)
            {
                parseTemp.Add(newData.ElementAt(i), parsed.ElementAt(i).Value);
            }
            parsed = parseTemp;
            parseTemp = null;
            
            
           
            //NIEISTOTNE, NIE RUSZAJ
           // foreach (var value in netInputs)
               // System.Console.WriteLine(value);
          /*  string dir = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory()));
            int featureCount = parser.getParsedEMail().Count;
            double[] max = new double[featureCount];
            

            System.IO.StreamReader fileMax = new System.IO.StreamReader(dir + Constants.paths[9]);
            System.IO.StreamReader fileMin = new System.IO.StreamReader(dir + Constants.paths[10]);

            string line;
            int a = 0;
          

            while (fileMax.Peek() > 0)
            {
                double value;
                line = fileMax.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    break;

                if (Double.TryParse(line, out value))
                    max[a++] = value;


            }
            foreach (int i in max)
                System.Console.WriteLine(i);
            System.Console.WriteLine("done");
            */
            //foreach (string line in eMail.getMetaData())
            //  System.Console.WriteLine(line);


              int madam = 0;
              int promotion = 0;

              int republic       = 0;
              int shortest       = 0;
              int mandatory      = 0;
              int standardization= 0;
              int sorry       =    0;
              int supported =      0;
              int people   = 0;
              int enter         =  0;
              int quality       =  0;
              int organization  =  0;
              int investment    =  0;
              int very          =  0;
              int valuable   = 0;
              
            
              System.Console.WriteLine("madam:{0}\n promotion:{1}\n shortest:{2}\n mandatory{3}\n standardization:{4}\n sorry:{5}\n supported:{6}\n people:{7}\n enter:{8}\n quality:{9}\n organization:{10}\n investment:{11}\n very:{12}\n valuable{13}\n republic{14}\n", 
                  madam, promotion,shortest,mandatory,standardization,sorry,supported,people,enter,quality,organization,investment,very,valuable,republic);
              
        
           

            //NIEISTOTNE, patrzylem po prostu ile tagow w sumie wytepuje dla danej grupy mejli
            /*List<string> filess = new List<string>();
            foreach (string file in Directory.EnumerateFiles(filePath1, "*.*"))
            {
                int value = 0;
                string contents = File.ReadAllText(file);
                files.Add(contents);
                foreach (string item in filess)
                {
                    value += Regex.Matches(item, Constants.IMG_PATTERN).Count;
                }
                System.Console.WriteLine(value);    
            }*/
          

            System.Console.ReadKey();

        }
        }     
}
    

