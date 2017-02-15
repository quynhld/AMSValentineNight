using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace KNNEW.Models
{
    public class HomeModel
    {
    }
    public class TopMenu
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MenuSeq { get; set; }
        public string MenuName { get; set; }
    }
}