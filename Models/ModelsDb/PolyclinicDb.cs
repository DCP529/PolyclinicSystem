using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Column("doctor_id")]
        public Guid DoctorId { get; set; }
                
        public CityDb City { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public ICollection<DoctorDb> Doctors { get; set; }
    }
}
