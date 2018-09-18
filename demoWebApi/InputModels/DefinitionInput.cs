using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace demoWebApi.InputModels
{
    public class DefinitionInput
    {
        [Range(1,int.MaxValue)]
        public int? id { get; set; }

        [Required]
        [MinLength(1)]
        public string name { get; set; }
    }
}
