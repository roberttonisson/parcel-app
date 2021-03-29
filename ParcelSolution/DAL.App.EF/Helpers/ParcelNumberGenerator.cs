using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DAL.App.EF.Helpers
{
    public class ParcelNumberGenerator : ValueGenerator<string>
    {
        public override bool GeneratesTemporaryValues => false;
        private readonly int[] numbers = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9};
        private readonly char[] alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
        
        private string NextParcelNumber()
        {
            var parcelNumber = "";
            var random = new Random();
            for (var i = 0; i < 10; i++)
            {
                switch (i)
                {
                    case < 2:
                        parcelNumber += alphabet[random.Next(0, alphabet.Length)].ToString();
                        break;
                    case < 8:
                        parcelNumber += numbers[random.Next(0, numbers.Length)].ToString();
                        break;
                    case >= 8:
                        parcelNumber += alphabet[random.Next(0, alphabet.Length)].ToString();
                        break;
                }
            }

            return parcelNumber;
        }

        /// <summary>
        /// Template method to perform value generation for AccountNumber.
        /// </summary>
        /// <param name="entry">In this case Customer</param>
        /// <returns>Current account number</returns>
        public override string Next(EntityEntry entry) => NextParcelNumber();

        /// <summary>
        /// Gets a value to be assigned to AccountNumber property
        /// </summary>
        /// <param name="entry">In this case Customer</param>
        /// <returns>Current account number</returns>
        protected override object NextValue(EntityEntry entry) => NextParcelNumber();
    }
}