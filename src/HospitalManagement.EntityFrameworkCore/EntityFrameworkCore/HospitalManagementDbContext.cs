using HospitalManagement.Departments;
using HospitalManagement.Doctors;
using HospitalManagement.Doctors;
using HospitalManagement.Hospitals;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
namespace HospitalManagement.EntityFrameworkCore;


[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class HospitalManagementDbContext :
    AbpDbContext<HospitalManagementDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Hospital> Hospitals { get; set; }
    public DbSet<Department> Departments { get; set; }

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    public HospitalManagementDbContext(DbContextOptions<HospitalManagementDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

            /* Configure your own tables/entities inside here */
            builder.Entity<Doctor>(b =>
            { 
                b.ToTable(HospitalManagementConsts.DbTablePrefix + "Doctors", HospitalManagementConsts.DbSchema); 
                b.ConfigureByConvention(); 
            
                b.Property(x => x.Name)
                .HasMaxLength(DoctorConsts.MaxNameLength)
                .IsRequired();  
           
                b.Property(x => x.ShortBio)
                .HasMaxLength(DoctorConsts.MaxShortBioLength)
                .IsRequired();

                //one-to-many relationship with Doctor table
                //b.HasOne<Hospital>().WithMany().HasForeignKey(x => x.HospitalId).IsRequired();
            }
            );
        
            builder.Entity<Hospital>(b => {
                b.ToTable(HospitalManagementConsts.DbTablePrefix + "Hospitals", HospitalManagementConsts.DbSchema); 
                b.ConfigureByConvention(); 
            
                b.Property(x => x.Name).HasMaxLength(HospitalConsts.MaxNameLength)
                .IsRequired();
                   
                //many-to-many relationship with Department table => HospitalDepartmnets
                b.HasMany(x=>x.Departments).WithOne().IsRequired();
            });

            builder.Entity<Department>(b => 
            { 
                b.ToTable(HospitalManagementConsts.DbTablePrefix + "Departments", HospitalManagementConsts.DbSchema); 
                b.ConfigureByConvention();
            
                b.Property(x => x.Name)
                .HasMaxLength(DepartmentConsts.MaxNameLength)
                .IsRequired(); 
            }); 
        
            builder.Entity<HospitalDepartment>(b => 
                {
                b.ToTable(HospitalManagementConsts.DbTablePrefix + "HospitalDepartments", HospitalManagementConsts.DbSchema); 
                    b.ConfigureByConvention();
                    //define composite key
                    b.HasKey(x=>new{x.HospitalId,x.DepartmentId});
                
                    //many-to-many configuration
                    b.HasOne<Hospital>().WithMany(x=>x.Departments).HasForeignKey(x=>x.HospitalId).IsRequired();
                    b.HasOne<Department>().WithMany().HasForeignKey(x=>x.DepartmentId).IsRequired();
                    b.HasIndex(x=>new{x.HospitalId,x.DepartmentId});
                });

            builder.Entity<HospitalDepartmentDoctor>(b =>
            {
                b.ToTable(HospitalManagementConsts.DbTablePrefix + "HospitalDepartmentDoctors", HospitalManagementConsts.DbSchema);
                b.ConfigureByConvention();
           
                //define composite key
                b.HasKey(x => new { x.HospitalId, x.DepartmentId,x.DoctorId });

                //many-to-many configuration
                b.HasOne<Hospital>().WithMany().HasForeignKey(x => x.HospitalId).IsRequired();
                b.HasOne<Department>().WithMany().HasForeignKey(x => x.DepartmentId).IsRequired();
                b.HasOne<Doctor>().WithMany().HasForeignKey(x => x.DoctorId).IsRequired();
            
                b.HasIndex(x => new { x.HospitalId, x.DepartmentId,x.DoctorId });

            });



    }
}
