using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Space101.Dtos
{
    public class RaceClassificationsDto
    {
        public int RaceClassificationID { get; set; }
        public string Name { get; set; }
    }
}