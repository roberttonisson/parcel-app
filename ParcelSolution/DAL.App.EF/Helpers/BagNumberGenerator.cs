using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DAL.App.EF.Helpers
{
    public class BagNumberGenerator : ValueGenerator<string>
    {
        public override bool GeneratesTemporaryValues => false;
        private readonly char[] characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
        
        private string NextBagNumber()
        {
            var bagNumber = "";
            var random = new Random();
            for (var i = 0; i < 15; i++)
            {
                bagNumber += characters[random.Next(0, characters.Length)].ToString();
            }

            return bagNumber;
        }

        /// <summary>
        /// Template method to perform value generation for AccountNumber.
        /// </summary>
        /// <param name="entry">In this case Customer</param>
        /// <returns>Current account number</returns>
        public override string Next(EntityEntry entry) => NextBagNumber();

        /// <summary>
        /// Gets a value to be assigned to AccountNumber property
        /// </summary>
        /// <param name="entry">In this case Customer</param>
        /// <returns>Current account number</returns>
        protected override object NextValue(EntityEntry entry) => NextBagNumber();
    }
}