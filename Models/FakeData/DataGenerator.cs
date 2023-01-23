using Bogus;
using Models;
using Models.ModelsDb;
using System.Security.Cryptography.X509Certificates;

namespace Services
{
    public class DataGenerator
    {
        public List<DoctorDb> DoctorsGenerate(int countDoctor)
        {
            List<DoctorDb> doctors = new();

            Faker<DoctorDb> generator = new Faker<DoctorDb>("ru")
                .StrictMode(true)
                .RuleFor(x => x.DoctorId, f => f.Random.Guid())
                .RuleFor(x => x.FIO, f => f.Name.FullName())
                .RuleFor(x => x.AdmissionCost, f => f.Random.Number(200, 5000))
                .RuleFor(x => x.ContactNumber, f => 77500000 + f.Random.Number(999))
                .RuleFor(x => x.FullDescription, f => f.Rant.ToString())
                .RuleFor(x => x.ShortDescription, f => f.Rant.ToString())
                .RuleFor(x => x.ImagePath, f => $"{Environment.CurrentDirectory}\\Images\\1661338526_2-oir-mobi-p-pustoi-fon-vkontakte-2.jpg")
                .RuleFor(x => x.Specializations, f => null)
                .RuleFor(x => x.Polyclinics, f => null)
                .RuleFor(x => x.Archived, f => false);

            doctors.AddRange(generator.Generate(countDoctor));

            return doctors;
        }

        public List<PolyclinicDb> PolyclinicsGenerate(int countPolyclinic)
        {
            List<PolyclinicDb> polyclinics = new();

            Faker<PolyclinicDb> generator = new Faker<PolyclinicDb>("ru")
                .StrictMode(true)
                .RuleFor(x => x.Address, f => f.Address.ToString())
                .RuleFor(x => x.PolyclinicId, f => f.Random.Guid())
                .RuleFor(x => x.ContactNumber, f => 77500000 + f.Random.Number(999))
                .RuleFor(x => x.Name, f => f.Company.ToString())
                .RuleFor(x => x.ImagePath, f => $"{Environment.CurrentDirectory}\\Images\\1661338462_1-oir-mobi-p-pustoi-fon-vkontakte-1.png")
                .RuleFor(x => x.Doctors, f => null)
                .RuleFor(x => x.City, f => null)
                .RuleFor(x => x.CityId, f => Guid.Empty)
                .RuleFor(x => x.Archived, f => false);

            polyclinics.AddRange(generator.Generate(countPolyclinic));

            return polyclinics;
        }

        public List<CityDb> CitiesGenerate(int countCities)
        {
            List<CityDb> cities = new();

            Faker<CityDb> generator = new Faker<CityDb>("ru")
                .StrictMode(true)
                .RuleFor(x => x.CityId, f => f.Random.Guid())
                .RuleFor(x => x.Name, f => f.Address.City())
                .RuleFor(x => x.Polyclinics, f => null)
                .RuleFor(x => x.Archived, f => false);

            cities.AddRange(generator.Generate(countCities));

            return cities;
        }

        public List<SpecializationDb> SpecializationsGenerate(int specializationsCount)
        {
            List<SpecializationDb> specializations = new();

            string[] specializationsName = { "Практолог", "Гениколог", "Детский врач", "Нарколог", "Алерголог" };

            Faker<SpecializationDb> generator = new Faker<SpecializationDb>("ru")
                .StrictMode(true)
                .RuleFor(x => x.ExperienceSpecialization, f => f.Random.Number(1, 10))
                .RuleFor(x => x.Name, f => specializationsName[f.Random.Number(0, specializationsName.Length - 1)])
                .RuleFor(x => x.SpecializationId, f => f.Random.Guid())
                .RuleFor(x => x.Doctors, f => null)
                .RuleFor(x => x.DoctorId, f => Guid.Empty)
                .RuleFor(x => x.Archived, f => false);

            specializations.AddRange(generator.Generate(specializationsCount));

            return specializations;
        }
    }
}
