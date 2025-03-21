﻿
namespace Task.Core.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime? Date { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
