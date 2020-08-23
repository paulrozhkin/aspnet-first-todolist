using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace aspnetwebapi.Dto.TodoItem
{
    public class TodoItemUpdateDto
    {
        [Required]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        
        [Required]
        [JsonProperty(PropertyName = "isComplete")]
        public bool? IsComplete { get; set; }
    }
}