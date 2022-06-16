using Employee.Application.Commands;
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

    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, EmployeeResponse>
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly ILogger<CreateEmployeeHandler> _logger;
        public CreateEmployeeHandler(IEmployeeRepository employeeRepository, ILogger<CreateEmployeeHandler> logger)
        {
            _employeeRepo = employeeRepository; _logger = logger;
        }
        public async Task<EmployeeResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employeeEntitiy = EmployeeMapper.Mapper.Map<Employee.Core.Entities.Employee>(request);
            if(employeeEntitiy is null)
            {
                _logger.LogInformation("Issue with mapper");
                return null;
            }
            var newEmployee = await _employeeRepo.AddAsync(employeeEntitiy);
            var employeeResponse = EmployeeMapper.Mapper.Map<EmployeeResponse>(newEmployee);
            return employeeResponse;
        }
    }
}
