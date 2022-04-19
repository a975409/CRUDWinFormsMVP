using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CRUDWinFormsMVP.Views
{
    public interface IPetView
    {
        string PetId { get; set; }
        string PetName { get; set; }
        string PetType { get; set; }
        string PetColor { get; set; }

        string SearchValue { get; set; }
        bool IsEdit { get; set; }
        bool IsSuccess { get; set; }
        string Message { get; set; }

        event EventHandler SearchEvent;
        event EventHandler AddNewEvent;
        event EventHandler EditEvent;
        event EventHandler DeleteEvent;
        event EventHandler SaveEvent;
        event EventHandler CancelEvent;

        void SetPetListBindingSource(BindingSource petList);
        void Show();
    }
}
