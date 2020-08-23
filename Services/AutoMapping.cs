using aspnetwebapi.Dto.TodoItem;
using aspnetwebapi.Models;
using AutoMapper;

namespace aspnetwebapi.Services
{
    public class AutoMapping: Profile
    {
        public AutoMapping()
        {
            CreateMap<TodoItemCreateDto, TodoItem>();
            CreateMap<TodoItem, TodoItemDto>();
        }
    }
}