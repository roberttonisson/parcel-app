using System;
using System.Linq;
using Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Identity;


namespace DAL.App.EF.Helpers
{
    public class DataInitializers
    {
        public static void MigrateDatabase(AppDbContext context)
        {
            context.Database.Migrate();
        }

        public static void DeleteDatabase(AppDbContext context)
        {
            context.Database.EnsureDeleted();
        }


        public static void SeedData(AppDbContext context)
        {
            var bags = new[]
            {
                new Bag
                {
                    Id = new Guid("bab00000-0000-0000-0000-000000000001"),
                },
                new Bag()
                {
                    Id = new Guid("bab00000-0000-0000-0000-000000000002"),
                },
                new Bag()
                {
                    Id = new Guid("bab00000-0000-0000-0000-000000000003"),
                },
            };

            foreach (var bag in bags)
            {
                if (!context.Bags.Any(b => b.Id == bag.Id))
                {
                    context.Bags.Add(bag);
                }
            }

            context.SaveChanges();

            var parcels = new[]
            {
                new Parcel
                {
                    Id = new Guid("eeaaee00-0000-0000-0000-000000000001"),
                    Recipient = "Kati Nuude",
                    Destination = "EE",
                    Weight = 2.115m,
                    Price = 4.25m,
                },
                new Parcel
                {
                    Id = new Guid("eeaaee00-0000-0000-0000-000000000002"),
                    Recipient = "Mati Usk",
                    Destination = "EE",
                    Weight = 3.0m,
                    Price = 12.6m,
                },
                new Parcel
                {
                    Id = new Guid("eeaaee00-0000-0000-0000-000000000003"),
                    Recipient = "Suomi Poika",
                    Destination = "FI",
                    Weight = 3.325m,
                    Price = 12.69m,
                },
                new Parcel
                {
                    Id = new Guid("eeaaee00-0000-0000-0000-000000000004"),
                    Recipient = "Vladislav Vlad",
                    Destination = "RU",
                    Weight = 1.5m,
                    Price = 2m,
                },
                new Parcel
                {
                    Id = new Guid("eeaaee00-0000-0000-0000-000000000005"),
                    Recipient = "Fridreich Dohh",
                    Destination = "DE",
                    Weight = 8.321m,
                    Price = 12.3m,
                },
                new Parcel
                {
                    Id = new Guid("eeaaee01-0000-0000-0000-000000000005"),
                    Recipient = "Astro Naut",
                    Destination = "IT",
                    Weight = 2.321m,
                    Price = 9.3m,
                },
                new Parcel
                {
                    Id = new Guid("eeaaee02-0000-0000-0000-000000000005"),
                    Recipient = "Gena Kona",
                    Destination = "JP",
                    Weight = 6.89m,
                    Price = 2.72m,
                },
            };

            foreach (var parcel in parcels)
            {
                if (!context.Parcels.Any(b => b.Id == parcel.Id))
                {
                    context.Parcels.Add(parcel);
                }
            }

            context.SaveChanges();

            var shipments = new[]
            {
                new Shipment
                {
                    Id = new Guid("aaaabbbb-0000-0000-0000-000000000001"),
                    Airport = Airport.TLL,
                    FlightDate = DateTime.Now.AddDays(14),
                    Finalized = false,
                },
                new Shipment
                {
                    Id = new Guid("aaaabbbb-0000-0000-0000-000000000002"),
                    Airport = Airport.HEL,
                    FlightDate = DateTime.Now.AddDays(15),
                    Finalized = false,
                }
            };

            foreach (var shipment in shipments)
            {
                if (!context.Shipments.Any(b => b.Id == shipment.Id))
                {
                    context.Shipments.Add(shipment);
                }
            }

            context.SaveChanges();

            var parcelBags = new[]
            {
                new ParcelBag
                {
                    Id = new Guid("eeaaee00-abc0-0000-0000-000000000001"),
                    BagId = new Guid("bab00000-0000-0000-0000-000000000001"),
                    ParcelId = new Guid("eeaaee00-0000-0000-0000-000000000001")
                },
                new ParcelBag
                {
                    Id = new Guid("eeaaee00-abc0-0000-0000-000000000002"),
                    BagId = new Guid("bab00000-0000-0000-0000-000000000001"),
                    ParcelId = new Guid("eeaaee00-0000-0000-0000-000000000002"),
                },
                new ParcelBag
                {
                    Id = new Guid("eeaaee00-abc0-0000-0000-000000000003"),
                    BagId = new Guid("bab00000-0000-0000-0000-000000000001"),
                    ParcelId = new Guid("eeaaee00-0000-0000-0000-000000000003"),
                },
                new ParcelBag
                {
                    Id = new Guid("eeaaee00-abc0-0000-0000-000000000004"),
                    BagId = new Guid("bab00000-0000-0000-0000-000000000002"),
                    ParcelId = new Guid("eeaaee00-0000-0000-0000-000000000004"),
                },
                new ParcelBag
                {
                    Id = new Guid("eeaaee00-abc0-0000-0000-000000000005"),
                    BagId = new Guid("bab00000-0000-0000-0000-000000000002"),
                    ParcelId = new Guid("eeaaee00-0000-0000-0000-000000000005"),
                },
            };
            foreach (var parcelBag in parcelBags)
            {
                if (!context.ParcelBags.Any(b => b.Id == parcelBag.Id))
                {
                    context.ParcelBags.Add(parcelBag);
                }
            }

            context.SaveChanges();

            var letterBags = new[]
            {
                new LetterBag
                {
                    Id = new Guid("cccaaa00-abc0-0000-0000-000000000001"),
                    BagId = new Guid("bab00000-0000-0000-0000-000000000003"),
                    Count = 123,
                    Weight = 341.125m,
                    Price = 15.5m,
                }
            };
            foreach (var letterBag in letterBags)
            {
                if (!context.LetterBags.Any(b => b.Id == letterBag.Id))
                {
                    context.LetterBags.Add(letterBag);
                }
            }

            context.SaveChanges();

            var shipmentBags = new[]
            {
                new ShipmentBag
                {
                    Id = new Guid("aaaabbbb-abc0-0000-0000-000000000001"),
                    ShipmentId = new Guid("aaaabbbb-0000-0000-0000-000000000001"),
                    BagId = new Guid("bab00000-0000-0000-0000-000000000001"),
                },
                new ShipmentBag
                {
                    Id = new Guid("aaaabbbb-abc0-0000-0000-000000000002"),
                    ShipmentId = new Guid("aaaabbbb-0000-0000-0000-000000000002"),
                    BagId = new Guid("bab00000-0000-0000-0000-000000000002")
                },
                new ShipmentBag
                {
                    Id = new Guid("aaaabbbb-abc0-0000-0000-000000000003"),
                    ShipmentId = new Guid("aaaabbbb-0000-0000-0000-000000000001"),
                    BagId = new Guid("bab00000-0000-0000-0000-000000000003"),
                }
            };
            foreach (var shipmentBag in shipmentBags)
            {
                if (!context.ShipmentBags.Any(b => b.Id == shipmentBag.Id))
                {
                    context.ShipmentBags.Add(shipmentBag);
                }
            }

            context.SaveChanges();
        }


