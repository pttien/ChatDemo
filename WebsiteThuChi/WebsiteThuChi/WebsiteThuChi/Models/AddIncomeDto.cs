using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace WebsiteThuChi.Models
{
    public class AddIncomeDto
    {
        public AddIncomeDto()
        {
            LineUp = new List<IncomeLineUp>();
        }
        public int Id { get; set; }
        [Required(ErrorMessage="*")]
        public string IncomeName { get; set; }
        [Required(ErrorMessage = "*")]
        [Remote("IsDuplicatedIncome","Income",ErrorMessage="Duplicated Income")]
        public string InputDate { get; set; }

        public IList<IncomeLineUp> LineUp { get; set; }
    }
   
}