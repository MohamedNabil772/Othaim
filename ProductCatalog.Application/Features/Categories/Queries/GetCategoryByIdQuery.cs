using MediatR;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.DTOs;
using ProductCatalog.Application.Interfaces.Repositories;
using ProductCatalog.Domain.Entities;


namespace ProductCatalog.Application.Features.Categories.Queries
{
    public record GetCategoryByIdQuery(Guid Id, bool IncludeDeleted = false) : IRequest<CategoryDto>;

    public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryDto>
    {
        private readonly ICategoryRepository _repository;

        public GetCategoryByIdQueryHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<CategoryDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id, request.IncludeDeleted);
            if (category is null)
                throw new NotFoundException(nameof(Category), request.Id);

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name
            };
        }
    }
}

