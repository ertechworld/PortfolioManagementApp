using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetMenus()
        {
            var menus = new List<string>()
            {
                "App Engineering/Sustaining",
                "Progress & Forecasting Tool",
                "Cook Learn - Q4 2020 Quarterly Release - (TID:N/A)"
            };
            return Ok(menus);
        }
    }
}
