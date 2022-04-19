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
            
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Edit(PetModel petModel)
        {
            throw new NotImplementedException();
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
