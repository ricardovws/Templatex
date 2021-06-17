using Api.Products.Commands;
using Api.Products.Domain.Models;
using FailFast;
using MediatR;
using Repos;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Products.Handlers
{
    public class CreateUserHandler : IRequestHandler<CreateUser, Response>
    {
        private readonly IUserRepository<User> _repository;

        public CreateUserHandler(IUserRepository<User> repository)
        {
            _repository = repository;
        }

        public async Task<Response> Handle(CreateUser request, CancellationToken cancellationToken)
        {
            var user = new User(request.Name, request.Email, request.Password);

            await _repository.Save(user);

            return new Response("Usuário criado com sucesso");
        }
    }
}
