using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models.ModelsDb
{
    [Table(name: "polyclinic")]
    public class PolyclinicDb
    {
        [Key]
        [Column("polyclinic_id")]
        public Guid PolyclinicId { get; set; }

        [Column("name")]
        public string Name { get; set; }

        [Column("address")]
        public string Address { get; set; }

        [Column("contact_number")]
        public int ContactNumber { get; set; }

        [Column("image_path")]
        public string ImagePath { get; set; }

        [Column("city_id")]
        public Guid CityId { get; set; }

        [Column("archived")]
        public bool Archived { get; set; }

        public virtual CityDb City { get; set; }
        public virtual ICollection<DoctorDb> Doctors { get; set; }

        public PolyclinicDb()
        {
            City = new CityDb();
            Doctors = new List<DoctorDb>();
        }
    }
}
