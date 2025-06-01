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
    public record UpdateCategoryCommand(Guid Id, string Name) :  IRequest<Unit>;

    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _repository;

        public UpdateCategoryCommandHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id, includeDeleted: true);

            if (category is null)
                throw new NotFoundException(nameof(Category), request.Id);

            category.Name = request.Name;
            await _repository.UpdateAsync(category);

            return Unit.Value;
        }
    }
}
