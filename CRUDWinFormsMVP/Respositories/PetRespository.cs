using CRUDWinFormsMVP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace CRUDWinFormsMVP.Respositories
{
    public class PetRespository: BaseRespository, IPetRespository
    {
        public PetRespository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Add(PetModel pet)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "insert into Pet (Pet_Name,Pet_Type,Pet_Colour) values (@Pet_Name,@Pet_Type,@Pet_Colour)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.Parameters.Add("@Pet_Name", SqlDbType.NVarChar).Value = pet.Name;
                    cmd.Parameters.Add("@Pet_Type", SqlDbType.NVarChar).Value = pet.Type;
                    cmd.Parameters.Add("@Pet_Colour", SqlDbType.NVarChar).Value = pet.Color;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "delete from Pet where Pet_Id=@Pet_Id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.Parameters.Add("@Pet_Id", SqlDbType.Int).Value = id;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Edit(PetModel pet)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "update Pet set " +
                    "Pet_Name=@Pet_Name,Pet_Type=@Pet_Type,Pet_Colour=@Pet_Colour " +
                    "where Pet_Id=@Pet_Id";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.Parameters.Add("@Pet_Id", SqlDbType.Int).Value = pet.Id;
                    cmd.Parameters.Add("@Pet_Name", SqlDbType.NVarChar).Value = pet.Name;
                    cmd.Parameters.Add("@Pet_Type", SqlDbType.NVarChar).Value = pet.Type;
                    cmd.Parameters.Add("@Pet_Colour", SqlDbType.NVarChar).Value = pet.Color;
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<PetModel> GetAllData()
        {
            List<PetModel> petList = new List<PetModel>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "select * from Pet";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PetModel pet = new PetModel();
                            pet.Id = (int)reader["Pet_Id"];
                            pet.Name = reader["Pet_Name"].ToString();
                            pet.Type = reader["Pet_Type"].ToString();
                            pet.Color = reader["Pet_Colour"].ToString();
                            petList.Add(pet);
                        }
                    }
                }
            }

            return petList;
        }

        public IEnumerable<PetModel> GetDataBySearchValue(string searchValue)
        {
            int searchId = -1;
            int.TryParse(searchValue, out searchId);


            List<PetModel> petList = new List<PetModel>();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string sql = "select * from Pet where Pet_Id=@Pet_Id or Pet_Name like @Pet_Name";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.Parameters.Add("@Pet_Id", SqlDbType.Int).Value = searchId;
                    cmd.Parameters.Add("@Pet_Name", SqlDbType.NVarChar).Value = searchValue + "%";

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            PetModel pet = new PetModel();
                            pet.Id = (int)reader["Pet_Id"];
                            pet.Name = reader["Pet_Name"].ToString();
                            pet.Type = reader["Pet_Type"].ToString();
                            pet.Color = reader["Pet_Colour"].ToString();
                            petList.Add(pet);
                        }
                    }
                }
            }

            return petList;
        }
    }
}
