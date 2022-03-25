using MediatR;
using Project.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Project.Application.Users.AuthenticateUseCase
{
    public class AuthenticateHandler : IRequestHandler<AuthenticateQuery, UserViewModel>
    {
        public async Task<UserViewModel> Handle(AuthenticateQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
