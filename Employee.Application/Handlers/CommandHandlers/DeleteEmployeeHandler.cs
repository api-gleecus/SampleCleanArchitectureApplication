using Employee.Application.Commands;
using Employee.Application.Common;
using Employee.Application.Mapper;
using Employee.Application.Responses;
using Employee.Core.Repositories;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Employee.Application.Handlers.CommandHandlers
{
    public class DeleteEmployeeCommand : IRequestWrapper<EmployeeResponse>
    {
        public int Id { get; set; }
    }

    public class DeleteEmployeeHandler : IRequestHandlerWrapper<DeleteEmployeeCommand, EmployeeResponse>
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly ILogger<DeleteEmployeeHandler> _logger;
        public DeleteEmployeeHandler(IEmployeeRepository employeeRepository, ILogger<DeleteEmployeeHandler> logger)
        {
            _employeeRepo = employeeRepository; _logger = logger;
        }
        public async Task<ServiceResult<EmployeeResponse>> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _employeeRepo.GetByIdAsync(request.Id);
            if (entity is null)
            {
                _logger.LogInformation("Issue with mapper");
                return null;
            }
             await _employeeRepo.DeleteAsync(entity);
            return null;
        }
    }
}
