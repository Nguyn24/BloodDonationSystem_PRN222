using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO
{
    public class RequestStatusDto
    {
        public List<StatusCountDto> Group { get; set; } = new();
        public int Count { get; set; }
    }
}
