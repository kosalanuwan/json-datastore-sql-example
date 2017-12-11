using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace SqlJsonSupport.Controllers
{
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        // GET api/products
        [HttpGet]
        public string[] Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/products/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            using (DbConnection db = new SqlConnection("connectionString"))
            {
                using (DbCommand comm = db.CreateCommand())
                {
                    comm.CommandText = $"SELECT * FROM dbo.Products WHERE Id = {id}";
                    comm.CommandType = System.Data.CommandType.Text;
                    comm.Connection.Open();
                    using (DbDataReader reader = comm.ExecuteReader())
                    {
                        int chargesOrdinal = reader.GetOrdinal("Charges"),
                            docsOrdinal = reader.GetOrdinal("Documents"),
                            featOrdinal = reader.GetOrdinal("Features");

                        if (reader.Read())
                        {
                            string charges = reader.GetFieldValue<string>(chargesOrdinal),
                                documents = reader.GetFieldValue<string>(docsOrdinal),
                                features = reader.GetFieldValue<string>(featOrdinal);

                            return Ok(new
                            {
                                Id = id,
                                Charges = JsonConvert.DeserializeObject<Dictionary<string, string>>(charges),
                                Documents = JsonConvert.DeserializeObject<string[]>(documents),
                                Features = JsonConvert.DeserializeObject<string[]>(features)
                            });
                        }
                        return NotFound();
                    }
                }
            }
        }

        // POST api/products
        [HttpPost]
        public IActionResult Post([FromBody]dynamic value)
        {
            using (DbConnection db = new SqlConnection("connectionString"))
            {
                using (DbCommand comm = db.CreateCommand())
                {
                     string features = JsonConvert.SerializeObject(new string[] 
                    {
                        "Airmiles", "Life cover", "Medical cover"
                    });
                    string charges = JsonConvert.SerializeObject(new Dictionary<string, string>
                    {
                        { "Late payment fees", "3987 LKR" },
                        { "Some other fees", "6% or 3999 LKR" }
                    });
                    string documents = JsonConvert.SerializeObject(new string[] 
                    {
                        "Copy of NIC", "Proof of residence"
                    });

                    comm.CommandText = "dbo.UpdateProduct";
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    comm.Parameters.AddRange(new SqlParameter[]
                    {
                        new SqlParameter("Charges", charges),
                        new SqlParameter("Features", features),
                        new SqlParameter("Documents", documents),
                        new SqlParameter("Id", 100103)
                    });

                    comm.Connection.Open();
                    comm.ExecuteNonQuery();
                }
            }
            return Ok();
        }

        // PUT api/products/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/products/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
