﻿using Domain.Entities;
using Domain.Entities.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Persistence.Db;

public partial class UserDbContext : DbContext
{
  private IHttpContextAccessor? _httpCtxRef;

  #region DbSet
  public DbSet<UserK> Users { get; set; } = null!;
  public DbSet<Role> Roles { get; set; } = null!;
  public DbSet<Feature> Features { get; set; } = null!;
  public DbSet<Permission> Permissions { get; set; } = null!;
  #endregion

  public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }

  public UserDbContext(
      DbContextOptions<UserDbContext> options,
      IHttpContextAccessor httpCtxRef) : base(options)
    => _httpCtxRef = httpCtxRef;

  protected override void OnConfiguring(DbContextOptionsBuilder builder) { }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    #region Define Relations
    builder.Entity<UserK>()
        .HasMany(f => f.BetaFeatures)
        .WithMany(f => f.Users)
        .UsingEntity(j => j.ToTable("BetaFeatureUserK"));
    #endregion

    #region Seed Entities
    builder.Entity<Role>().HasData(
        UserDbSeed.Entity<Role>(new List<string>() {
                    "Entreprise.Support", "Entreprise.Distributeur" }));

    builder.Entity<Feature>().HasData(
        UserDbSeed.Entity<Feature>(new List<string>() {
                    "client", "user", "task", "contract" }));

    builder.Entity<Permission>().HasData(
        UserDbSeed.Entity<Permission>(new List<string> {
                    "client.read", "client.create", "client.update",
                    "user.read", "user.impersonate", "user.create", "user.update", "user.suspend", "user.deactivate",
                    "task.read", "task.create", "task.update",
                    "contract.read" }));
    #endregion

    #region Seed Relations
    builder.Entity<Role>()
        .HasMany(f => f.Features)
        .WithMany(f => f.Roles)
        .UsingEntity(j => j.HasData(
            UserDbSeed.RelationRoleFeature(
                new Dictionary<int, List<int>> {
                            { 1, new List<int> { 1, 2, 3, 4 } }, // Entreprise.Support
                            { 2, new List<int> { 1, 3, 4 } }  // Entreprise.ChargeMission
                })));

    builder.Entity<Feature>()
        .HasMany(f => f.Permissions)
        .WithMany(f => f.Features)
        .UsingEntity(j => j.HasData(
            UserDbSeed.RelationFeaturePermission(
                new Dictionary<int, List<int>> {
                            { 1, new List<int> { 1, 2, 3 } }, // Client
                            { 2, new List<int> { 4, 5, 6, 7, 8, 9 } }, // User
                            { 3, new List<int> { 10, 11, 12 } }, // Task
                            { 4, new List<int> { 13 } } // Contract
                })));
    #endregion
  }

  #region SaveChanges Override
  public override int SaveChanges()
  {
    AddTimestamps();
    return base.SaveChanges();
  }

  public override async Task<int> SaveChangesAsync(CancellationToken token = default)
  {
    AddTimestamps();
    return await base.SaveChangesAsync(token);
  }

  private void AddTimestamps()
  {
    var entityEntries = ChangeTracker
        .Entries()
        .Where(x => x.Entity is ITrackedEntity
            && (x.State == EntityState.Added
                || x.State == EntityState.Modified));

    var httpCtxUser = _httpCtxRef!.HttpContext?.User;
    string userName = httpCtxUser?.FindFirst(c => c.Type == ClaimTypes.Email)?.Value ?? // user connection : email
                      httpCtxUser?.FindFirst(c => c.Type == "client_id")?.Value ?? // m2m connection : client_id
                      "anonymous";

    foreach (var entityEntry in entityEntries)
    {
      var now = DateTime.Now;
      var entity = (ITrackedEntity)entityEntry.Entity;

      if (entityEntry.State == EntityState.Added)
      {
        entity.CreatedAt = now;
        entity.CreatedBy = userName;
      }

      entity.ModifiedAt = now;
      entity.ModifiedBy = userName;
    }
  }
  #endregion
}