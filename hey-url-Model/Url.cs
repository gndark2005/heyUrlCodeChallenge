using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hey_url_Model
{
    public class Url
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UrlId { get; set; }

        [Required]
        [MaxLength(5)]
        public string ShortUrl { get; set; }

        [Required(AllowEmptyStrings =false)]
        [Url(ErrorMessage ="URL is not valid")]
        public string OrinalUrl { get; set; }

        [Required]
        public int Count { get; set; }

        [Required]
        [DataType(dataType:DataType.DateTime)]
        public DateTime CretionDate { get; set; }


    }
}
