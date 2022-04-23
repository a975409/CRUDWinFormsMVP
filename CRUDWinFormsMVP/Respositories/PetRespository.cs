using CRUDWinFormsMVP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CRUDWinFormsMVP.Respositories
{
    public class PetRespository : BaseRespository, IPetRespository
    {
        public PetRespository(string connectionString) 
        {
            this.connectionString = connectionString;
        }

        public void Add(PetModel petModel)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                string sql = "insert into Pet values (@name,@type,@colour)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@name", petModel.Name);
                    cmd.Parameters.AddWithValue("@type", petModel.Type);
                    cmd.Parameters.AddWithValue("@colour", petModel.Color);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                string sql = "delete from Pet where Pet_Id=@id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Edit(PetModel petModel)
        {
            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                string sql = "update Pet " +
                    "set Pet_Name=@name,Pet_Type=@type,Pet_Colour=@colour " +
                    "where Pet_Id=@id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@id", petModel.Id);
                    cmd.Parameters.AddWithValue("@name", petModel.Name);
                    cmd.Parameters.AddWithValue("@type", petModel.Type);
                    cmd.Parameters.AddWithValue("@colour", petModel.Color);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<PetModel> GetAll()
        {
            var petList = new List<PetModel>();

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                string sql = "select * from Pet order by Pet_Id desc";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PetModel pet = new PetModel();
                            pet.Id = (int)reader[0];
                            pet.Name = reader[1].ToString();
                            pet.Type = reader[2].ToString();
                            pet.Color = reader[3].ToString();
                            petList.Add(pet);
                        }
                    }
                }
            }

            return petList;
        }

        public IEnumerable<PetModel> GetByValue(string searchValue)
        {
            int petId = -1;
            string petName = string.Empty;

            if (!int.TryParse(searchValue, out petId))
                petName = searchValue;

            var petList = new List<PetModel>();

            using (SqlConnection conn = new SqlConnection(this.connectionString))
            {
                string sql = "select * from Pet " +
                    "where Pet_Id=@Pet_Id or Pet_Name like @Pet_Name " +
                    "order by Pet_Id desc";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Pet_Id", petId);
                    cmd.Parameters.AddWithValue("@Pet_Name", petName+"%");

                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PetModel pet = new PetModel();
                            pet.Id = (int)reader[0];
                            pet.Name = reader[1].ToString();
                            pet.Type = reader[2].ToString();
                            pet.Color = reader[3].ToString();
                            petList.Add(pet);
                        }
                    }
                }
            }

            return petList;
        }
    }
}
