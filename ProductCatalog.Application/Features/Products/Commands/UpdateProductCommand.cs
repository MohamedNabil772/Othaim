using MediatR;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Interfaces.Repositories;
using ProductCatalog.Domain.Entities;


namespace ProductCatalog.Application.Features.Products.Commands
{
    public record UpdateProductCommand(Guid Id, string Name, string Description, decimal Price, int StockQuantity, bool IsActive, Guid CategoryId) : IRequest<Unit>;

    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
    {
        private readonly IProductRepository _repository;

        public UpdateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id, includeDeleted: true);

            if (product == null)
                throw new NotFoundException(nameof(Product), request.Id);

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.StockQuantity = request.StockQuantity;
            product.IsActive = request.IsActive;
            product.CategoryId = request.CategoryId;

            await _repository.UpdateAsync(product);
            return Unit.Value;
        }
    }
}