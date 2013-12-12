using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Registration.Models
{
    public class Kompressor
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please, fill in this area")]
        public string PressIn { get; set; }
        [Required(ErrorMessage = "Please, fill in this area")]
        public string PressOut { get; set; }
        [Required(ErrorMessage = "Please, fill in this area")]
        public string Performance { get; set; }
        [Required(ErrorMessage = "Please, fill in this area")]
        public string Drive { get; set; }


        public string Power { get; set; }
        public string DegreesOfPressure { get; set;}
        public string NumberOfCylinders { get; set; }
        public string Bore { get; set; }
        public string LengthOfStroke { get; set; }
        public string SpeedOfRotation { get; set; }
    }

}
