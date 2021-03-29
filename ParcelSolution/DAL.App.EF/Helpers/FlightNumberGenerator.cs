using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DAL.App.EF.Helpers
{
    public class FlightNumberGenerator : ValueGenerator<string>
    {
        public override bool GeneratesTemporaryValues => false;
        private readonly int[] numbers = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
        private readonly char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        
        private string NextFlightNumber()
        {
            var flightNumber = "";
            var random = new Random();
            for (var i = 0; i < 6; i++)
            {
                switch (i)
                {
                    case < 2:
                        flightNumber += alphabet[random.Next(0, alphabet.Length)].ToString();
                        break;
                    case >= 2:
                        flightNumber += numbers[random.Next(0, numbers.Length)].ToString();
                        break;
                }
            }

            return flightNumber;
        }

        /// <summary>
        /// Template method to perform value generation for AccountNumber.
        /// </summary>
        /// <param name="entry">In this case Customer</param>
        /// <returns>Current account number</returns>
        public override string Next(EntityEntry entry) => NextFlightNumber();

        /// <summary>
        /// Gets a value to be assigned to AccountNumber property
        /// </summary>
        /// <param name="entry">In this case Customer</param>
        /// <returns>Current account number</returns>
        protected override object NextValue(EntityEntry entry) => NextFlightNumber();
    }
}