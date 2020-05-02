using FastReslectionForHabrahabr.Models;
using Microsoft.EntityFrameworkCore;

namespace FastReslectionForHabrahabr.Interfaces
{
    public interface IStorage
    {
        DbSet<ContactMapSchema> ContactMapSchemas { get; set; }
    }
}