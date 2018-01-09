using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    class EMail
    {
        private string content;
        private List <string> metaData;

        public EMail()
        {
            content = "";
            metaData = new List<string>();
        }


        public string getContent()
        {
            return content;
        }

        public void setContent(string value)
        {
            content = value;
        }

        public List <string> getMetaData()
        {
            return metaData;
        }

        public void setMetaData(string value)
        {
            metaData.Add(value);
        }

        public bool IsBase64()
        {
            foreach (string line in metaData)
              if (line.Contains(Constants.ENCODING))
                     return true;
            return false; 
        }

        public bool isHTML()
        {
            foreach (string line in metaData)
                if (line.Contains(Constants.HTYPE))
                    return true;
            return false; 
        }
       
    }
}
