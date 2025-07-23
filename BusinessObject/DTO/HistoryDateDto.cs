using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class HistoryDateDto
    {
        public List<MonthlyDonationDto> MonthlyDonation { get; set; } = new List<MonthlyDonationDto>();
        public int TotalCount { get; set; } = 0;
    }
}
