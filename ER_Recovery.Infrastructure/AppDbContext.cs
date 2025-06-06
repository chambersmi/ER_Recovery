﻿using ER_Recovery.Domains.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ER_Recovery.Infrastructure
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<MessageBoard>()
                .HasOne(m => m.User)
                .WithMany(u => u.Messages)
                .HasForeignKey(m => m.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<PostReply>()
                .HasOne(r => r.Message)
                .WithMany(m => m.Replies)
                .HasForeignKey(r => r.ParentMessageId)
                .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<MessageBoard> MessageBoard { get; set; }
        public DbSet<Announcement> Announcements { get; set; }
        public DbSet<PostReply> MessageReplies { get; set; }
    }
}
