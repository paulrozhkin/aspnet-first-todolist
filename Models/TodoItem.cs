using System;
using System.ComponentModel.DataAnnotations;

namespace aspnetwebapi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public bool IsComplete { get; set; }
        
        public DateTime TimeCreated { get; set; }
        
        public DateTime? TimeCompleted { get; set; }
    }
}