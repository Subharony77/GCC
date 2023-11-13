using GCCWebAPI.RequestObjects;
using GCCWebAPI.ResponseObjects;

namespace GCCWebAPI.ProcessorForAllQuestions
{
    public class Processor
    {
        public Processor() { }

        public FileRearrangeResponse GetFileRearrangeOutput(RequestFileString requestFileString)
        {
            var listToReturn = new List<int>();

            foreach (var file in requestFileString.inputs) {
                var largestLen = CalculatelargestPallindrome(file);
                listToReturn.Add(largestLen);

            }

            var newObject = new FileRearrangeResponse();
            newObject.answer = listToReturn;    
            return newObject;    
        }

        public  int CalculatelargestPallindrome(string FileName)
        {
            // Counting the occurrences of each character in the string
            Dictionary<char, int> noOfOccurrance = new Dictionary<char, int>();
            foreach (char c in FileName)
            {
                if (noOfOccurrance.ContainsKey(c))
                {
                    noOfOccurrance[c]++;
                }
                else
                {
                    noOfOccurrance[c] = 1;
                }
            }

            int len = 0;
            int oddCount = 0;

            foreach (int count in noOfOccurrance.Values)
            {
                len += (count / 2) * 2;

                 //if odd count then only one is added to oddcount. e.g if it is 5 then 4 will be considered and plus 1 
                if (count % 2 != 0)
                {
                    oddCount = 1;
                }
            }

            // If there is an odd count character, add one to the length
            len += oddCount;

            return len;
        }
    }
}