        public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            var roles = new (string roleName, string roleDisplayName)[]
            {
                ("user", "User"),
                ("admin", "Admin")
            };

            foreach (var (roleName, roleDisplayName) in roles)
            {
                var role = roleManager.FindByNameAsync(roleName).Result;
                if (role == null)
                {
                    role = new AppRole()
                    {
                        Name = roleName,
                        DisplayName = roleDisplayName
                    };

                    var result = roleManager.CreateAsync(role).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("Role creation failed!");
                    }
                }
            }


            var users = new (string name, string password, string firstName, string lastName, string address)[]
            {
                ("admin@parcel.com", "Admin123@", "Admin", "One", "MyAddress"),
            };

            foreach (var userInfo in users)
            {
                var user = userManager.FindByEmailAsync(userInfo.name).Result;
                if (user == null)
                {
                    user = new AppUser()
                    {
                        Email = userInfo.name,
                        UserName = userInfo.name,
                        FirstName = userInfo.firstName,
                        LastName = userInfo.lastName,
                        Address = userInfo.address,
                        EmailConfirmed = true
                    };
                    var result = userManager.CreateAsync(user, userInfo.password).Result;
                    if (!result.Succeeded)
                    {
                        throw new ApplicationException("User creation failed!");
                    }
                }

                var roleResult = userManager.AddToRoleAsync(user, "admin").Result;
                roleResult = userManager.AddToRoleAsync(user, "user").Result;
            }
        }
    }
}