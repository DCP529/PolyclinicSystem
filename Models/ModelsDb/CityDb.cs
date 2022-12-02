using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.ModelsDb
{
    [Table(name: "city")]
    public class CityDb
    {
        [Key]
        [Column("city_id")]
        public Guid CityId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [ForeignKey(nameof(CityId))]
        public ICollection<PolyclinicDb> Polyclinics { get; set; }
    }
}
