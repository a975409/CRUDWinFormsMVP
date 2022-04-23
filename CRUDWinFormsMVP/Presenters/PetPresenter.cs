using CRUDWinFormsMVP.Enums;
using CRUDWinFormsMVP.Models;
using CRUDWinFormsMVP.Presenters.Common;
using CRUDWinFormsMVP.Respositories;
using CRUDWinFormsMVP.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDWinFormsMVP.Presenters
{
    public class PetPresenter
    {
        private IPetView petView;
        private IPetRespository petRespository;
        private IEnumerable<PetModel> petList;
        private BindingSource bindingSource;

        public PetPresenter(IPetView petView, IPetRespository petRespository)
        {
            this.petView = petView;
            this.petRespository = petRespository;
            this.petView.State = CRUDState.Read;
            this.petView.AddPetEvent += PetView_AddPetEvent;
            this.petView.DeletePetEvent += PetView_DeletePetEvent;
            this.petView.EditPetEvent += PetView_EditPetEvent;
            this.petView.SaveEvent += PetView_SaveEvent;
            this.petView.SearchPetEvent += PetView_SearchPetEvent;

            petList = petRespository.GetAllData();
            bindingSource = new BindingSource();
            bindingSource.DataSource = petList;
            this.petView.SetDataGridViewDataSource(bindingSource);
        }

        private void PetView_SearchPetEvent(object sender, EventArgs e)
        {
            petView.State = CRUDState.Read;
            if (string.IsNullOrEmpty(petView.SearchValue))
                petList = petRespository.GetAllData();
            else
                petList = petRespository.GetDataBySearchValue(petView.SearchValue);

            bindingSource.DataSource = petList;
        }

        private void PetView_SaveEvent(object sender, EventArgs e)
        {
            PetModel pet = bindingSource.Current as PetModel;

            try
            {
                ModelDataValidation.Validate(pet);
                if (petView.State == CRUDState.Create)
                {
                    petRespository.Add(pet);
                    petView.Message = "新增資料成功";
                }
                else
                {
                    petRespository.Edit(pet);
                    petView.Message = "更新資料成功";
                }
                petView.IsSuccessfulle = true;
            }
            catch (Exception ex)
            {
                petView.IsSuccessfulle = false;
                petView.Message = ex.Message;
            }

            petView.State = CRUDState.Read;
            petList = petRespository.GetAllData();
            bindingSource.DataSource = petList;
        }

        private void PetView_EditPetEvent(object sender, EventArgs e)
        {
            petView.State = CRUDState.Update;
        }

        private void PetView_DeletePetEvent(object sender, EventArgs e)
        {
            try
            {
                petRespository.Delete(petView.Pet_Id);
            }
            catch (Exception ex)
            {
                petView.IsSuccessfulle = false;
                petView.Message = ex.Message;
            }

            petList = petRespository.GetAllData();
            bindingSource.DataSource = petList;
        }

        private void PetView_AddPetEvent(object sender, EventArgs e)
        {
            petView.State = CRUDState.Create;
            bindingSource.AddNew();
            bindingSource.MoveLast();
        }
    }
}
