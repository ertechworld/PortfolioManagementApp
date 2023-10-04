using Application.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace Application.Controllers
{
    public class InitiativeController : Controller
    {
        public IActionResult Index()
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

            return View("Initiative",model);
        }
        public IActionResult Detail(int id)
        {
            String connectionString = "Data Source=localhost;Initial Catalog=PortfolioManagement;Integrated Security=true";
            String sql = "SELECT * FROM Initiative WHERE id = "+id;

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

            return View(model.First());
        }
        [HttpPost]
        public IActionResult Update(InitiativeModel initiative)
        {
            string connectionString = "Data Source=localhost;Initial Catalog=PortfolioManagement;Integrated Security=true";
            string sql = "UPDATE Initiative SET FixVersionName = '"+initiative.FixVersionName+ "', PriorityOrder = '"+initiative.PriorityOrder+"'  WHERE id = " + initiative.ID;

            SqlConnection conn = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand(sql, conn);
            
            try
            {
                using (conn)
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }               
            }
            catch (Exception)
            {
                
            }
            finally
            {
                conn.Close();
            }                        
            return View("Detail", initiative);
        }


    }
}
