using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CatRenta.Domain
{
    [Table ("tblCatPrices")]
    public class AppCatPrice
    {
        [Key]
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime DateCreate { get; set; }
        [ForeignKey("Cat")]
        public int CatId { get; set; }
        public virtual AppCat Cat { get; set; }
    }
}
