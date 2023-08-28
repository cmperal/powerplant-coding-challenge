using Microsoft.AspNetCore.Mvc;

namespace PowerplantCodingChallenge.Controllers
{
    [ApiController]
    public class PowerPlant : ControllerBase
    {
     
        private readonly ILogger<PowerPlant> _logger;

        public PowerPlant(ILogger<PowerPlant> logger)
        {
            _logger = logger;
        }

        [HttpPost("productionplan")]
        public IActionResult ProductionPlan(PowerPlantRequest request)
        {
            try
            {
                _logger.Log(LogLevel.Information, request.ToString());

                return Ok(Load.Calculate(request));
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex.ToString());

                return StatusCode(500);
      
            }
        }
    }
}