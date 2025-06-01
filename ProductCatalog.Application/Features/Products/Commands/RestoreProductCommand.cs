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
    public record RestoreProductCommand(Guid Id) : IRequest<Unit>;

    public class RestoreProductCommandHandler : IRequestHandler<RestoreProductCommand, Unit>
    {
        private readonly IProductRepository _repository;

        public RestoreProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(RestoreProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id, includeDeleted: true);
            if (product is null)
                throw new NotFoundException(nameof(Product), request.Id);

            if (!product.IsDeleted)
                return Unit.Value; 

            product.IsDeleted = false;
            await _repository.UpdateAsync(product);

            return Unit.Value;
        }
    }
}
