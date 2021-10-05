using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NewWorldData.Models
{
    public class WebsiteNewsModel
    {
        [Display(Name = "ID")]
        [Key]
        public int id { get; set; }
        [Display(Name = "Title")]
        public string title { get; set; }
        [Display(Name = "Content")]
        public string content { get; set; }
        public string source_url { get; set; }
        public string posted_at { get; set; }
        public string developer_name { get; set; }
    }
}
