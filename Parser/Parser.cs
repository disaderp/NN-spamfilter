using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;

namespace Parser
{
    class Parser
    {  
        private EMail eMail;
        private const string wordPattern = "[a-zA-Z]+";
    
        
        
       // private const string sentencePattern = "(\a|[\.!\?:])[^\.!\?:]+";
        private List <double> parsedEmail;

       
     

        public Parser(EMail email)
        {
            eMail = email;
            parsedEmail = new List<double>();
        }

        
        public void Base64Decode()
        {
            var base64EncodedBytes = System.Convert.FromBase64String(eMail.getContent());
            eMail.setContent (System.Text.Encoding.UTF8.GetString(base64EncodedBytes));
        }

        public void parseContent()
        {
            if (eMail.IsBase64())
                Base64Decode();
            parsedEmail.Add(Regex.Matches(eMail.getContent(), Constants.HTML_PATTERN).Count); //html tags number
            parsedEmail.Add(getHyperTextCount());
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
            parsedEmail.Add(getShortWordsCount());
            parsedEmail.Add(getHeaderLength());
            countSpecialChars();
            countPunctationChars();
            countCommonWords();
            /*
            charCount = Regex.Matches(eMail.getContent(), "[a-zA-Z]+").Count); //total character number in message
            sentenceCount = Regex.Matches(eMail.getContent(), sentencePattern).Count;
            wordCount = Regex.Matches(eMail.getContent(), Constants.WORD_PATTERN).Count;//total words number in message
            float avgWordLength = charCount / wordCount;
            parsedEmail.Add(charCount);
            parsedEmail.Add(wordCount);
            parsedEmail.Add(avgWordLength);
            parsedEmail.Add(eMail.getContent().Count(Char.IsUpper)/charCount);
            parsedEmail.Add(eMail.getContent().Count(Char.IsWhiteSpace)/charCount);// whitespaces note: a lot of occured after deleting html tags
           
            parsedEmail.Add(Regex.Matches(eMail.getContent(), Constants.HTML_PATTERN).Count); //html tags number
            
            
            parsedEmail.Add(eMail.getContent().Count(char.IsLetterOrDigit)/charCount);//alphanumeric char ratio
            parsedEmail.Add(eMail.getContent().Count(Char.IsDigit)/charCount); //digit chars ratio
          */

        }
        public double getTextLength()
        {
            return eMail.getContent().Length;
        }
        public double getCharCount()
        {
            //return eMail.getContent().Count(char.);
           return Regex.Matches(eMail.getContent(), "[a-zA-Z]+").Count;
        }

        public double getAlphaCharsRatio()
        {
            return eMail.getContent().Count(char.IsLetterOrDigit)/getCharCount();
           // return parsedEmail.Add(eMail.getContent().Count(char.IsLetterOrDigit)/getCharCount());
        }

        public double getUpperToLowerRatio()
        {
            return eMail.getContent().Count(char.IsUpper)/eMail.getContent().Count(char.IsLower);
        }

        public double getDigitRatio()
        {
            return eMail.getContent().Count(char.IsDigit)/getCharCount();
        }

        public double getWhiteSpacesRatio()
        {
            return eMail.getContent().Count(char.IsWhiteSpace) / getCharCount();
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

        public double getShortWordsCount()
        {
             return Regex.Matches(eMail.getContent(), Constants.LONG_WORD_PATTERN).Count - Regex.Matches(eMail.getContent(), Constants.HREF_PATTERN).Count;
        }
        //public void analiseWord()
       // {
        //    double wordsCount
      //  }

        public void countSpecialChars()
        {
            foreach ( var item in Constants.SPECIAL_CHARS.Where(x=> !char.IsLetterOrDigit(x)).GroupBy(x => x))
                parsedEmail.Add(item.Count());            
        }
       
        public void countPunctationChars()
        {
            foreach (var item in Constants.PUNCTATION_CHARS.Where(x => !char.IsLetterOrDigit(x)).GroupBy(x => x))
                parsedEmail.Add(item.Count()); 
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
        public void StripHTML()
        {
            eMail.setContent(Regex.Replace(eMail.getContent(), Constants.HTML_PATTERN, String.Empty));// remove all markups
            eMail.setContent(HttpUtility.HtmlDecode(eMail.getContent()));// remove entities
        }

    
        
        public void countCommonWords()
        {
            parsedEmail.Add(Regex.Matches(eMail.getContent(), Constants.COMMON_WORDS[0]).Count);
            parsedEmail.Add(Regex.Matches(eMail.getContent(), Constants.COMMON_WORDS[1]).Count);
            parsedEmail.Add(Regex.Matches(eMail.getContent(), Constants.COMMON_WORDS[2]).Count);
            parsedEmail.Add(Regex.Matches(eMail.getContent(), Constants.COMMON_WORDS[3]).Count);
            parsedEmail.Add(Regex.Matches(eMail.getContent(), Constants.COMMON_WORDS[4]).Count);
            parsedEmail.Add(Regex.Matches(eMail.getContent(), Constants.COMMON_WORDS[5]).Count);
            parsedEmail.Add(Regex.Matches(eMail.getContent(), Constants.COMMON_WORDS[6]).Count);
            parsedEmail.Add(Regex.Matches(eMail.getContent(), Constants.COMMON_WORDS[7]).Count);
            parsedEmail.Add(Regex.Matches(eMail.getContent(), Constants.COMMON_WORDS[8]).Count);
            parsedEmail.Add(Regex.Matches(eMail.getContent(), Constants.COMMON_WORDS[9]).Count);
            parsedEmail.Add(Regex.Matches(eMail.getContent(), Constants.COMMON_WORDS[10]).Count);
            parsedEmail.Add(Regex.Matches(eMail.getContent(), Constants.COMMON_WORDS[11]).Count);
            parsedEmail.Add(Regex.Matches(eMail.getContent(), Constants.COMMON_WORDS[12]).Count);
                
         }
        
      
      //remove entity!!
         
    };
}








