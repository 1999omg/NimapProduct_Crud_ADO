using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using System.Reflection.PortableExecutable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NimapProduct_Crud_ADO.Models

{
    public class ProductDataAcess
    {

        public class ProductDAL
        {
            SqlConnection con;
            SqlCommand cmd;
            SqlDataReader reader;
            private readonly IConfiguration configuration;
            public ProductDAL(IConfiguration configuration)
            {
                this.configuration = configuration;
                string constr = configuration["ConnectionStrings:defaultConnection"];
                con = new SqlConnection(constr);
            }

            public List<Product> ProductList()
            {
                List<Product> list = new List<Product>();
                string str = "select * from tblProduct";
                cmd = new SqlCommand(str, con);
                con.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Product p = new Product();
                        p.ProductId = Convert.ToInt32(reader["pId"]);
                        p.ProductName = reader["pName"].ToString();
                        p.CategoryId = Convert.ToInt32(reader["ccId"]);
                        list.Add(p);

                    }
                }
                con.Close();
                return list;
            }

            public int AddProd(Product p)
            {
                string str = "insert into Product values(@ProductId, @ProductName, @ccId)";
                cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@pId", p.ProductId);
                cmd.Parameters.AddWithValue("@pName", p.ProductName);
                cmd.Parameters.AddWithValue("@ccId", p.CategoryId);
                con.Open();
                int res = cmd.ExecuteNonQuery();
                con.Close();
                return res;
            }

            public int UpdateProd(Product p)
            {
                string str = "update Product set tblProduct pName=@ProductName, cId=@CategoryId where pId=@id";
                cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@id", p.ProductId);
                cmd.Parameters.AddWithValue("@ProductName", p.ProductName);
                cmd.Parameters.AddWithValue("@CategoryId", p.CategoryId);
                con.Open();
                int res = cmd.ExecuteNonQuery();
                con.Close();
                return res;
            }

            public int DeleteProd(int id)
            {
                string str = "delete from tblProduct where pId = @pId ";
                cmd = new SqlCommand(str, con);
                cmd.Parameters.AddWithValue("@pId", id);
                con.Open();
                int res = cmd.ExecuteNonQuery();
                con.Close();
                return res;
            }

            public Product GetProdById(int id)
            {
                Product p = new Product();
                string query = "select * from tblProduct where pId=@id";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        p.ProductId = Convert.ToInt32(reader["pId"]);
                        p.ProductName = reader["pName"].ToString();
                        p.CategoryId = Convert.ToInt32(reader["pId"]);
                    }
                }
                con.Close();
                return p;

            }
        }
    }
}