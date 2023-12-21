﻿// <auto-generated />
using ASP_API;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ASP_API.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20231216055547_twoTable")]
    partial class twoTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ASP_API.Model.Public.Domain", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("MainDomain")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SubDomain")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Domains");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            MainDomain = "Frontend",
                            SubDomain = "Angular"
                        });
                });

            modelBuilder.Entity("ASP_API.Model.Student.StudentDetails", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BirthDate")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Documents")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("DomainId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Profile")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Qualification")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("DomainId");

                    b.ToTable("studentDetails");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Calicut",
                            BirthDate = "1996-03-19",
                            Documents = "nil",
                            DomainId = 1,
                            Email = "Abhijith@gmail.com",
                            FirstName = "Abhijith",
                            Gender = "male",
                            LastName = "K",
                            Mobile = "9876454423",
                            Password = "abhijith123",
                            Profile = "nil",
                            Qualification = "BCA",
                            Status = "PENDING"
                        });
                });

            modelBuilder.Entity("ASP_API.Model.Student.StudentDetails", b =>
                {
                    b.HasOne("ASP_API.Model.Public.Domain", "Domain")
                        .WithMany()
                        .HasForeignKey("DomainId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Domain");
                });
#pragma warning restore 612, 618
        }
    }
}
