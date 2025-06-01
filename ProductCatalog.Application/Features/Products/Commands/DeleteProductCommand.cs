using MediatR;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Interfaces.Repositories;
using ProductCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Features.Products.Commands
{
    public record DeleteProductCommand(Guid Id) : IRequest<Unit>;

    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
    {
        private readonly IProductRepository _repository;

        public DeleteProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id, includeDeleted: true);
            if (product is null)
                throw new NotFoundException(nameof(Product), request.Id);

            await _repository.DeleteAsync(request.Id); 
            return Unit.Value;
        }
    }
}
