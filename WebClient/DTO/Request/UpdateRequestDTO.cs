using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClient.DTO.Request
{
    public class UpdateRequestDTO
    {
        public int requestId { get; set; }
        public string? responeContent { get; set; }
        public int  newStatusId { get; set; }
        public int? newGradeValue {  get; set; }    
    }
}
