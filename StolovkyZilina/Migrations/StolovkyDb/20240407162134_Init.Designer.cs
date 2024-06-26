﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StolovkyZilina.Data;

#nullable disable

namespace StolovkyZilina.Migrations.StolovkyDb
{
    [DbContext(typeof(StolovkyDbContext))]
    [Migration("20240407162134_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ContentTag", b =>
                {
                    b.Property<Guid>("ContentsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TagsId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ContentsId", "TagsId");

                    b.HasIndex("TagsId");

                    b.ToTable("ContentTag");
                });

            modelBuilder.Entity("GameGamePoll", b =>
                {
                    b.Property<Guid>("GamePollsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GamesInPollId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GamePollsId", "GamesInPollId");

                    b.HasIndex("GamesInPollId");

                    b.ToTable("GameGamePoll");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.AdminMessage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Subject")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AdminMessages");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.AuctionOffer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int?>("IdealPlayerCount")
                        .HasColumnType("int");

                    b.Property<int?>("Offer")
                        .HasColumnType("int");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("AuctionOffers");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.Comment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ContentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("ContentId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.Content", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Contents");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Content");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.Event", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("AuctionType")
                        .HasColumnType("int");

                    b.Property<Guid>("ContentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("FeaturedImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("MakeGamesSuggestion")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortDescription")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ContentId");

                    b.HasIndex("LocationId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Approved")
                        .HasColumnType("bit");

                    b.Property<string>("Author")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("ContentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Cooperative")
                        .HasColumnType("bit");

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Difficulty")
                        .HasColumnType("int");

                    b.Property<byte[]>("FeaturedImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid?>("GameCategoryId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MaxPlayerCount")
                        .HasColumnType("int");

                    b.Property<int>("MinPlayerCount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OnPoints")
                        .HasColumnType("bit");

                    b.Property<int?>("Playtime")
                        .HasColumnType("int");

                    b.Property<string>("ShortDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SpaceRequirement")
                        .HasColumnType("int");

                    b.Property<string>("UrlHandle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ContentId");

                    b.HasIndex("GameCategoryId");

                    b.ToTable("Games");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.GameCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GameCategories");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.GameLanguage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("LanguageName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.GameOwner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BuyingInterest")
                        .HasColumnType("int");

                    b.Property<int?>("Condition")
                        .HasColumnType("int");

                    b.Property<bool>("DeluxeComponents")
                        .HasColumnType("bit");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Insert")
                        .HasColumnType("bit");

                    b.Property<bool>("IsFavorite")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOwner")
                        .HasColumnType("bit");

                    b.Property<Guid?>("LanguageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("PlaiyngInterest")
                        .HasColumnType("int");

                    b.Property<bool>("PromoContent")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("LanguageId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Owners");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.GamePlay", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ContentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("EndTime")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid?>("LocationId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("StartTime")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ContentId");

                    b.HasIndex("EventId");

                    b.HasIndex("GameId");

                    b.HasIndex("LocationId");

                    b.ToTable("Plays");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.GamePoll", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("NumberOfVotesPerUser")
                        .HasColumnType("int");

                    b.Property<string>("PollName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("GamePolls");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.GameVote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GamePollId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("GamePollId");

                    b.HasIndex("UserId");

                    b.ToTable("GameVotes");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.Location", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("BuildingNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DoorNumer")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FloorNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GoogleMapsLink")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostalCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShortDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Space")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlHandle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.ParticipationVote", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("EventId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("VoteStatus")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.ToTable("ParticipationVotes");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.PlayerMmr", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Mmr")
                        .HasColumnType("float");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.HasIndex("UserId");

                    b.ToTable("PlayerMmrs");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.PlayerPlayResult", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GamePlayId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("GuestPlayerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MmrDiff")
                        .HasColumnType("int");

                    b.Property<Guid?>("PlayerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PlayerInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Result")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GamePlayId");

                    b.HasIndex("PlayerId");

                    b.ToTable("PlayerPlayResults");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.Rating", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ContentId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("UserRating")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ContentId");

                    b.ToTable("Ratings");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.UserProfile", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("City")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Competitive")
                        .HasColumnType("bit");

                    b.Property<string>("Desc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("FeaturedImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("Influence")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PrefferedDifficulty")
                        .HasColumnType("int");

                    b.Property<string>("PrefferedPlayDay")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PrefferedPlayerCount")
                        .HasColumnType("int");

                    b.Property<int?>("PrefferedPlaytime")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.BlogPost", b =>
                {
                    b.HasBaseType("StolovkyZilina.Models.Domain.Content");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("FeaturedImage")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Heading")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PageTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("PublishDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("ShortDesc")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UrlHandle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Visible")
                        .HasColumnType("bit");

                    b.HasDiscriminator().HasValue("BlogPost");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.Feed", b =>
                {
                    b.HasBaseType("StolovkyZilina.Models.Domain.Content");

                    b.Property<string>("Body")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("datetime2");

                    b.HasDiscriminator().HasValue("Feed");
                });

            modelBuilder.Entity("ContentTag", b =>
                {
                    b.HasOne("StolovkyZilina.Models.Domain.Content", null)
                        .WithMany()
                        .HasForeignKey("ContentsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StolovkyZilina.Models.Domain.Tag", null)
                        .WithMany()
                        .HasForeignKey("TagsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameGamePoll", b =>
                {
                    b.HasOne("StolovkyZilina.Models.Domain.GamePoll", null)
                        .WithMany()
                        .HasForeignKey("GamePollsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StolovkyZilina.Models.Domain.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesInPollId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.AuctionOffer", b =>
                {
                    b.HasOne("StolovkyZilina.Models.Domain.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StolovkyZilina.Models.Domain.UserProfile", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.Comment", b =>
                {
                    b.HasOne("StolovkyZilina.Models.Domain.Content", null)
                        .WithMany("Comments")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.Event", b =>
                {
                    b.HasOne("StolovkyZilina.Models.Domain.Content", "Content")
                        .WithMany()
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StolovkyZilina.Models.Domain.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Content");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.Game", b =>
                {
                    b.HasOne("StolovkyZilina.Models.Domain.Content", "Content")
                        .WithMany()
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StolovkyZilina.Models.Domain.GameCategory", "GameCategory")
                        .WithMany()
                        .HasForeignKey("GameCategoryId");

                    b.Navigation("Content");

                    b.Navigation("GameCategory");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.GameOwner", b =>
                {
                    b.HasOne("StolovkyZilina.Models.Domain.Game", "BoardGame")
                        .WithMany("Owners")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StolovkyZilina.Models.Domain.GameLanguage", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId");

                    b.HasOne("StolovkyZilina.Models.Domain.UserProfile", "Owner")
                        .WithMany("GamesOwned")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BoardGame");

                    b.Navigation("Language");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.GamePlay", b =>
                {
                    b.HasOne("StolovkyZilina.Models.Domain.Content", "Content")
                        .WithMany()
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StolovkyZilina.Models.Domain.Event", "Event")
                        .WithMany("GamePlays")
                        .HasForeignKey("EventId");

                    b.HasOne("StolovkyZilina.Models.Domain.Game", "Game")
                        .WithMany("Plays")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StolovkyZilina.Models.Domain.Location", "Location")
                        .WithMany("Plays")
                        .HasForeignKey("LocationId");

                    b.Navigation("Content");

                    b.Navigation("Event");

                    b.Navigation("Game");

                    b.Navigation("Location");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.GamePoll", b =>
                {
                    b.HasOne("StolovkyZilina.Models.Domain.Event", "Event")
                        .WithMany("GamePolls")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Event");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.GameVote", b =>
                {
                    b.HasOne("StolovkyZilina.Models.Domain.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StolovkyZilina.Models.Domain.GamePoll", null)
                        .WithMany("GameVotes")
                        .HasForeignKey("GamePollId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StolovkyZilina.Models.Domain.UserProfile", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.ParticipationVote", b =>
                {
                    b.HasOne("StolovkyZilina.Models.Domain.Event", null)
                        .WithMany("ParticipationVotes")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.PlayerMmr", b =>
                {
                    b.HasOne("StolovkyZilina.Models.Domain.Game", "Game")
                        .WithMany()
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StolovkyZilina.Models.Domain.UserProfile", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");

                    b.Navigation("User");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.PlayerPlayResult", b =>
                {
                    b.HasOne("StolovkyZilina.Models.Domain.GamePlay", "GamePlay")
                        .WithMany("Results")
                        .HasForeignKey("GamePlayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("StolovkyZilina.Models.Domain.UserProfile", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");

                    b.Navigation("GamePlay");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.Rating", b =>
                {
                    b.HasOne("StolovkyZilina.Models.Domain.Content", null)
                        .WithMany("Likes")
                        .HasForeignKey("ContentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.Content", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("Likes");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.Event", b =>
                {
                    b.Navigation("GamePlays");

                    b.Navigation("GamePolls");

                    b.Navigation("ParticipationVotes");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.Game", b =>
                {
                    b.Navigation("Owners");

                    b.Navigation("Plays");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.GamePlay", b =>
                {
                    b.Navigation("Results");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.GamePoll", b =>
                {
                    b.Navigation("GameVotes");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.Location", b =>
                {
                    b.Navigation("Plays");
                });

            modelBuilder.Entity("StolovkyZilina.Models.Domain.UserProfile", b =>
                {
                    b.Navigation("GamesOwned");
                });
#pragma warning restore 612, 618
        }
    }
}
