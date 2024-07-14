using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebClient.DTO.GradeType
{
    public class CreateGradeTypeDTO
    {
        [RegularExpression(@"\S+", ErrorMessage = "GradeType Name cannot be empty or whitespace.")]
        public string Name { get; set; } = null!;
        public int? ComparasionTypeId { get; set; }
        public int? GradeValue { get; set; }
        public int GradedByRole { get; set; }

    }
}
