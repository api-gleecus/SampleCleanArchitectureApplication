using MediatR;
using System.Collections.Generic;

namespace Employee.Application.Common
{
    public interface IRequestWrapper<T> : IRequest<ServiceResult<T>>
    {

    }

    public interface IRequestHandlerWrapper<TIn, TOut> : IRequestHandler<TIn, ServiceResult<TOut>> where TIn : IRequestWrapper<TOut>
    {

    }
}
