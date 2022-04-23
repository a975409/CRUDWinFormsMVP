﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRUDWinFormsMVP.Models;
using CRUDWinFormsMVP.Views;
using CRUDWinFormsMVP.Presenters.Common;

namespace CRUDWinFormsMVP.Presenters
{
    public class PetPresenter
    {
        private IPetView view;
        private IPetRespository respository;
        private BindingSource petBindingSource;
        private IEnumerable<PetModel> petList;

        public PetPresenter(IPetView view, IPetRespository respository)
        {
            this.petBindingSource = new BindingSource();
            this.view = view;
            this.respository = respository;
            this.view.AddNewEvent += View_AddNewEvent;
            this.view.CancelEvent += View_CancelEvent;
            this.view.DeleteEvent += View_DeleteEvent;
            this.view.SaveEvent += View_SaveEvent;
            this.view.SearchEvent += View_SearchEvent;
            this.view.EditEvent += View_EditEvent;

            this.view.SetPetListBindingSource(petBindingSource);


            LoadAllPetList();

            this.view.Show();
        }

        private void LoadAllPetList()
        {
            petList = respository.GetAll();
            petBindingSource.DataSource = petList;
        }

        private void View_EditEvent(object sender, EventArgs e)
        {
            var pet = (PetModel)petBindingSource.Current;
            view.PetId = pet.Id.ToString();
            view.PetName = pet.Name;
            view.PetType = pet.Type;
            view.PetColor = pet.Color;
            view.IsEdit = true;
        }

        private void View_SearchEvent(object sender, EventArgs e)
        {
            bool emptyValue = string.IsNullOrWhiteSpace(this.view.SearchValue);

            if (!emptyValue)
                this.petList = this.respository.GetByValue(this.view.SearchValue);
            else
                this.petList = this.respository.GetAll();

            this.petBindingSource.DataSource = this.petList;
        }

        private void View_SaveEvent(object sender, EventArgs e)
        {
            var model = new PetModel();
            model.Id = Convert.ToInt32(view.PetId);
            model.Name = view.PetName;
            model.Type = view.PetType;
            model.Color = view.PetColor;

            try
            {
                new ModelDataValidation().Validate(model);
                if (view.IsEdit)
                {
                    respository.Edit(model);
                    view.Message = "Pet edited successfuly";
                }
                else
                {
                    respository.Add(model);
                    view.Message = "Pet added successfuly";
                }
                view.IsSuccess = true;
                LoadAllPetList();
                CleanviewFields();
            }
            catch(Exception ex)
            {
                view.IsSuccess = false;
                view.Message = ex.Message;
            }

        }

        private void CleanviewFields()
        {
            view.PetId ="0";
            view.PetName = "";
            view.PetType = "";
            view.PetColor = "";
            view.IsEdit = false;
        }

        private void View_DeleteEvent(object sender, EventArgs e)
        {
            try
            {
                var pet = (PetModel)petBindingSource.Current;
                respository.Delete(pet.Id);
                view.IsSuccess = true;
                view.Message = "Pet deleted successfully";
                LoadAllPetList();
            }
            catch (Exception ex)
            {
                view.IsSuccess = false;
                view.Message = "An error ocurred,could not delete pet";
            }
        }

        private void View_CancelEvent(object sender, EventArgs e)
        {
            CleanviewFields();
        }

        private void View_AddNewEvent(object sender, EventArgs e)
        {
            view.IsEdit = false;
        }
    }
}
