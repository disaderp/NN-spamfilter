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
        private string[] paths = {"easyHam","easyHam2","easyHam3", "hardHam","hardHam2","spam","spam2","spam3","spam4"};

        public void LoadData()
        {
            for (int i = 8; i<9; ++i)
            {
                string dir = Path.GetDirectoryName(Path.GetDirectoryName(System.IO.Directory.GetCurrentDirectory())) + @"\EMAILS\" + paths[i] + @"\";
                foreach (string file in Directory.EnumerateFiles(dir, "*.*"))
                {
                    FileReader fr = new FileReader(file);
                    EMail eMail = fr.getEmail();
                    Parser parser = new Parser(eMail);
                    parser.parseContent();
                    normalizedData.Add(parser.getParsedEMail());
                                    
                }
            }

            foreach (var sublist in normalizedData)
            {
                foreach (var obj in sublist)
                {
                    Console.WriteLine(obj);
                }
            }
        }
    }
}
