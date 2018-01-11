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
            //Parser parser = new Parser();

            string fileName = "00485.94b2cb3aa454e6f6701c42cb1fd35ffe";
            string filePath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\EMAILS\spam4\" + fileName;
            string filePath1 = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\EMAILS\spam4\";
            FileReader fr = new FileReader(filePath);
            EMail eMail = fr.getEmail();
            Parser parser = new Parser(eMail);
            parser.parseContent();
            var netInputs = parser.getParsedEMail();

            foreach (var value in netInputs)
                System.Console.WriteLine(value);

            //System.Console.WriteLine(eMail.getContent());
            
           /* Normalizer n = new Normalizer();
            n.loadData();
            n.countMaxMin();
            var netInputs = n.normalizeData(parser.getParsedEMail());

            foreach (var value in netInputs)
                System.Console.WriteLine(value);
            */
            System.Console.WriteLine("done");

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
              
              List <string> files = new List<string>(); 
              foreach (string file in Directory.EnumerateFiles(filePath1, "*.*"))
              {
                  string contents = File.ReadAllText(file);
                  files.Add(contents);
                  foreach (string item in files)
                  {
                      if (item.Contains("madam"))
                          ++madam;
                      //else if (item.Contains("promotion"))
                         // ++promotion;
                      else if (item.Contains("shortest"))
                          ++shortest;
                      else if (item.Contains("mandatory"))
                          ++mandatory;
                      else if (item.Contains("standardization"))
                          ++standardization;
                      else if (item.Contains("sorry"))
                          ++sorry;
                      else if (item.Contains("supported"))
                          ++supported;
                      else if (item.Contains("people's"))
                          ++people;
                      else if (item.Contains("enter"))
                          ++enter;
                      else if (item.Contains("quality"))
                          ++quality;
                      else if (item.Contains("organization"))
                          ++organization;
                      else if (item.Contains("investment"))
                          ++investment;
                      else if (item.Contains("very"))
                          ++very;
                      else if (item.Contains("valuable"))
                          ++valuable;
                      else if (item.Contains("republic"))
                          ++republic;
                        
                  }
              }
            
              System.Console.WriteLine("madam:{0}\n, promotion:{1}\n, shortest:{2}\n mandatory{3}\n standardization:{4}\n sorry:{5}\n supported:{6}\n people:{7}\n enter:{8}\n quality:{9}\n organization:{10}\n investment:{11}\n very:{12}\n valuable{13}\n republic{14}\n", 
                  madam, promotion,shortest,mandatory,standardization,sorry,supported,people,enter,quality,organization,investment,very,valuable,republic);
              
        
           /*
            List<string> files = new List<string>();
            foreach (string file in Directory.EnumerateFiles(filePath1, "*.*"))
            {
                int value = 0;
                string contents = File.ReadAllText(file);
                files.Add(contents);
                foreach (string item in files)
                {
                    value += Regex.Matches(item, Constants.IMG_PATTERN).Count;
                }
                System.Console.WriteLine(value);    
            }
           */
            //System.Console.WriteLine(filePath1);

            System.Console.ReadKey();

        }
        }     
}
    

