using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace InAndOut.Models.ViewModels
{
    public class ExpensesVM
    {
        public Expenses Expenses { get; set; }
        public IEnumerable<SelectListItem> TypeDropDown { get; set; }

    }
}
