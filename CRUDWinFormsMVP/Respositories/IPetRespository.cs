using CRUDWinFormsMVP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUDWinFormsMVP.Respositories
{
    public interface IPetRespository
    {
        void Add(PetModel pet);
        void Edit(PetModel pet);
        void Delete(int id);
        IEnumerable<PetModel> GetAllData();
        IEnumerable<PetModel> GetDataBySearchValue(string searchValue);
    }
}
