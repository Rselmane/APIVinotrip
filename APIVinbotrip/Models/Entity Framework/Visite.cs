﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APIVinotrip.Models.Entity_Framework
{
    [Table("VISITE")]
    public partial class Visite
    {
        [Key]
        [Column("idVisite")]
        public int IdVisite { get; set; }

        [Column("idPartenaire")]
        public int? IdPartenaire { get; set; }

        [Column("descriptionVisite")]
        [StringLength(4096)]
        public string? DescriptionVisite { get; set; }

        [Column("photoVisite")]
        [StringLength(512)]
        public string? PhotoVisite { get; set; }

        [Column("lienVisite")]
        [StringLength(512)]
        public string? LienVisite { get; set; }

        // Navigation properties
        [ForeignKey(nameof(IdPartenaire))]
        [InverseProperty(nameof(Partenaire.Visites))]
        public virtual Partenaire? Partenaire { get; set; }

        // Collection navigation properties
        [InverseProperty(nameof(Appartient.Visite))]
        public virtual ICollection<Appartient> AppartientCollection { get; set; } = new List<Appartient>();

        [ForeignKey(nameof(IdPartenaire))]
        [InverseProperty(nameof(Cave.Visites))]
        public virtual Cave? Cave { get; set; }


    }
}
