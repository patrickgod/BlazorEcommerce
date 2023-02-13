namespace BlazorEcommerce.Server.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;

        public CategoryService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Categories>>> AddCategory(Categories category)
        {
            category.Editing = category.IsNew = false;
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return await GetAdminCategories();
        }

        public async Task<ServiceResponse<List<Categories>>> DeleteCategory(int id)
        {
            Categories category = await GetCategoryById(id);
            if (category == null)
            {
                return new ServiceResponse<List<Categories>>
                {
                    Success = false,
                    Message = "Categories not found."
                };
            }

            category.Deleted = true;
            await _context.SaveChangesAsync();

            return await GetAdminCategories();
        }

        private async Task<Categories> GetCategoryById(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ServiceResponse<List<Categories>>> GetAdminCategories()
        {
            var categories = await _context.Categories
                .Where(c => !c.Deleted)
                .ToListAsync();
            return new ServiceResponse<List<Categories>>
            {
                Data = categories
            };
        }

        public async Task<ServiceResponse<List<Categories>>> GetCategories()
        {
            var categories = await _context.Categories
                .Where(c => !c.Deleted && c.Visible)
                .ToListAsync();
            return new ServiceResponse<List<Categories>>
            {
                Data = categories
            };
        }

        public async Task<ServiceResponse<List<Categories>>> UpdateCategory(Categories category)
        {
            var dbCategory = await GetCategoryById(category.Id);
            if (dbCategory == null)
            {
                return new ServiceResponse<List<Categories>>
                {
                    Success = false,
                    Message = "Categories not found."
                };
            }

            dbCategory.Name = category.Name;
            dbCategory.Url = category.Url;
            dbCategory.Visible = category.Visible;

            await _context.SaveChangesAsync();

            return await GetAdminCategories();

        }
    }
}
