using System.Collections.Generic;
using Newtonsoft.Json;

namespace aspnetwebapi.Dto
{
    public class EntitiesDto<T>
    {
        [JsonProperty(PropertyName = "currentPage")]
        public int CurrentPage { get; }
        
        [JsonProperty(PropertyName = "totalItems")]
        public int TotalItems { get; }
        
        [JsonProperty(PropertyName = "totalPages")]
        public int TotalPages { get; }
        
        [JsonProperty(PropertyName = "items")]
        public IEnumerable<T> Items { get; }

        public EntitiesDto(int currentPage, int totalItems, int totalPages, IEnumerable<T> items)
        {
            CurrentPage = currentPage;
            TotalItems = totalItems;
            TotalPages = totalPages;
            Items = items;
        }
    }
}