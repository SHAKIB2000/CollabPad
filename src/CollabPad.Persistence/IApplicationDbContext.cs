using CollabPad.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CollabPad.Persistence
{
    public interface IApplicationDbContext
    {
        DbSet<Proposal> Proposals { get; set; }
    }
}
