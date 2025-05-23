﻿
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIVinotrip.Models.EntityFramework
{
    [Table("hebergement")]
    public partial class Hebergement
    {
        [Key]
        [Column("idhebergement")]
        public int IdHebergement { get; set; }

        [Column("idpartenaire")]
        public int IdPartenaire { get; set; }

        [Column("descriptionhebergement")]
        [StringLength(4096)]
        public string? DescriptionHebergement { get; set; }

        [Column("photohebergement")]
        [StringLength(512)]
        public string? PhotoHebergement { get; set; }

        [Column("lienhebergement")]
        [StringLength(512)]
        public string? LienHebergement { get; set; }

        [Column("prixhebergement", TypeName ="NUMERIC(8,2)")]
        public decimal? PrixHebergement { get; set; }

        [ForeignKey(nameof(IdPartenaire))]
        [InverseProperty(nameof(Hotel.HotelHebergements))]
        public virtual Hotel? HebergementHotel { get; set; }

        [InverseProperty(nameof(Etape.Hebergement))]
        public virtual List<Etape>? Etapes { get; set; } = new List<Etape>();

        [InverseProperty(nameof(DescriptionPanier.Hebergement))]
        public virtual List<DescriptionPanier>? DescriptionsPanier { get; set; } = new List<DescriptionPanier>();

        [InverseProperty(nameof(DescriptionCommande.Hebergements))]
        public virtual List<DescriptionCommande>? DescriptionsCommande { get; set; } =  new List<DescriptionCommande>();
    }
}
