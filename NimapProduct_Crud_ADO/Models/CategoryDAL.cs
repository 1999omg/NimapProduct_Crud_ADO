using Microsoft.Data.SqlClient;

namespace NimapProduct_Crud_ADO.Models
{
    public class CategoryDAL
    {

        SqlConnection con;
        SqlCommand cmd;
        SqlDataReader reader;
        private readonly IConfiguration configuration;
        public CategoryDAL(IConfiguration configuration)
        {
            this.configuration = configuration;
            string constr = configuration.GetConnectionString("defaultConnection");
            con = new SqlConnection(constr);
        }

        public List<Category> CategoryList()
        {
            List<Category> list = new List<Category>();
            string str = "select * from tblCategory";
            cmd = new SqlCommand(str, con);
            con.Open();
            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Category c = new Category();
                    c.CategoryId = Convert.ToInt32(reader["cId"]);
                    c.CategoryName = reader["cName"].ToString();
                    list.Add(c);
                }
            }
            con.Close();
            return list;
        }

        public int AddCat(Category cat)
        {
            string str = "insert into tblCategory values(@cId, @cName)";
            cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@cId", cat.CategoryId);
            cmd.Parameters.AddWithValue("@cName", cat.CategoryName);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        public int UpdateCat(Category cat)
        {
            string str = "update Category set cName = @cName where cId = @cId";
            cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@cId", cat.CategoryId);
            cmd.Parameters.AddWithValue("@cName", cat.CategoryName);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }


        public int DeleteCat(int id)
        {
            string str = "delete from tblCategory where cId = @cId ";
            cmd = new SqlCommand(str, con);
            cmd.Parameters.AddWithValue("@cId", id);
            con.Open();
            int res = cmd.ExecuteNonQuery();
            con.Close();
            return res;
        }

        public Category GetCatById(int id)
        {
            Category c = new Category();
            string query = "select * from tblCategory where cId=@id";
            cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@id", id);
            con.Open();




            reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {

                    c.CategoryId = Convert.ToInt32(reader["cId"]);
                    c.CategoryName = reader["cName"].ToString();
                }
            }
            con.Close();
            return c;

        }
    }

}

