using FastReslectionForHabrahabr.Interfaces;
using FastReslectionForHabrahabr.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace FastReslectionForHabrahabr
{
    public class HabraDbContext : DbContext, IStorage
    {
        public virtual DbSet<ContactMapSchema> ContactMapSchemas { get; set; }
    }
}
