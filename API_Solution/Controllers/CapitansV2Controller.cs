using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Solution.Controllers
{
    [ApiVersion("2.0")]
    [Route("api/capitans")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "v2")]

    public class CapitansV2Controller : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        public CapitansV2Controller(IRepositoryManager repository)
        {
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> GetCapitans()
        {
            var capitans = await _repository.Capitan.GetAllCapitansAsync(trackChanges: false);
            return Ok(capitans);
        }
    }
}
