using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Domain.ValueObjects
{
    public sealed class Cnpj
    {
        public string Value { get; }

        private Cnpj(string value)
        {
            Value = value;
        }

        public static Cnpj Create(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
                throw new ArgumentException("CNPJ cannot be null or empty.", nameof(cnpj));

            var normalizedCnpj = cnpj.Trim();

            if (!IsValidCnpj(normalizedCnpj))
                throw new ArgumentException("Invalid CNPJ format.", nameof(cnpj));

            return new Cnpj(normalizedCnpj);
        }

        private static bool IsValidCnpj(string cnpj)
        {
            // Basic validation: CNPJ must be 14 digits long
            var digitsOnly = new string(cnpj.Where(char.IsDigit).ToArray());
            return digitsOnly.Length == 14;
        }

        public override string ToString() => Value;
    }
}
