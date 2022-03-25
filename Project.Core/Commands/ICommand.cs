using FluentValidation.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Core.Commands
{
    public interface ICommand<out TResult> : IRequest<TResult>
    {
        ValidationResult Validate();
    }

    public interface ICommand : IRequest
    {
        ValidationResult Validate();
    }
}
