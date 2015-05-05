using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebsiteThuChi.Models
{
    public class OutcomeDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Date)]
        [Remote("IsDuplicatedOutcome", "Outcome", ErrorMessage = "Duplicated Outcome")]
        public DateTime? OutcomeDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [Required(ErrorMessage = "*")]
        public string OutcomeName { get; set; }      
        public IList<OutcomeLineUpDto> LineUp { get; set; }
       


        public OutcomeDto()
        {
            LineUp = new List<OutcomeLineUpDto>(); 
        }
        public OutcomeDto(int ID)
        {
            
        }
        public OutcomeDto(List<OutcomeLineUp> lineUp)
        {
            LineUp = new List<OutcomeLineUpDto>(); 
            foreach (var item in lineUp)
            {
                OutcomeLineUpDto data = new OutcomeLineUpDto();
                data.Id = item.Id;
                data.Asset = item.Asset;
                data.Customer = item.Customer;
                data.OutcomeId = item.Id;
                data.Notes = item.Notes;
                LineUp.Add(data);
            }

            Id = lineUp.FirstOrDefault().Outcome.Id;
            OutcomeName = lineUp.FirstOrDefault().Outcome.OutcomeName;
            OutcomeDate = lineUp.FirstOrDefault().Outcome.OutcomeDate;

        }
    }
    public class OutcomeLineUpDto
    {
        public int Id { get; set; }
        public decimal? Asset { get; set; }
       
        public string Notes { get; set; }
        public int? OutcomeId { get; set; }
        public Customer Customer { get; set; }
        
    }
}