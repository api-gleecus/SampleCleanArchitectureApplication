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
    public class UpdateEmployeeCommand : IRequestWrapper<EmployeeResponse>
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }

    public class UpdateEmployeeHandler : IRequestHandlerWrapper<UpdateEmployeeCommand, EmployeeResponse>
    {
        private readonly IEmployeeRepository _employeeRepo;
        private readonly ILogger<UpdateEmployeeCommand> _logger;
        public UpdateEmployeeHandler(IEmployeeRepository employeeRepository, ILogger<UpdateEmployeeCommand> logger)
        {
            _employeeRepo = employeeRepository; _logger = logger;
        }
        public async Task<ServiceResult<EmployeeResponse>> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            var entity = await _employeeRepo.GetByIdAsync(request.Id);
            if (entity is null)
            {
                _logger.LogInformation("Issue with mapper");
                return null;
            }

            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.DateOfBirth = request.DateOfBirth;
            entity.PhoneNumber = request.PhoneNumber;
            entity.Email = request.Email;

            await _employeeRepo.UpdateAsync(entity);
            return ServiceResult.Success(EmployeeMapper.Mapper.Map<EmployeeResponse>(entity));
        }
    }
}
