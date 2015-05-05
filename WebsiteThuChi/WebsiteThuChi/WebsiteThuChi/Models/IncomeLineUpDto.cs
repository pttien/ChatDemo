using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebsiteThuChi.Models;
namespace WebsiteThuChi.Models
{
    public class IncomeLineUpDto
    {

        public int Id { get; set; }
        public int? CustomerId { get; set; }
        public decimal? Asset { get; set; }
        public string Notes { get; set; }
        public int? IncomeId { get; set; }
    }
    public class IncomeDto
    {
        public IncomeDto()
        {
            LineUp = new List<IncomeLineUpDto>(); 
        }

        public IncomeDto(List<IncomeLineUp> lineUp)
        {
            LineUp = new List<IncomeLineUpDto>(); 
            foreach (var item in lineUp)
            {
                IncomeLineUpDto data = new IncomeLineUpDto();
                data.Id = item.Id;
                data.Asset = item.Asset;
                data.CustomerId = item.Customer.Id;
                data.IncomeId = item.Income.Id;
                data.Notes = item.Notes;
                LineUp.Add(data);
            }

            Id = lineUp.FirstOrDefault().Income.Id;
            Name = lineUp.FirstOrDefault().Income.IncomeName;
            IncomeDate = lineUp.FirstOrDefault().Income.IncomeDate;

        }
        public IList<IncomeLineUpDto> LineUp { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime? IncomeDate { get; set; }
    }
}