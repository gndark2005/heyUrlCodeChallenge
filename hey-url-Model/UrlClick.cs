using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using hey_url_Model;

namespace hey_url_Model
{
    public class UrlClick
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UrlClickId { get; set; }

        [Required]
        public int UrlId { get; set; }

        [Required]
        [DataType(dataType: DataType.DateTime)]
        public DateTime ClickDateTime { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Browser { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Platform { get; set; }

        [ForeignKey("UrlId")]
        public virtual Url Url { get; set; }

    }
}
