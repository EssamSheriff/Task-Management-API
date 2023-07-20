using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Task_Management_API.DTOS;
using Task_Management_API.Models;

namespace Task_Management_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TasksController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public ActionResult CreateTask(TaskDtoModel taskDto)
        {
            Models.Task task = new()
            {
                Title = taskDto.Title is null ? "System Title" : taskDto.Title,
                Description = taskDto.Description is null ? "System Description" : taskDto.Description,
                DueDate = (DateTime)(taskDto.DueDate is null ? DateTime.Now : taskDto.DueDate),
                IsCompleted = (bool)(taskDto.IsCompleted is null ? false : taskDto.IsCompleted),
            };
            _context.Add(task);
            _context.SaveChanges();
            return Ok(task);

        }


        [HttpGet]
        public ActionResult GetAllTasks() {
            var tasks = _context.Tasks.OrderBy(t => t.IsCompleted).ToList();

            if (!tasks.Any()) return NotFound("No Tasks Found!!");
            return Ok(tasks);
        }
        
        [HttpGet("id/{id}")]
        public ActionResult GetTaskById(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();
            return Ok(task);
        }
        
        [HttpGet("title/{title}")]
        public ActionResult GetTaskByName(String title)
        {
            var task = _context.Tasks.FirstOrDefault(t=> t.Title.ToLower() == title.ToLower());
            if (task == null) return NotFound();
            return Ok(task);
        }

        [HttpGet("Completed")]
        public ActionResult GetAllTasksCompleted()
        {
            var tasks = _context.Tasks.Where(t => t.IsCompleted).ToList();

            if (!tasks.Any()) return NotFound("No Tasks Found!!");
            return Ok(tasks);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteTaskById(int id)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();
            _context.Remove(task);
            _context.SaveChanges();
            return Ok(task);
        }

        [HttpPatch("{id}")]
        public ActionResult UpdateUsingPatch(int id, [FromBody] JsonPatchDocument<Models.Task> jsonPatch)
        {
            if (jsonPatch == null)
                return BadRequest();

            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();

            jsonPatch.ApplyTo(task);
            TryValidateModel(task);

            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.SaveChanges();

            return Ok(task);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTask(int id,[FromBody] TaskDtoModel taskDto)
        {
            var task = _context.Tasks.Find(id);
            if (task == null) return NotFound();
            task.Title = taskDto.Title is null ? task.Title : taskDto.Title;
            task.Description = taskDto.Description is null ? task.Description : taskDto.Description ;
            task.DueDate = (DateTime)(taskDto.DueDate is null ? task.DueDate : taskDto.DueDate);
            _context.Update(task);
            _context.SaveChanges();
            return Ok(task);

        }


    }

}
