using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id {get;set;}
        
        public DateTime DateCreate{get;set;}
        public DateTime? DateUpdate{get;set;}
    }
}
