using System;
using System.Linq;

namespace CollabPad.Domain.Entities
{
    public enum Status
    {
        Active,
        Inactive
    }
    public class Proposal : IEntity<Guid>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Area { get; set; }
        public DateTime Creation { get; set; }
        public Status Status { get; set; }
    }
}
