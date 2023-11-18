using GCCWebAPI.ProcessorForAllQuestions;
using GCCWebAPI.RequestObjects;
using GCCWebAPI.ResponseObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace GCCWebAPI.Controllers
{
    [Route("/")]
    [ApiController]
    public class ChallengesController : ControllerBase
    {
        public readonly Processor ProcessorForAllQuestions;
        public ChallengesController() {
            ProcessorForAllQuestions = new Processor();

        }

        [HttpPost]
        [Route("file-reorganization")]
        public FileRearrangeResponse FileRearrange([FromBody] RequestFileString requestFileString)
        {
            //RequestFileString FileString = JsonSerializer.Deserialize<RequestFileString>(requestFileString);
            var response = ProcessorForAllQuestions.GetFileRearrangeOutput(requestFileString);

            return response;
        }

        [HttpPost]
        [Route("portfolio-operations")]
        public responsePortfolio PortfolioAccount([FromBody] RequestPortfolio requestPortfolio)
        {
            var response = ProcessorForAllQuestions.portfolioAcc(requestPortfolio);

            return response;
        }

        [HttpPost]
        [Route("coin-change")]
        public ResponseCoinChange CoinChange([FromBody] RequestPortfolio requestPortfolio)
        {
            var response = ProcessorForAllQuestions.caluculateCoinChange(requestPortfolio);

            return response;
        }

        [HttpPost]
        [Route("data-encryption")]
        public ResponseDataEncryption DataEncryption([FromBody] RequestDataEncryption requestDataEncryption)
        {
            var response = ProcessorForAllQuestions.dataEncrypt(requestDataEncryption);

            return response;
        }

        [HttpPost]
        [Route("risk-mitigation")]
        public responsePortfolio RiskManagement([FromBody] RequestPortfolio requestinputsRisk)
        {
            var response = ProcessorForAllQuestions.calculateRisk(requestinputsRisk);

            return response;
        }

        [HttpPost]
        [Route("time-intervals")]
        public ResponseTimeIntervals TimeIntervals([FromBody] RequestTimeIntervals requestTimeIntervals)
        {
            var response = ProcessorForAllQuestions.timeIntervals(requestTimeIntervals);
            return response;
        }

        [HttpPost]
        [Route("profit-maximization")]
        public FileRearrangeResponse ProfMax([FromBody] RequestMaxProf requestMaxProf)
        {
            var response = ProcessorForAllQuestions.ProfMax(requestMaxProf);
            return response;
        }


    }
}
