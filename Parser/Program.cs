using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            //Parser parser = new Parser();

            string fileName = "0052.f12ac251d1fbdc679daadc6b97229e63";
            string filePath = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\spam\" + fileName;
            
            FileReader fr = new FileReader(filePath);
            EMail eMail = fr.getEmail();
            Parser parser = new Parser(eMail);
            parser.parseContent();
            var netInputs = parser.getParsedEMail();
            
           System.Console.WriteLine(eMail.getContent());

            foreach (var value in netInputs)
                System.Console.WriteLine(value);

            //foreach (string line in eMail.getMetaData())
              //  System.Console.WriteLine(line);
                                                            
            System.Console.ReadKey();
                               
        }
    }  
}
    

