using System;
using System.Threading.Tasks;
using Core;
using Core.Abstractions.Services;
using Domain.Commands;
using Domain.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CreateTaskCommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Create(CreateTaskCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _taskService.CreateTaskCommandHandler(command);
            return Created($"/api/tasks/{result.Payload.Id}", result);
        }

        [HttpPut]
        [Route("[action]")]
        [ProducesResponseType(typeof(CompleteTaskCommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Complete(CompleteTaskCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _taskService.CompleteTaskCommandHandler(command);
                return Ok(result);
            }
            catch (NotFoundException<Guid>)
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("[action]")]
        [ProducesResponseType(typeof(AssignTaskCommandResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> Assign(AssignTaskCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _taskService.AssignTaskCommandHandler(command);
                return Ok(result);
            }
            catch (NotFoundException<Guid>)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(GetAllTasksQueryResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
        {
            var result = await _taskService.GetAllTasksQueryHandler();
            return Ok(result);
        }
    }
}