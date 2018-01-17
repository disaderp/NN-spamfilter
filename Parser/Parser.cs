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
        private double charsCount;
        private double sentencesCount;
        private double wordsCount;
        public bool error = false;
            
        public Parser(EMail email)
        {
            eMail = email;
            parsedEmail = new List<double>();
        }

        
        public void Base64Decode() //decode email if it'g given in base64
        {
            Regex r = new Regex(@"[a-zA-Z0-9\+\/]*={0,3}");
            string text = eMail.getContent().Replace("\r\n", "").Replace("\n", "");
            Match m = r.Match(text);
            var base64EncodedBytes = System.Convert.FromBase64String(m.Value);
            eMail.setContent (System.Text.Encoding.UTF8.GetString(base64EncodedBytes));
        }

        public void parseContent() // main parsing function, where
        {
            if (eMail.IsBase64())
                Base64Decode();


            parsedEmail.Add(Regex.Matches(eMail.getContent(), Constants.HTML_PATTERN).Count); //html tags number
            parsedEmail.Add(getHyperTextCount());
            parsedEmail.Add(getFontTagsCount());
            parsedEmail.Add(getImgTagsCount());
            
            if(eMail.isHTML())
                StripHTML();

            sentencesCount = countSentences();
            wordsCount = countWords();
            charsCount = countChars();
            if (sentencesCount == 0) sentencesCount = 1;
            if (wordsCount == 0) wordsCount = 1;
            if (charsCount == 0) charsCount = 1;

            parsedEmail.Add(getTextLength());
            parsedEmail.Add(charsCount);
            parsedEmail.Add(getAlphaCharsRatio());
            parsedEmail.Add(getUpperToLowerRatio());
            parsedEmail.Add(getDigitRatio());
            parsedEmail.Add(getWhiteSpacesRatio());
            parsedEmail.Add(getLinksCount());
            parsedEmail.Add(wordsCount);
            parsedEmail.Add(getShortWordsCount());
            parsedEmail.Add(getHeaderLength());
            parsedEmail.Add(sentencesCount);
            parsedEmail.Add(getPunctationCharsCount());
           // parsedEmail.Add(getSpecialCharsCount());
            parsedEmail.Add(getCharsInWordRatio());
            countSentenceFeatures();
            countCommonWords();
            analiseWords();

            foreach (double test in parsedEmail)
            {
                if (double.IsNaN(test)) { error = true; }
            }
           
        }
        
        public double getTextLength()
        {
            return eMail.getContent().Length;
        }

        public double countChars()
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
            return (eMail.getContent().Count(char.IsLetterOrDigit))/charsCount;
        }

        public double getUpperToLowerRatio()
        {
            return eMail.getContent().Count(char.IsUpper) /(double)eMail.getContent().Count(char.IsLower);
        }

        public double getDigitRatio()
        {
            return eMail.getContent().Count(char.IsDigit)/charsCount;
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

        public double countWords()
        {
            return Regex.Matches(eMail.getContent(), Constants.WORD_PATTERN).Count;
        }

        public double getCharsInWordRatio()
        {
            return charsCount / wordsCount;
        }

        private double getFontTagsCount()
        {
            return Regex.Matches(eMail.getContent(), Constants.FONT_PATTERN).Count;
        }

        private double getImgTagsCount()
        {
            return Regex.Matches(eMail.getContent(), Constants.IMG_PATTERN).Count;
        }

        public double countSentences()
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
            if(sentencesCount == 0)
            {
                parsedEmail.Add(0);
                parsedEmail.Add(0);
                return;
            }
            parsedEmail.Add(wordCount/ sentencesCount);
            parsedEmail.Add(charCount/ sentencesCount);
        }

        public void analiseWords()// counting some words featuress
        {
           var temp = Regex.Split(eMail.getContent(), Constants.WORD_PATTERN)
                           .AsEnumerable()
                           .GroupBy(w => w)
                           .Select(g => new { key = g.Key, count = g.Count()});

           double simpson = 0; 
           int twiceOccured = 0;
           int onceOccured = 0;
           int unique = temp.Count();
           double N = wordsCount * ( wordsCount - 1); //needed to count simpson measure

           //temp.Max();
           foreach (var item in temp)
           {
               simpson += (double)item.count * ((double)item.count - 1) / N;
               if (item.count == 2)
                   ++ twiceOccured;
               if (item.count == 1)
                   ++onceOccured;
           }
           

      /*int Yule(int M1, int n, int [,] tab) // M1 to liczba wszystkich słów w tekście, n to liczba typów powtórzeń (jednokrotne,dwukrotne, ośmio itp), no i powiedzmy jakaś tablica 2d określająca ile słów wystąpiło n razy.
{
      int K;
      int M2 = 0;
      for (int i = 0; i < n; i++)
      {
           M2 += tab[i,1] * (int)Math.Pow((double)tab[i,0],2.0); // tab[i,0] - liczba powtórzeń, tab[i,1] - liczba słów, które powtórzyły się tab[i,0] razy 
      }   
      K = 10000 * (M2 - M1) / (M1 * M1);
      return K;
}*/


           double bruno = Math.Pow(wordsCount, unique - Constants.value);

           parsedEmail.Add(onceOccured/wordsCount); //hapax legomena
           parsedEmail.Add(twiceOccured/wordsCount); //hapax dislegomena
           parsedEmail.Add(twiceOccured / unique); //sichel measure
           //parsedEmail.Add(100 * Math.Log(wordsCount, 10) / (1 - onceOccured / unique)); // honore measure
          // parsedEmail.Add(bruno); //brunet measure
          parsedEmail.Add(simpson); //simpson measure
        }

        public void StripHTML()
        {
            eMail.setContent(Regex.Replace(eMail.getContent(), Constants.HTML_PATTERN, String.Empty));// remove all markups
            eMail.setContent(HttpUtility.HtmlDecode(eMail.getContent()));// remove entities
        }

        public void countCommonWords()
        {
            int common = 0;
            for (int i = 0; i < Constants.COMMON_WORDS.Count(); ++i)
                common += Regex.Matches(eMail.getContent(), Constants.COMMON_WORDS[i]).Count;
            parsedEmail.Add(common);//scalenie danych
        }    
     
        public double getShortWordsCount()
        {
             return wordsCount - Regex.Matches(eMail.getContent(), Constants.LONG_WORD_PATTERN).Count;
        }

        public double getSpecialCharsCount()
        {
            return Regex.Matches(eMail.getContent(), Constants.SPECIAL_CHAR).Count;
           
        }
       
        public double getPunctationCharsCount()
        {
            return Regex.Matches(eMail.getContent(), Constants.PUNCTATION_CHAR).Count;
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








