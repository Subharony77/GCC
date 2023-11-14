using GCCWebAPI.RequestObjects;
using GCCWebAPI.ResponseObjects;

namespace GCCWebAPI.ProcessorForAllQuestions
{
    public class Processor
    {
       

        public Processor() { }

        //Question: File rearrangement 
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

        //Question : Portfolio operations
       
        public responsePortfolio portfolioAcc(RequestPortfolio requestPortfolio)
        {
            int maxsum = 0;
            
            List<int> resultSet = new List<int>();

            foreach(List<string> list in requestPortfolio.inputs)
            {
                var numberarrays = new List<List<int>>();
                foreach (string s in list)
                {
                    var numbersList = new List<int>();
                    string[] numberStrings = s.Split(' ');

                    int[] numbersArray = Array.ConvertAll(numberStrings, int.Parse);
                    numbersList = numberStrings.Select(int.Parse).ToList();
                    numberarrays.Add(numbersList);
                }
                

                var arr2 = numberarrays[1];
                var arr3 = numberarrays[2];
                maxsum = numberarrays[0][2];
                Console.WriteLine(numberarrays);

                int result = getEarnings(arr2, arr3, maxsum);
                resultSet.Add(result);
            }

            var newObj = new responsePortfolio();
            newObj.answer = resultSet;
            return newObj;

        }
        public int getEarnings(List<int> arr2, List<int> arr3, int maxsum)
        {

            var minSizeArr = 0;
            var counter = 0;
            var i = 0;
            var j = 0;

            int sum1 = 0;
            int result = 0;

            if (arr2.Count() < arr3.Count())
            {
                minSizeArr = arr2.Count();
            }
            else
            {
                minSizeArr = arr3.Count();
            }
            while (true)
            {
                if (i > arr2.Count() - 1 || j > arr3.Count() - 1)
                {
                    break;
                }
                var a = arr2[i];
                var b = arr3[j];
                if (a < b)
                {
                    sum1 += a;
                    result += 1;
                    i += 1;

                }
                else
                {
                    sum1 += b;
                    result += 1;
                    j += 1;

                }
                //counter += 1;
                if (sum1 > maxsum)
                {
                    result -= 1;
                    break;
                }
            }

            if (i != arr2.Count() - 1)
            {
                while (sum1 <= maxsum && i <= arr2.Count() - 1)
                {
                    sum1 += arr2[i++];
                    result++;
                }
            }
            if (j != arr3.Count() -1)
            {
                while (sum1 <= maxsum && j <= arr3.Count() - 1)
                {
                    sum1 += arr3[j++];
                    result++;
                }
            }


            return result;
        

        }


    }
}
