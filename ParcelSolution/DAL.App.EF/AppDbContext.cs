using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contracts.DAL.Base;
using DAL.App.EF.Helpers;
using Domain;
using Domain.Base;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Identity;

namespace DAL.App.EF
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>, IBaseEntityTracker
    {
        private readonly IUserNameProvider? _userNameProvider;

        public DbSet<Bag> Bags { get; set; } = default!;
        public DbSet<LetterBag> LetterBags { get; set; } = default!;
        public DbSet<Parcel> Parcels { get; set; } = default!;
        public DbSet<ParcelBag> ParcelBags { get; set; } = default!;
        public DbSet<Shipment> Shipments { get; set; } = default!;
        public DbSet<ShipmentBag> ShipmentBags { get; set; } = default!;

        private readonly Dictionary<IDomainEntityId<Guid>, IDomainEntityId<Guid>> _entityTracker =
            new Dictionary<IDomainEntityId<Guid>, IDomainEntityId<Guid>>();

        public AppDbContext(DbContextOptions<AppDbContext> options, IUserNameProvider userNameProvider) : base(options)
        {
            _userNameProvider = userNameProvider;
        }

        public void AddToEntityTracker(IDomainEntityId<Guid> internalEntity, IDomainEntityId<Guid> externalEntity)
        {
            _entityTracker.Add(internalEntity, externalEntity);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            //Make Parcel.ParcelNumber Unique
            builder.Entity<Parcel>()
                .HasIndex(p => p.ParcelNumber)
                .IsUnique();
            //Make Parcel.ParcelNumber have a random generated value
            builder.Entity<Parcel>()
                .Property(p => p.ParcelNumber)
                .ValueGeneratedOnAdd()
                .HasValueGenerator<ParcelNumberGenerator>();
            
            //Make Bag.BagNumber Unique
            builder.Entity<Bag>()
                .HasIndex(b => b.BagNumber)
                .IsUnique();
            //Make Bag.BagNumber have a random generated value
            builder.Entity<Bag>()
                .Property(b => b.BagNumber)
                .ValueGeneratedOnAdd()
                .HasValueGenerator<BagNumberGenerator>();
            
            //Make Shipment.ShipmentNumber Unique
            builder.Entity<Shipment>()
                .HasIndex(s => s.ShipmentNumber)
                .IsUnique();
            //Make Bag.ShipmentNumber have a random generated value
            builder.Entity<Shipment>()
                .Property(s => s.ShipmentNumber)
                .ValueGeneratedOnAdd()
                .HasValueGenerator<ShipmentNumberGenerator>();
            
            //Make Shipment.FlightNumber Unique
            builder.Entity<Shipment>()
                .HasIndex(s => s.FlightNumber)
                .IsUnique();
            //Make Bag.FlightNumber have a random generated value
            builder.Entity<Shipment>()
                .Property(s => s.FlightNumber)
                .ValueGeneratedOnAdd()
                .HasValueGenerator<FlightNumberGenerator>();


            // enable/disable cascade delete
            foreach (var relationship in builder.Model
                .GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Cascade;
            }
        }

        private void SaveChangesMetadataUpdate()
        {
            // update the state of ef tracked objects
            ChangeTracker.DetectChanges();

            var markedAsAdded = ChangeTracker.Entries().Where(x => x.State == EntityState.Added);
            foreach (var entityEntry in markedAsAdded)
            {
                if (!(entityEntry.Entity is IDomainEntityMetadata entityWithMetaData)) continue;

                entityWithMetaData.CreatedAt = DateTime.Now;
                entityWithMetaData.CreatedBy = _userNameProvider?.CurrentUserName;
                entityWithMetaData.ChangedAt = entityWithMetaData.CreatedAt;
                entityWithMetaData.ChangedBy = entityWithMetaData.CreatedBy;
            }

            var markedAsModified = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified);
            foreach (var entityEntry in markedAsModified)
            {
                // check for IDomainEntityMetadata
                if (!(entityEntry.Entity is IDomainEntityMetadata entityWithMetaData)) continue;

                entityWithMetaData.ChangedAt = DateTime.Now;
                entityWithMetaData.ChangedBy = _userNameProvider?.CurrentUserName;

                // do not let changes on these properties get into generated db sentences - db keeps old values
                entityEntry.Property(nameof(entityWithMetaData.CreatedAt)).IsModified = false;
                entityEntry.Property(nameof(entityWithMetaData.CreatedBy)).IsModified = false;
            }
        }

        private void UpdateTrackedEntities()
        {
            foreach (var (key, value) in _entityTracker)
            {
                value.Id = key.Id;
            }
        }

        public override int SaveChanges()
        {
            SaveChangesMetadataUpdate();
            var result = base.SaveChanges();
            UpdateTrackedEntities();
            return result;
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SaveChangesMetadataUpdate();
            var result = base.SaveChangesAsync(cancellationToken);
            UpdateTrackedEntities();
            return result;
        }
    }
}