using Mottu.RentalApp.Domain.Enums;
using Mottu.RentalApp.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Domain.Entities
{
    public class Rider
    {
        public Guid Id { get; private set; }

        public string Identifier { get; private set; } = default!;
        public string Name { get; private set; } = default!;
        public string Cnpj { get; private set; } = default!;
        public DateTime BirthDate { get; private set; } 
        public string CnhNumber { get; private set; } = default!;
        public CnhType CnhType { get; private set; }
        public string? CnhImageUrl { get; private set; }
        public DateTime CreatedAtUtc { get; private set; } = DateTime.UtcNow;

        protected Rider() { }
        private Rider(Guid id, string identifier, string name, Cnpj cnpj, DateTime birthDate, string cnhNumber, CnhType cnhType)
        {
            Id = id;
            Identifier = identifier;
            Name = name;
            Cnpj = cnpj.Value;
            BirthDate = birthDate;
            CnhNumber = cnhNumber;
            CnhType = cnhType;
            CreatedAtUtc = DateTime.UtcNow;
        }

        public static Rider Create(Guid id, string identifier, string name, Cnpj cnpj, DateTime birthDate , string cnhNumber, CnhType cnhType)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name is required.", nameof(name));
            if (birthDate >= DateTime.UtcNow.Date)
                throw new ArgumentException("BirthDate must be in the past.", nameof(birthDate));
            if (string.IsNullOrWhiteSpace(cnhNumber))
                throw new ArgumentException("CNH number is required.", nameof(cnhNumber));

            return new Rider(id,  identifier, name.Trim(), cnpj, DateTime.SpecifyKind(birthDate.Date, DateTimeKind.Utc) , cnhNumber.Trim(), cnhType);
        }

        public void UpdateCnhImage(string imageUrl)
        {
            CnhImageUrl = imageUrl ?? throw new ArgumentNullException(nameof(imageUrl));
        }

        public bool HasCategoryA()
        {
            return CnhType == CnhType.A || CnhType == CnhType.AB;
        }
    }
}
