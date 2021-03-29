using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace DAL.App.EF.Helpers
{
    public class ShipmentNumberGenerator : ValueGenerator<string>
    {
        public override bool GeneratesTemporaryValues => false;
        private readonly char[] characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789".ToCharArray();
        
        private string NextShipmentNumber()
        {
            var shipmentNumber = "";
            var random = new Random();
            for (var i = 0; i < 10; i++)
            {
                switch (i)
                {
                    case < 3:
                        shipmentNumber += characters[random.Next(0, characters.Length)].ToString();
                        break;
                    case 3:
                        shipmentNumber += "-";
                        break;
                    case > 3:
                        shipmentNumber += characters[random.Next(0, characters.Length)].ToString();
                        break;
                }
            }
            Console.WriteLine(shipmentNumber);
            return shipmentNumber;
        }

        /// <summary>
        /// Template method to perform value generation for AccountNumber.
        /// </summary>
        /// <param name="entry">In this case Customer</param>
        /// <returns>Current account number</returns>
        public override string Next(EntityEntry entry) => NextShipmentNumber();

        /// <summary>
        /// Gets a value to be assigned to AccountNumber property
        /// </summary>
        /// <param name="entry">In this case Customer</param>
        /// <returns>Current account number</returns>
        protected override object NextValue(EntityEntry entry) => NextShipmentNumber();
    }
}