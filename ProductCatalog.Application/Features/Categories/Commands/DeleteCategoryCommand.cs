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
    public record DeleteCategoryCommand(Guid Id) : IRequest<Unit>;

    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand, Unit>
    {
        private readonly ICategoryRepository _repository;

        public DeleteCategoryCommandHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id, includeDeleted: true);
            if (category is null)
                throw new NotFoundException(nameof(Category), request.Id);

            await _repository.DeleteAsync(request.Id);
            return Unit.Value;
        }
    }
}
