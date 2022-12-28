using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Models.ModelsDb
{
    [Table(name: "doctror")]
    public class DoctorDb
    {
        [Key]
        [Column("doctor_id")]
        public Guid DoctorId { get; set; }

        [Column("fio")]
        public string FIO { get; set; }

        [Column("admission_cost")]
        public decimal AdmissionCost { get; set; }

        [Column("contact_number")]
        public int ContactNumber { get; set; }

        [Column("image_path")]
        public string ImagePath { get; set; }        

        [Column("short_description")]
        public string ShortDescription { get; set; }       
        
        [Column("full_description")]
        public string FullDescription { get; set; }

        [JsonIgnore]
        [ForeignKey(nameof(DoctorId))]
        public ICollection<PolyclinicDb> Polyclinics { get; set; } = new List<PolyclinicDb>();

        [ForeignKey(nameof(DoctorId))]
        public ICollection<SpecializationDb> Specializations { get; set; } = new List<SpecializationDb>();
    }
}
