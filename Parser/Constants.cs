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

        public static readonly string WORD_PATTERN = "[a-zA-Z]+"; //word pattern for regex
        public static readonly string HTML_PATTERN = "<[^>]+>"; //html pattern for regex
        public static readonly string LONG_WORD_PATTERN = @"[a-zA-Z]{2,}"; //more than 2 words
        public static readonly string WORD_PATTER = @"(?:[a-z]{2,}|[ai])"; //all word, regardless of lengt
        public static readonly string HREF_PATTERN = "<a href=";
        public static readonly string LINK_PATTERN = "http://";

        public static readonly char[] PUNCTATION_CHARS = { '.', '،', ';', '?', '!', ':', '(', ')', '–', '“', '«', '»', '<', '>', '[', ']', '{', '}' };
        public static readonly char[] SPECIAL_CHARS = {'*', '_', '+', '=', '%', '$', '@', 'ـ', '/', '\"' };
        public static readonly string[] COMMON_WORDS = { "shortest", "mandatory", "standardization", "sorry", "supported", "people's", "enter", "quality", "organization", "ivestment", "vary", "valuable", "republic" };
    }
}
