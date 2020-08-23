using System;
using Newtonsoft.Json;

namespace aspnetwebapi.Dto.TodoItem
{
    public class TodoItemDto
    {
        [JsonProperty(PropertyName = "id")]
        public long Id { get; set; }
        
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        
        [JsonProperty(PropertyName = "isComplete")]
        public bool IsComplete { get; set; }
        
        [JsonProperty(PropertyName = "timeCreated")]
        public DateTime TimeCreated { get; set; }
        
        [JsonProperty(PropertyName = "timeCompleted")]
        public DateTime TimeCompleted { get; set; }
    }
}