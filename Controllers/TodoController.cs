using System;
using System.Linq;
using System.Threading.Tasks;
using aspnetwebapi.Data;
using aspnetwebapi.Dto;
using aspnetwebapi.Dto.TodoItem;
using aspnetwebapi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace aspnetwebapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _todoContext;
        private readonly IMapper _mapper;

        public TodoController(TodoContext todoContext, IMapper mapper)
        {
            _todoContext = todoContext;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItemDto>> GetTodoItem(long id)
        {
            var todoItem = await _todoContext.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return _mapper.Map<TodoItemDto>(todoItem);
        }

        [HttpGet]
        public async Task<EntitiesDto<TodoItemDto>> GetTodoItem(int? page,
            int? size,
            string name)
        {
            var query = _todoContext.TodoItems.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }

            var todoItems = await PaginatedList<TodoItem>.CreateAsync(
                query.AsNoTracking(),
                page ?? int.MaxValue, size ?? int.MaxValue);

            return new EntitiesDto<TodoItemDto>
            (
                todoItems.PageIndex,
                await query.CountAsync(),
                todoItems.TotalPages,
                todoItems.Select(x => _mapper.Map<TodoItemDto>(x))
            );
        }

        [HttpPost]
        public async Task<ActionResult<TodoItemDto>> PostTodoItem(TodoItemCreateDto todoItemCreateDto)
        {
            if (!TryValidateModel(todoItemCreateDto, nameof(TodoItemUpdateDto)))
            {
                return BadRequest();
            }

            var todoItem = _mapper.Map<TodoItem>(todoItemCreateDto);
            todoItem.TimeCreated = DateTime.UtcNow;

            var todoItemResult = await _todoContext.TodoItems.AddAsync(todoItem);
            await _todoContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new {id = todoItemResult.Entity.Id}, todoItem);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItemDto>> UpdateTodoItem(long id, TodoItemUpdateDto todoItemUpdateDto)
        {
            if (!TryValidateModel(todoItemUpdateDto, nameof(TodoItemUpdateDto)))
            {
                return BadRequest();
            }

            var todoItem = await _todoContext.TodoItems.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _todoContext.Entry(todoItem).State = EntityState.Modified;

            todoItem.IsComplete = todoItemUpdateDto.IsComplete ?? false;
            todoItem.Name = todoItemUpdateDto.Name;

            if (todoItem.IsComplete == false)
            {
                todoItem.TimeCompleted = null;
            }
            else
            {
                todoItem.TimeCompleted = DateTime.UtcNow;
            }

            await _todoContext.SaveChangesAsync();

            return await GetTodoItem(id);
        }
    }
}