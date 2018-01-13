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
           
            
            //odpalam 1 mejla i parsuje
            FileReader fr = new FileReader(filePath);
            EMail eMail = fr.getEmail();
            Parser parser = new Parser(eMail);
            parser.parseContent();
            var netInputs = parser.getParsedEMail();

            foreach (var value in netInputs)
                System.Console.WriteLine(value);//wyswietlam wartosci sparsowanego mejla

           
             System.Console.WriteLine(eMail.getContent());// wyswietlam zawartosc mejla po usunieciu wszelkiej masci gowna, celem weryfikacji (html etc)
            
           
            
            /* parsuje wszystkie dane Normalizuje wszystkie ( 8 folderów) je  i zapisuje do pliku Parsemax.txt
            Normalizer n = new Normalizer();
            n.loadData();
            n.normalizeData();
            System.Console.WriteLine("done");
            */
            
            
            
           
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






            //zliczam wystapienie danych slow w we wszystkich mejlach, tam gdzie wyjdzie 0, trzeba je wyjebiac z constants i mozesz pokminic
            // jakie moga byc w spmie a nie w hard hamie, jak nie to razem pokmninimy w sored
            //POTRWA 30 SEKUND ZANIM JE POLICZY, WIEC NIE WYLACZAJ OD RAZU JAK CI SIE WYSWIETLI CONTENT I WARTOSCI LICZBOWE
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
           
 // PODSUMOWUJAC, W MEJNIE ODPALAM JEDNEGO MEJLA, PARSUJE GO I WYSWIETLAM WARTOSCI A POTEM WYSWIETLAM JEGO CONTENT PO SPODEM LICZE WSYTAPIENIA WYMIENIONYCH SLOW W SUMIE
 // JAK COS, PYTAJ

            System.Console.ReadKey();

        }
        }     
}
    

