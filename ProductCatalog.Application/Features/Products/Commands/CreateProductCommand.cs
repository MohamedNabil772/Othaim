using MediatR;
using ProductCatalog.Application.Interfaces.Repositories;
using ProductCatalog.Domain.Entities;


namespace ProductCatalog.Application.Features.Products.Commands
{
    public record CreateProductCommand(string Name, string Description, decimal Price, int StockQuantity, bool IsActive, Guid CategoryId) : IRequest<Guid>;
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Guid>
    {
        private readonly IProductRepository _repository;

        public CreateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                IsActive = request.IsActive,
                CategoryId = request.CategoryId
            };

            await _repository.AddAsync(product);
            return product.Id;
        }
    }

}
