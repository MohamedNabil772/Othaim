using MediatR;
using ProductCatalog.Application.Common.Exceptions;
using ProductCatalog.Application.Interfaces.Repositories;
using ProductCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCatalog.Application.Features.Categories.Commands
{
    public record RestoreCategoryCommand(Guid Id) : IRequest<Unit>;

    public class RestoreCategoryCommandHandler : IRequestHandler<RestoreCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _repository;

        public RestoreCategoryCommandHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(RestoreCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id, includeDeleted: true);
            if (category is null)
                throw new NotFoundException(nameof(Category), request.Id);

            if (!category.IsDeleted)
                return Unit.Value;

            category.IsDeleted = false;
            await _repository.UpdateAsync(category);

            return Unit.Value;
        }
    }
}
