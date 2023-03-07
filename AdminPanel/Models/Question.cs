﻿using AdminPanel.Models.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public partial class Question : IEntityTypeConfiguration<Question>
    {
        public int Id { get; set; }
        public string Text { get; set; } = null!;
        public DateTime CreateDate { get; set; }
        public int UsefullQuestionCount { get; set; }
        public int NeedlesslyCount { get; set; }
        public bool IsActive { get; set; }

        public int UserId { get; set; }
        //public ApplicationUser User { get; set; } = null!;

        public int ProductId { get; set; }
        public Product Product { get; set; } = null!;

        public ICollection<Answer>? Answers { get; set; }


        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.CreateDate).HasColumnType("date");

            builder.HasOne(d => d.Product)
                .WithMany(p => p.Questions)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
