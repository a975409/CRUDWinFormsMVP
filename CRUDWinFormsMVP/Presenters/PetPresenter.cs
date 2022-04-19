using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CRUDWinFormsMVP.Models;
using CRUDWinFormsMVP.Views;

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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        private void View_DeleteEvent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void View_CancelEvent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void View_AddNewEvent(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
