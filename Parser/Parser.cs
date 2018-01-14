using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using System.Collections;

namespace Parser
{
    class Parser
    {  
        private EMail eMail;
        private List <double> parsedEmail;

            
        public Parser(EMail email)
        {
            eMail = email;
            parsedEmail = new List<double>();
        }

        
        public void Base64Decode()
        {
            Regex r = new Regex(@"[a-zA-Z0-9\+\/]*={0,3}");
            string text = eMail.getContent().Replace("\r\n", "");
            Match m = r.Match(text);
            var base64EncodedBytes = System.Convert.FromBase64String(m.Value);
            eMail.setContent (System.Text.Encoding.UTF8.GetString(base64EncodedBytes));
        }

        public void parseContent()
        {
            if (eMail.IsBase64())
                Base64Decode();

            parsedEmail.Add(Regex.Matches(eMail.getContent(), Constants.HTML_PATTERN).Count); //html tags number
            parsedEmail.Add(getHyperTextCount());
            parsedEmail.Add(getFontTagsCount());
            parsedEmail.Add(getImgTagsCount());
            
            if(eMail.isHTML())
                StripHTML();

            parsedEmail.Add(getTextLength());
            parsedEmail.Add(getCharCount());
            parsedEmail.Add(getAlphaCharsRatio());
            parsedEmail.Add(getUpperToLowerRatio());
            parsedEmail.Add(getDigitRatio());
            parsedEmail.Add(getWhiteSpacesRatio());
            parsedEmail.Add(getLinksCount());
            parsedEmail.Add(getWordsCount());
            parsedEmail.Add(getImgTagsCount());
            parsedEmail.Add(getFontTagsCount());
            parsedEmail.Add(getShortWordsCount());
            parsedEmail.Add(getHeaderLength());
            parsedEmail.Add(getSentencesCount());
            countSentenceFeatures();
            countSpecialChars();
            countPunctationChars();
            countCommonWords();
          //  getWordsApperance();
            // TO DO funkcje grabara, jesli bedziesz zwracal wartosc to  parsedEmail.Add(fun1), jesli nie to w body dodaj, tak jak np. countCommonWord()
        }
        
        // TO DO fun1, fun2 fun3.......




        public double getTextLength()
        {
            return eMail.getContent().Length;
        }

        public double getCharCount()
        {

            double result = 0;
            foreach (char c in eMail.getContent())
            {
                if (!char.IsWhiteSpace(c))
                {
                    result++;
                }
            }
            return result;

        }

        public double getAlphaCharsRatio()
        {
            return (eMail.getContent().Count(char.IsLetterOrDigit))/getCharCount();
        }

        public double getUpperToLowerRatio()
        {
            return eMail.getContent().Count(char.IsUpper) /(double)eMail.getContent().Count(char.IsLower);
        }

        public double getDigitRatio()
        {
            return eMail.getContent().Count(char.IsDigit)/getCharCount();
        }

        public double getWhiteSpacesRatio()
        {
            return eMail.getContent().Count(char.IsWhiteSpace) / getTextLength();
        }

        public double getHyperTextCount() 
        {
            return Regex.Matches(eMail.getContent(), Constants.HREF_PATTERN).Count;
        }

        public double getLinksCount()
        {
            return Regex.Matches(eMail.getContent(), Constants.LINK_PATTERN).Count;
        }

        public double getWordsCount()
        {
            return Regex.Matches(eMail.getContent(), Constants.WORD_PATTERN).Count;
        }

        public double getCharsInWordRatio()
        {
            return getCharCount() / getWordsCount();
        }

        private double getFontTagsCount()
        {
            return Regex.Matches(eMail.getContent(), Constants.FONT_PATTERN).Count;
        }

        private double getImgTagsCount()
        {
            return Regex.Matches(eMail.getContent(), Constants.IMG_PATTERN).Count;
        }

        public double getSentencesCount()
        {
            return Regex.Matches(eMail.getContent(), Constants.SENTENCE_PATTERN).Count;
        }

        public void countSentenceFeatures()
        {
            double wordCount = 0;
            double charCount = 0;
            MatchCollection matches = Regex.Matches(eMail.getContent(), Constants.SENTENCE_PATTERN);
            var list = matches.OfType<Match>().Select(match => match.Value).ToList();
            foreach (string line in list)
            {
                wordCount += Regex.Matches(line, Constants.WORD_PATTERN).Count;
                foreach (char c in line)
                {
                    if (!char.IsWhiteSpace(c))
                    {
                        charCount++;
                    }
                }
            }
            if(getSentencesCount() == 0)
            {
                parsedEmail.Add(0);
                parsedEmail.Add(0);
                return;
            }
            parsedEmail.Add(wordCount/ getSentencesCount());
            parsedEmail.Add(charCount/getSentencesCount());
        }

