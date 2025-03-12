﻿using APIVinbotrip.Models.Entity_Framework;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APIVinotrip.Models.Entity_Framework
{
    [Table("ROUTE_DES_VINS")]
    public partial class RouteDesVins
    {
        [Key]
        [Column("idRoute")]
        public int IdRoute { get; set; }

        [Column("libRoute")]
        [StringLength(50)]
        public string? LibRoute { get; set; }

        [Column("descriptionRoute")]
        [StringLength(2048)]
        public string? DescriptionRoute { get; set; }

        [Column("photoRoute")]
        [StringLength(512)]
        public string? PhotoRoute { get; set; }

        // Collection navigation properties
        [InverseProperty(nameof(SeLocalise.Route))]
        public virtual ICollection<SeLocalise> SaLocalites { get; set; } = new List<SeLocalise>();
    }
}
