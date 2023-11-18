using GCCWebAPI.RequestObjects;
using GCCWebAPI.ResponseObjects;
using System;

namespace GCCWebAPI.ProcessorForAllQuestions
{
    public class Processor
    {


        public Processor() { }

        //Question: File rearrangement 
        public FileRearrangeResponse GetFileRearrangeOutput(RequestFileString requestFileString)
        {
            var listToReturn = new List<int>();

            foreach (var file in requestFileString.inputs)
            {
                var largestLen = CalculatelargestPallindrome(file);
                listToReturn.Add(largestLen);

            }

            var newObject = new FileRearrangeResponse();
            newObject.answer = listToReturn;
            return newObject;
        }

        public int CalculatelargestPallindrome(string FileName)
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

            foreach (List<string> list in requestPortfolio.inputs)
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


                var arr1 = numberarrays[1];
                var arr2 = numberarrays[2];
                maxsum = numberarrays[0][2];
                Console.WriteLine(numberarrays);

                int result = getEarnings(arr1.ToArray(), arr2.ToArray(), maxsum);
                resultSet.Add(result);
            }

            var newObj = new responsePortfolio();
            newObj.answer = resultSet;
            return newObj;

        }
        public int getEarnings(int[] a, int[] b, int maxSum)
        {
            //List<int> sumArr1 = new List<int>();
            //List<int> sumArr2 = new List<int>();
            //sumArr1.Add(arr1[0]);
            //sumArr2.Add(arr2[0]);
            //for (int i = 1; i < arr1.Count(); i++)
            //{
            //    sumArr1.Add(arr1[i] + sumArr1[i - 1]);
            //}

            //for (int j = 1; j < arr2.Count(); j++)
            //{
            //    sumArr2.Add(arr2[j] + sumArr2[j - 1]);
            //}

            //int arr2Index = arr2.Count() - 1;
            //int arr1Index = arr1.Count() - 1;

            //while ((sumArr1[arr1Index] > maxsum))
            //{
            //    arr1Index--;
            //    if (arr1Index < 0)
            //        break;
            //}

            //while ((sumArr2[arr2Index] > maxsum) )
            //{
            //    arr2Index--;
            //    if (arr2Index < 0)
            //        break;
            //}

            //if(arr1Index < 0 && arr2Index < 0)
            //{
            //    return 0;
            //}

            //if(arr1Index <0 )
            //{
            //    return arr2Index + 1;
            //}

            //if( arr2Index < 0 )
            //{
            //    return arr1Index + 1;
            //}
            //int currSum = sumArr1[arr1Index] + sumArr2[arr2Index];

            //while (currSum > maxsum)
            //{
            //    if (arr1[arr1Index] > arr2[arr2Index])
            //    {
            //        arr1Index--;
            //        if(arr1Index < 0) { break; }
            //    }
            //    else
            //    {
            //        arr2Index--;
            //        if (arr2Index < 0) { break; }
            //    }
            //    currSum = sumArr1[arr1Index] + sumArr2[arr2Index];
            //}

            //return (arr1Index + arr2Index + 2);

            int i = 0, j = 0, currSum = 0, maxCount = 0;

            while (i < a.Length && currSum + a[i] <= maxSum)
            {
                currSum += a[i];
                i++;
            }

            if (currSum > maxSum)
            {
                i--;
                maxCount = Math.Max(maxCount, i);
            }
            else
            {
                maxCount = Math.Max(maxCount, i);
                i--;
            }

            while (j < b.Length && currSum <= maxSum)
            {
                currSum += b[j];
                j++;

                while (currSum > maxSum && i >= 0)
                {
                    currSum -= a[i];
                    i--;
                }

                if (currSum <= maxSum)
                {
                    maxCount = Math.Max(maxCount, i + j + 1);
                }
            }

            return maxCount;
        }

        //question CoinChange


        public ResponseCoinChange caluculateCoinChange(RequestPortfolio requestPortfolio)
        {

            List<long> resultSet = new List<long>();

            foreach (List<string> list in requestPortfolio.inputs)
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


                var amount = numberarrays[0][0];
                var coins = numberarrays[1];

                Console.WriteLine(numberarrays);

                var result = Change(amount, coins);
                resultSet.Add(result);
            }

            var newObj = new ResponseCoinChange();
            newObj.answer = resultSet;
            return newObj;


        }
        public long Change(int amount, List<int> coins)
        {
            if (amount == 0)
                return 1;
            if (coins.Count() == 0)
                return 0;

            int m = coins.Count();
            int n = amount;
            long[,] dp = new long[m + 1, n + 1];

            dp[0, 0] = 1;

            for (int i = 1; i <= n; i++)
            {
                dp[0, i] = 0;
            }

            for (int i = 1; i <= m; i++)
            {
                dp[i, 0] = 1;
            }

            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (j - coins[i - 1] >= 0)
                        dp[i, j] = dp[i, j - coins[i - 1]] + dp[i - 1, j];
                    else
                        dp[i, j] = dp[i - 1, j];
                }
            }

            return dp[m, n];
        }



        public ResponseDataEncryption dataEncrypt(RequestDataEncryption requestDataEncryption)
        {
            List<string> resultSet = new List<string>();
            foreach (var s in requestDataEncryption.inputs)
            {
                string t = "";
                int count = 0;
                int n, m;
                for (int i = 0; i < s.Length; i++)
                {
                    if (char.IsLetter(s[i]))
                    {
                        t += s[i];
                        count++;
                    }
                }

                Console.WriteLine(t);

                int ceiloflength = (int)Math.Ceiling(Math.Sqrt(t.Length));
                int flooroflength = (int)Math.Floor(Math.Sqrt(t.Length));

                if (ceiloflength * flooroflength == t.Length)
                {
                    n = flooroflength;
                    m = ceiloflength;
                }
                else
                {
                    n = m = ceiloflength;
                }

                int x = 0;
                char[,] arr = new char[n, m];

                //Console.WriteLine();
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < m; j++)
                    {
                        if (x == t.Length)
                        {
                            arr[i, j] = ' ';
                        }
                        else
                        {
                            arr[i, j] = t[x++];
                        }
                    }
                }

                //for (int i = 0; i < n; i++)
                //{
                //    for (int j = 0; j < m; j++)
                //    {
                //        Console.Write(arr[i, j]);
                //    }
                //    Console.WriteLine();
                //}

                //Console.WriteLine();
                string result = "";
                for (int i = 0; i < m; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (arr[j, i] != ' ')
                        {
                            result += arr[j, i];
                        }
                    }
                    result += ' ';
                }

                result = result.TrimEnd();
                resultSet.Add(result);
            }

            var newObj = new ResponseDataEncryption();
            newObj.answer = resultSet;
            return newObj;
        }

        //Question Risk mitigation 


        public responsePortfolio calculateRisk(RequestPortfolio requestPortfolio)
        {

            List<int> resultSet = new List<int>();

            foreach (List<string> list in requestPortfolio.inputs)
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

                var n = numberarrays[0][0];
                var m = numberarrays[0][1];
                var costs = numberarrays[1];

                Console.WriteLine(numberarrays);
                var result = MaxProfit(n, costs.ToArray());
                resultSet.Add(result);
            }

            var newObj = new responsePortfolio();
            newObj.answer = resultSet;
            return newObj;


        }

        public int MaxProfit(int k, int[] prices)
        {
            int[] buy = Enumerable.Repeat(int.MaxValue, k + 1).ToArray();
            int[] sell = new int[k + 1];

            foreach (int p in prices)
            {
                for (int i = 1; i <= k; i++)
                {
                    buy[i] = Math.Min(buy[i], p - sell[i - 1]);
                    sell[i] = Math.Max(sell[i], p - buy[i]);
                }
            }

            return sell.Last();
        }


        //time intervals
        public ResponseTimeIntervals timeIntervals(RequestTimeIntervals requestTimeIntervals)
        {
            List<List<string>> resultSet = new List<List<string>>();
            foreach (List<string> input in requestTimeIntervals.inputs)
            {
                List<string> names = new List<string>();
                var numberarrays = new List<List<int>>();
                var num = int.Parse(input[0]);
                HashSet<int> intervalsSet = new HashSet<int>();
                names = input[1].Split(" ").ToList();
                for (int i = 2; i < num + 2; i++)
                {
                    var numbersList = new List<int>();
                    string[] numberStrings = input[i].Split(' ');

                    int[] numbersArray = Array.ConvertAll(numberStrings, int.Parse);
                    numbersList = numberStrings.Select(int.Parse).ToList();
                    foreach (int number in numbersList)
                    {
                        intervalsSet.Add(number);
                    }
                    numberarrays.Add(numbersList);
                }
                var intervals = intervalsSet.ToList();
                intervals.Sort();
                var result = getOverlappingIntervals(intervals, names, numberarrays);

                resultSet.Add(result);
            }
            var newObj = new ResponseTimeIntervals();
            newObj.answer = resultSet;
            return newObj;
        }
        public List<string> getOverlappingIntervals(List<int> intervals, List<string> names, List<List<int>> timeShifts)
        {
            Dictionary<int, List<string>> mapPeopleToIntervals = new Dictionary<int, List<string>>();
            List<string> resultSet = new List<string>();
            resultSet.Add((intervals.Count() - 1).ToString());
            for (int j = 0; j < intervals.Count() - 1; j++)
            {

                mapPeopleToIntervals[j] = new List<string>();

            }
            for (int i = 0; i < timeShifts.Count(); i++)
            {
                var n1 = timeShifts[i][0];
                var n2 = timeShifts[i][1];
                var index1 = intervals.IndexOf(n1);
                var index2 = intervals.IndexOf(n2);


                for (int j = index1; j < index2; j++)
                {

                    mapPeopleToIntervals[j].Add(names[i]);

                }
            }
            int index = 0;
            foreach (var entry in mapPeopleToIntervals)
            {
                entry.Value.Sort();
            }

            foreach (var entry in mapPeopleToIntervals)
            {
                string result = "";
                //result += (intervals.Count() - 1).ToString() + " ";
                result += intervals[index].ToString() + " " + intervals[++index].ToString() + " " + entry.Value.Count() + " " + string.Join(" ", entry.Value);
                resultSet.Add(result);
            }
            return resultSet;
        }

        //Profit maximization 



        public FileRearrangeResponse ProfMax(RequestMaxProf requestMaxProf)
        {
            List<int> resultSet = new List<int>();
            foreach (string input in requestMaxProf.inputs)
            {
                int[] integerList = input.Split(' ').Select(int.Parse).ToArray();
                int[] newArray = new int[integerList.Length - 1];
                Array.Copy(integerList, 1, newArray, 0, newArray.Length);

                var result = MaxProfit(newArray);


                //List<string> names = new List<string>();
                //var numberarrays = new List<List<int>>();
                //var num = int.Parse(input[0]);
                //HashSet<int> intervalsSet = new HashSet<int>();
                //names = input[1].Split(" ").ToList();
                //for (int i = 2; i < num + 2; i++)
                //{
                //    var numbersList = new List<int>();
                //    string[] numberStrings = input[i].Split(' ');

                //    int[] numbersArray = Array.ConvertAll(numberStrings, int.Parse);
                //    numbersList = numberStrings.Select(int.Parse).ToList();
                //    foreach (int number in numbersList)
                //    {
                //        intervalsSet.Add(number);
                //    }
                //    numberarrays.Add(numbersList);
                //}
                //var intervals = intervalsSet.ToList();
                //intervals.Sort();
                //var result = getOverlappingIntervals(intervals, names, numberarrays);

                resultSet.Add(result);
            }
            var newObj = new FileRearrangeResponse();
            newObj.answer = resultSet;
            return newObj;
        }

        public int MaxProfit(int[] prices)
        {
            if (prices == null || prices.Length == 0)
                return 0;

            int[] res = new int[prices.Length];
            int diff = -prices[0];

            for (int i = 1; i < prices.Length; i++)
            {
                res[i] = Math.Max(res[i - 1], prices[i] + diff);
                diff = Math.Max(diff, -prices[i]);
            }

            return res[prices.Length - 1];
        }


    }
}



