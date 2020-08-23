using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace aspnetwebapi.Dto.TodoItem
{
    public class TodoItemCreateDto
    {
        [Required]
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
    }
}