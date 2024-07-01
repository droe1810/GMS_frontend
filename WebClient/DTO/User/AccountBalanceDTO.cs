using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClient.DTO.User
{
    public class AccountBalanceDTO
    {
        public int StudentId { get; set; }
        public string? Username { get; set; }
        public string? Action { get; set; }
        public bool? isSuccess { get; set; }
        public int? OldAccountBalance { get; set; }
        public int? NewAccountBalance { get; set; }
    }
}
