namespace Konwerter.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Konwerter_stat
    {
        public int Id { get; set; }

        public DateTime? DateTime { get; set; }

        [Required]
        [StringLength(50)]
        public string UnitFrom { get; set; }

        [Required]
        [StringLength(50)]
        public string UnitTo { get; set; }

        public double? RawValue { get; set; }

        public double? ConvertedValue { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }
    }
}
