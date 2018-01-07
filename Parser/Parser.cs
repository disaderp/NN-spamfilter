using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Parser
{
    class Parser
    {
        private EMail eMail;
        private const string wordPattern = "[a-zA-Z]+";
        private const string htmlPattern = "<[^>]+>";
       // private const string sentencePattern = "(\a|[\.!\?:])[^\.!\?:]+";
        private List <double> parsedEmail;
        private float wordCount;
        private float charCount;
        private float sentenceCount;
       
        

        public Parser(EMail email)
        {
            eMail = email;
            parsedEmail = new List<double>();
        }

        public void countBasicFeatures()
        {
            parsedEmail.Add(Regex.Matches(eMail.getContent(), htmlPattern).Count); //html tags number
            StripHTML();
            //charCount = Regex.Matches(eMail.getContent(), "[a-zA-Z]+").Count); //total character number in message
            //sentenceCount = Regex.Matches(eMail.getContent(), sentencePattern).Count;
            wordCount = Regex.Matches(eMail.getContent(), wordPattern).Count;//total words number in message
            float avgWordLength = charCount / wordCount;
            parsedEmail.Add(charCount);
            parsedEmail.Add(wordCount);
            parsedEmail.Add(avgWordLength);
            parsedEmail.Add(eMail.getContent().Count(Char.IsUpper)/charCount);
            parsedEmail.Add(eMail.getContent().Count(Char.IsWhiteSpace)/charCount);// whitespaces note: a lot of occured after deleting html tags
           
            parsedEmail.Add(Regex.Matches(eMail.getContent(), htmlPattern).Count); //html tags number
            
            
            parsedEmail.Add(eMail.getContent().Count(char.IsLetterOrDigit)/charCount);//alphanumeric char ratio
            parsedEmail.Add(eMail.getContent().Count(Char.IsDigit)/charCount); //digit chars ratio
          

        }
        /*
        public void countAvgSentenceLength()
        {

        }
        public void countWordLengthFrequency()
        {

        }
        public void countUniqueWordsFrequency()
        {

        }
        public void countHapaxLegFrequency()
        {

        }
        public void countHapaxDislegFrequency()
        {

        }
        public void countYuleMeasure()
        {

        }
        public void countSimpsonMeasure()
        {

        }
        public void countSichelMeasure()
        {

        }
        public void countBrunetMeasure()
        {

        }
        public void countHonoreMeasure()
        {

        }
        public void countPunctCharFrequency()
        {

        }
        */
        public List<double> getParsedEMail()
        {
            return parsedEmail;
        }

 
        public void StripHTML()
        {
             eMail.setContent(Regex.Replace(eMail.getContent(), htmlPattern, String.Empty));
        }

        public void parseContent()
        {
         countBasicFeatures();
            
        }
      
      
         
    };
}
