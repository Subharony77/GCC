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
            if (j != arr3.Count() - 1)
            {
                while (sum1 <= maxsum && j <= arr3.Count() - 1)
                {
                    sum1 += arr3[j++];
                    result++;
                }
            }


            return result;


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
                var result = MaxProfit(0, 1, n, costs, m);
                resultSet.Add(result);
            }

            var newObj = new responsePortfolio();
            newObj.answer = resultSet;
            return newObj;


        }

        public int MaxProfit(int ind, int start, int cap, List<int> costs, int n)
        {
            if (ind == n || cap == 0)
                return 0;

            if (start == 1)
            {
                return Math.Max(-costs[ind] + MaxProfit(ind + 1, 0, cap, costs, n),
                                 0 + MaxProfit(ind + 1, 1, cap, costs, n));
            }

            return Math.Max(costs[ind] + MaxProfit(ind + 1, 1, cap - 1, costs, n),
                             0 + MaxProfit(ind + 1, 0, cap, costs, n));
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