        public void getWordsApperance()// TO DO zamienic na regexa Constants.WORD_PATTERN
        {
            var words = Regex.Split(eMail.getContent(), Constants.WORD_PATTERN)
                         .AsEnumerable()
                         .GroupBy(w => w)
                         .Select(g => new { key = g.Key, count = g.Count() });
            foreach (var item in words)
            {
                System.Console.WriteLine(item.key + ":" + item.count);
            
            }
        }

        public void StripHTML()
        {
            eMail.setContent(Regex.Replace(eMail.getContent(), Constants.HTML_PATTERN, String.Empty));// remove all markups
            eMail.setContent(HttpUtility.HtmlDecode(eMail.getContent()));// remove entities
        }

        public void countCommonWords()
        {
            for (int i = 0; i < Constants.COMMON_WORDS.Count(); ++i)
                parsedEmail.Add(Regex.Matches(eMail.getContent(), Constants.COMMON_WORDS[i]).Count);
        }    
     
        public double getShortWordsCount()
        {
             return Regex.Matches(eMail.getContent(), Constants.WORD_PATTERN).Count - Regex.Matches(eMail.getContent(), Constants.LONG_WORD_PATTERN).Count;
        }

        public void countSpecialChars()
        {
            int [] values = Enumerable.Repeat(0, 10).ToArray();//@TODO: niepotrzebne przechowywanie poszczegolnych wartosci
            foreach (char c in eMail.getContent())
            {
                if (c.Equals(Constants.PUNCTATION_CHARS[0]))
                    ++values[0];
                else if (c.Equals(Constants.SPECIAL_CHARS[1]))
                    ++values[1];
                else if (c.Equals(Constants.SPECIAL_CHARS[2]))
                    ++values[2];
                else if (c.Equals(Constants.SPECIAL_CHARS[3]))
                    ++values[3];
                else if (c.Equals(Constants.SPECIAL_CHARS[4]))
                    ++values[4];
                else if (c.Equals(Constants.SPECIAL_CHARS[5]))
                    ++values[5];
                else if (c.Equals(Constants.SPECIAL_CHARS[6]))
                    ++values[6];
                else if (c.Equals(Constants.SPECIAL_CHARS[7]))
                    ++values[7];
                else if (c.Equals(Constants.SPECIAL_CHARS[8]))
                    ++values[8];
                else if (c.Equals(Constants.SPECIAL_CHARS[9]))
                    ++values[9];
            }

            for (int i = 0; i<10; ++i)
            {
                parsedEmail.Add((double)values[i]/getCharCount());
            }
        }
       
        public void countPunctationChars()
        {
            int [] values = Enumerable.Repeat(-0, 18).ToArray();//@TODO: niepotrzebne przechowywanie poszczegolnych wartosci
            foreach (char c in eMail.getContent())
            {
                if(c.Equals(Constants.PUNCTATION_CHARS[0]))
                    ++values[0];
                else if(c.Equals(Constants.PUNCTATION_CHARS[1]))
                    ++values[1];
                else if(c.Equals(Constants.PUNCTATION_CHARS[2]))
                    ++values[2];
                else if(c.Equals(Constants.PUNCTATION_CHARS[3]))
                    ++values[3];
                else if(c.Equals(Constants.PUNCTATION_CHARS[4]))
                    ++values[4];
                else if(c.Equals(Constants.PUNCTATION_CHARS[5]))
                    ++values[5];
                else if(c.Equals(Constants.PUNCTATION_CHARS[6]))
                    ++values[6];
                else if(c.Equals(Constants.PUNCTATION_CHARS[7]))
                    ++values[7];
                else if(c.Equals(Constants.PUNCTATION_CHARS[8]))
                    ++values[8];
                else if(c.Equals(Constants.PUNCTATION_CHARS[9]))
                    ++values[9];
                else if(c.Equals(Constants.PUNCTATION_CHARS[10]))
                    ++values[10];
                else if(c.Equals(Constants.PUNCTATION_CHARS[11]))
                    ++values[11];
                else if(c.Equals(Constants.PUNCTATION_CHARS[12]))
                    ++values[12];
                else if(c.Equals(Constants.PUNCTATION_CHARS[13]))
                    ++values[13];
                else if(c.Equals(Constants.PUNCTATION_CHARS[14]))
                    ++values[14];
                else if(c.Equals(Constants.PUNCTATION_CHARS[15]))
                    ++values[15];
                else if(c.Equals(Constants.PUNCTATION_CHARS[16]))
                    ++values[16];
                else if(c.Equals(Constants.PUNCTATION_CHARS[17]))
                    ++values[17];
            }

            for (int i = 0; i<18; ++i)
            {
                parsedEmail.Add((double)values[i]/getCharCount());
            }
        }
                
        
        public List<double> getParsedEMail()
        {
            return parsedEmail;
        }

        public double getHeaderLength()
        {
            int headerLength = 0;
            foreach (string line in eMail.getMetaData())
                headerLength += line.Length;
            return headerLength;
        }
    };
}








