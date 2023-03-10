using AdminPanel.Data;
using AdminPanel.Models;
using MediatR;

namespace AdminPanel.MediatorHandlers.Products.Categories
{
    public class GetSubcategoriesForMainCategoryQuery : IRequest<IQueryable<Subcategory>>
    {
        public int Id { get; set; }
        public GetSubcategoriesForMainCategoryQuery(int id)
        {
            Id = id;
        }
    }


    public class GetSubcategoriesForMainCategoryQueryHandler : IRequestHandler<GetSubcategoriesForMainCategoryQuery, IQueryable<Subcategory>>
    {
        private readonly ApplicationDbContext _context;

        public GetSubcategoriesForMainCategoryQueryHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IQueryable<Subcategory>> Handle(GetSubcategoriesForMainCategoryQuery request, CancellationToken cancellationToken)
        {
            return _context.Subcategories.Where(x => x.MainCategoryId == request.Id);
        }
    }
}
