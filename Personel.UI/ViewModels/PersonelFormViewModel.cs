using Personel.UI.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Personel.UI.ViewModels
{
    public class PersonelFormViewModel
    {
        public IEnumerable<Departman> Departmanlar { get; set; }
        public Personel.UI.Models.EntityFramework.Personel Personel { get; set; }
    }
}