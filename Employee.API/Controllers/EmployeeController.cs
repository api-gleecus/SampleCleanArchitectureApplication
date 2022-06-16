using Employee.Application.Commands;
using Employee.Application.Common;
using Employee.Application.Handlers.CommandHandlers;
using Employee.Application.Queries;
using Employee.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Employee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> _logger;
        private readonly IMediator _mediator;
        public EmployeeController(IMediator mediator, ILogger<EmployeeController> logger)
        {
            _mediator = mediator; _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<Employee.Core.Entities.Employee>> Get()
        {
            try
            {
                return await _mediator.Send(new GetAllEmployeeQuery());
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            return null;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<EmployeeResponse>> CreateEmployee([FromBody] CreateEmployeeCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(result);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
            }
            return null;
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResult<EmployeeResponse>>> Update(UpdateEmployeeCommand command, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(command, cancellationToken));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResult<EmployeeResponse>>> Delete(int id, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(new DeleteEmployeeCommand { Id = id }, cancellationToken));
        }

    }
}
