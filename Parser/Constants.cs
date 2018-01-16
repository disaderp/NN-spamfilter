using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Parser
{
    public static class Constants
    {
        public static readonly string ENCODING = "Content-Transfer-Encoding: base64"; //is email encoded
        public static readonly string MTYPE = "Content-Type: multipart"; //is email multipart
        public static readonly string HTYPE = "Content-Type: text/html"; //is email in html

        public static readonly string WORD_PATTERN = @"(\w+)"; //word pattern for regex
        public static readonly string HTML_PATTERN = "<[^>]+>"; //html pattern for regex
        public static readonly string LONG_WORD_PATTERN = @"[a-zA-Z]{2,}"; //more than 2 words
        public static readonly string HREF_PATTERN = "<a href=";
        public static readonly string LINK_PATTERN = "http://";
        public static readonly string IMG_PATTERN = "<img";
        public static readonly string FONT_PATTERN = "<font";
        public static readonly string SENTENCE_PATTERN = @"(\S.+?[.!?])(?=\s+|$)";
        public static readonly string PUNCTATION_CHAR =@"[\p{P}]";
        public static readonly string SPECIAL_CHAR = @"[*',_+=%$@-/\]";
        public static readonly double value = 0.17; //brunet measure value
        public static readonly string[] COMMON_WORDS = { "shortest", "mandatory", "standardization", "sorry", "supported", "people's", "enter", "quality", "organization", "ivestment", "vary", "valuable", "republic" };

        public static readonly string[] paths = { "easyHam", "easyHam2", "easyHam3", "hardHam", "hardHam2", "spam", "spam2", "spam3", "spam4", "max.txt", "min.txt" };
        public static readonly string[] spampaths = {  "spam", "spam2", "spam3", "spam4" };
        public static readonly string[] cleanpaths = { "easyHam", "easyHam2", "easyHam3", "hardHam", "hardHam2" };

    }
}
