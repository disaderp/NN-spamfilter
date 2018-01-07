using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class FileReader
    {
        private System.IO.StreamReader file;


        public FileReader(string filename)
        {
            file = new System.IO.StreamReader(filename);
        }
        
     
        public EMail getEmail()
        {
            EMail eMail = new EMail();
            bool isSkipped = false;
            bool isMulti = false;
            string line;

            while (file.Peek() > 0)
            {
                line = file.ReadLine();
                if (string.IsNullOrWhiteSpace(line)) // possibly another lines of metadata
                {
                    foreach (string item in eMail.getMetaData())
                    {
                        if (item.Contains("Content-Type: multipart"))
                            isMulti = true;
                    }

                    if (!isSkipped && isMulti)
                    {
                        isSkipped = true;
                        continue;
                    }

                    break;
                }

                eMail.getMetaData().Add(line);
            }
            eMail.setContent(file.ReadToEnd());//read the rest to EMail.content

            return eMail;
        }            
    }
    
}
