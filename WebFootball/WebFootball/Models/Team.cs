using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebFootball.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        [StringLength(35)]
        public String Name { get; set; }        
        public int Victories { get; set; }
     
    }
    



}