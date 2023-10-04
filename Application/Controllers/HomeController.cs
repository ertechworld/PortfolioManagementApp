using Application.Configuration;
using Application.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Application.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult RenderMenu()
        {
            String connectionString = "Data Source=localhost;Initial Catalog=PortfolioManagement;Integrated Security=true";
            String sql = "SELECT * FROM Initiative WHERE Initiative.Prediction IN ('DOD 23') ORDER BY Initiative.PriorityOrder";

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);

            var model = new List<InitiativeModel>();
            using (conn)
            {
                conn.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    var initiative = new InitiativeModel();
                    initiative.PriorityOrder = rdr["PriorityOrder"].ToString();
                    initiative.ID = Convert.ToInt32(rdr["ID"]);
                    initiative.FixVersionName = rdr["FixVersionName"].ToString();
                    model.Add(initiative);
                }

            }
            //HttpContext.Session.Set<IEnumerable<InitiativeModel>>("Menu", model);
            return PartialView("_partialMenu", model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}