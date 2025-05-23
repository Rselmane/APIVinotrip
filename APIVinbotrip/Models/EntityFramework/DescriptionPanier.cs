
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Data;
using APIVinotrip.Models.EntityFramework;

namespace APIVinotrip.Models.EntityFramework
{
	[Table("descriptionpanier")]
	public partial class DescriptionPanier
	{
		[Key]
		[Column("iddescriptionpanier")]
		public int IdDescriptionPanier { get; set; }

        [Column("idsejour")]
        public int? IdSejour { get; set; }

        [Column("idpanier")]
        public int? IdPanier { get; set; }

        [Column("idhebergement")]
        public int? IdHebergement { get; set; }

        [Column("quantite")]
		public int? Quantite { get; set; }

		[Column("datedebut", TypeName = "date")]
		public DateTime? DateDebut { get; set; }

		[Column("datefin", TypeName = "date")]
		public DateTime? DateFin { get; set; }

		[Column("nbadultes")]
		public int? NbAdultes { get; set; }

		[Column("nbenfants")]
		public int? NbEnfants { get; set; }

		[Column("nbchambressimple")]
		public int? NbChambresSimple { get; set; }

		[Column("nbchambresdouble")]
		public int? NbChambresDouble { get; set; }

		[Column("nbchambrestriple")]
		public int? NbChambresTriple { get; set; }

		[Column("offrir")]
		public bool? Offrir { get; set; }

		[Column("ecoffret")]
		public bool? ECoffret { get; set; }

		[Column("disponibilitehebergement")]
		public bool? DisponibiliteHebergement { get; set; }

	

        [ForeignKey(nameof(IdSejour))]
        [InverseProperty(nameof(Sejour.Descriptionpaniers))]
        public virtual Sejour? Sejour { get; set; }

        [ForeignKey(nameof(IdPanier))]
        [InverseProperty(nameof(Panier.DescriptionsPanier))]
        public virtual Panier? Panier { get; set; }

        [ForeignKey(nameof(IdHebergement))]
        [InverseProperty(nameof(Hebergement.DescriptionsPanier))]
        public virtual Hebergement? Hebergement { get; set; }

        [InverseProperty(nameof(Detient.DescriptionPanierDetient))]
        public virtual List<Detient> DetientCollection { get; set; } = new List<Detient>();

        [InverseProperty(nameof(Comporte.DescriptionPanierComporte))]
        public virtual List<Comporte> ListeDescriptions { get; set; } = new List<Comporte>();

    }
}