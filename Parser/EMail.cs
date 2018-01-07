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
        
    }
}
