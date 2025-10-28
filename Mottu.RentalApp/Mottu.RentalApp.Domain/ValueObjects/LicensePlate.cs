using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Mottu.RentalApp.Domain.ValueObjects
{
    public sealed class LicensePlate
    {
        private static readonly Regex PlateRegex = new Regex(@"^[A-Z0-9\-]{4,8}$", RegexOptions.Compiled);

        public string Value { get; }

        private LicensePlate(string value)
        {
            Value = value;
        }

        public static LicensePlate Create(string plate)
        {
            if(string.IsNullOrWhiteSpace(plate))
                throw new ArgumentException("License plate cannot be null or empty.", nameof(plate));

            var normalizedPlate = plate.ToUpper().Trim();

            if(!PlateRegex.IsMatch(normalizedPlate))
                throw new ArgumentException("Invalid license plate format.", nameof(plate));

            return new LicensePlate(normalizedPlate);
        }


        public override string ToString() => Value;
        
    }
}
