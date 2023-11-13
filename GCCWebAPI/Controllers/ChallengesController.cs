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

    }
}
