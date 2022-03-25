using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Queries
{
    public interface IQuery<out TResult> : IRequest<TResult>
    {
        ValidationResult Validate();
    }
}
