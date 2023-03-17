﻿using AutoMapper;
using BlazorEcommerce.Server.Services.FinanceService;
using BlazorEcommerce.Shared.Models;

namespace BlazorEcommerce.Server.Services.PersonService
{
    public class PersonService : IPersonService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly FinanceService.FinanceService _financeService;

        public PersonService(DataContext context, IHttpContextAccessor httpContextAccessor,IMapper mapper,FinanceService.FinanceService financeService)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _financeService = financeService;

        }

         public async Task<ServiceResponse<PersonDto>> CreatePerson(PersonDto person)
        {

            var objectToAdd = _mapper.Map<Person>(person);
            _context.Person.Add(objectToAdd);
             await _context.SaveChangesAsync();

            //Add Finance record for new user
            await _financeService.AddFinanceDateForNewPerson(FinanceOperation.Operation.Add, objectToAdd.Personid);
            return new ServiceResponse<PersonDto> { Data = person };

            //throw new NotImplementedException();

        }

        public async Task<ServiceResponse<PersonDto>> UpdatePerson(PersonDto person)
        {
            var dbProduct = await _context.Person.AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Personid == person.Personid);

            if (dbProduct == null)
            {
                return new ServiceResponse<PersonDto>
                {
                    Success = false,
                    Message = "Kullanıcı bulunamadı"
                };
            }

            var updatedEntity = _mapper.Map<Person>(person);
//            updatedEntity.Class = null;

            try
            {  
                if(_context.Entry(updatedEntity).State == EntityState.Detached){
                    _context.Attach(updatedEntity);
                    _context.Entry(updatedEntity).State = EntityState.Modified;
                }
        

                await _context.SaveChangesAsync(); 
                
                    }
            catch (Exception exp)
            {

                throw new Exception(exp.Message);
            }
          
            return new ServiceResponse<PersonDto> { Data = person };
        }

        public async Task<ServiceResponse<bool>> DeletePerson(Guid personID)
        {
            var dbProduct = await _context.Person.FindAsync(personID);
            if (dbProduct == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Kullanıcı bulunamadı"
                };
            }

            dbProduct.Deleted = true;
            await _context.SaveChangesAsync();

            //Delete from Finance
            //await this._financeService.DeleteByPersonId(personID);

            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<PersonDto>> GetPersonAsync(int personId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Guid>> GetPersonIdListAsync()
        {
            var personIdList = await _context.Person.Where(s => !s.Deleted).OrderBy(t=>t.Fullname).Select(t => t.Personid).ToListAsync();
            return personIdList;

        }

        public async Task<ServiceResponse<List<PersonDto>>> GetPersonListAsync()
        {
            var personList = await _context.Person.OrderBy(t => t.Fullname).Where(s=>!s.Deleted).Include(s=>s.Class).ToListAsync();
            var result = personList.Select(s => _mapper.Map<PersonDto>(s)).ToList();
             return new ServiceResponse<List<PersonDto>> { Data = result };
            
        }

        public async Task<ServiceResponse<PersonDto>> GetPersonListByName(string searchText)
        {
            throw new NotImplementedException();
        }

    

        //    public async Task<ServiceResponse<PersonDto>> CreateProduct(PersonDto product)
        //    {
        //        foreach (var variant in product.Variants)
        //        {
        //            variant.ProductType = null;
        //        }
        //        _context.Products.Add(product);
        //        await _context.SaveChangesAsync();
        //        return new ServiceResponse<Product> { Data = product };
        //    }



        //    public async Task<ServiceResponse<bool>> DeleteProduct(int productId)
        //    {
        //        var dbProduct = await _context.Products.FindAsync(productId);
        //        if (dbProduct == null)
        //        {
        //            return new ServiceResponse<bool>
        //            {
        //                Success = false,
        //                Data = false,
        //                Message = "Product not found."
        //            };
        //        }

        //        dbProduct.Deleted = true;

        //        await _context.SaveChangesAsync();
        //        return new ServiceResponse<bool> { Data = true };
        //    }

        //    public async Task<ServiceResponse<List<Product>>> GetAdminProducts()
        //    {
        //        var response = new ServiceResponse<List<Product>>
        //        {
        //            Data = await _context.Products
        //                .Where(p => !p.Deleted)
        //                .Include(p => p.Variants.Where(v => !v.Deleted))
        //                .ThenInclude(v => v.ProductType)
        //                .Include(p => p.Images)
        //                .ToListAsync()
        //        };

        //        return response;
        //    }

        //    public async Task<ServiceResponse<List<Product>>> GetFeaturedProducts()
        //    {
        //        var response = new ServiceResponse<List<Product>>
        //        {
        //            Data = await _context.Products
        //                .Where(p => p.Featured && p.Visible && !p.Deleted)
        //                .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
        //                .Include(p => p.Images)
        //                .ToListAsync()
        //        };

        //        return response;
        //    }

        //    public async Task<ServiceResponse<Product>> GetProductAsync(int productId)
        //    {
        //        var response = new ServiceResponse<Product>();
        //        Product product = null;

        //        if (_httpContextAccessor.HttpContext.User.IsInRole("Admin"))
        //        {
        //            product = await _context.Products
        //                .Include(p => p.Variants.Where(v => !v.Deleted))
        //                .ThenInclude(v => v.ProductType)
        //                .Include(p => p.Images)
        //                .FirstOrDefaultAsync(p => p.Id == productId && !p.Deleted);
        //        }
        //        else
        //        {
        //            product = await _context.Products
        //                .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
        //                .ThenInclude(v => v.ProductType)
        //                .Include(p => p.Images)
        //                .FirstOrDefaultAsync(p => p.Id == productId && !p.Deleted && p.Visible);
        //        }

        //        if (product == null)
        //        {
        //            response.Success = false;
        //            response.Message = "Sorry, but this product does not exist.";
        //        }
        //        else
        //        {
        //            response.Data = product;
        //        }

        //        return response;
        //    }

        //    public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        //    {
        //        var response = new ServiceResponse<List<Product>>
        //        {
        //            Data = await _context.Products
        //                .Where(p => p.Visible && !p.Deleted)
        //                .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
        //                .Include(p => p.Images)
        //                .ToListAsync()
        //        };

        //        return response;
        //    }

        //    public async Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl)
        //    {
        //        var response = new ServiceResponse<List<Product>>
        //        {
        //            Data = await _context.Products
        //                .Where(p => p.Category.Url.ToLower().Equals(categoryUrl.ToLower()) &&
        //                    p.Visible && !p.Deleted)
        //                .Include(p => p.Variants.Where(v => v.Visible && !v.Deleted))
        //                .Include(p => p.Images)
        //                .ToListAsync()
        //        };

        //        return response;
        //    }

        //    public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText)
        //    {
        //        var products = await FindProductsBySearchText(searchText);

        //        List<string> result = new List<string>();

        //        foreach (var product in products)
        //        {
        //            if (product.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase))
        //            {
        //                result.Add(product.Title);
        //            }

        //            if (product.Description != null)
        //            {
        //                var punctuation = product.Description.Where(char.IsPunctuation)
        //                    .Distinct().ToArray();
        //                var words = product.Description.Split()
        //                    .Select(s => s.Trim(punctuation));

        //                foreach (var word in words)
        //                {
        //                    if (word.Contains(searchText, StringComparison.OrdinalIgnoreCase)
        //                        && !result.Contains(word))
        //                    {
        //                        result.Add(word);
        //                    }
        //                }
        //            }
        //        }

        //        return new ServiceResponse<List<string>> { Data = result };
        //    }

        //    public async Task<ServiceResponse<ProductSearchResult>> SearchProducts(string searchText, int page)
        //    {
        //        var pageResults = 2f;
        //        var pageCount = Math.Ceiling((await FindProductsBySearchText(searchText)).Count / pageResults);
        //        var products = await _context.Products
        //                            .Where(p => p.Title.ToLower().Contains(searchText.ToLower()) ||
        //                                p.Description.ToLower().Contains(searchText.ToLower()) &&
        //                                p.Visible && !p.Deleted)
        //                            .Include(p => p.Variants)
        //                            .Include(p => p.Images)
        //                            .Skip((page - 1) * (int)pageResults)
        //                            .Take((int)pageResults)
        //                            .ToListAsync();

        //        var response = new ServiceResponse<ProductSearchResult>
        //        {
        //            Data = new ProductSearchResult
        //            {
        //                Products = products,
        //                CurrentPage = page,
        //                Pages = (int)pageCount
        //            }
        //        };

        //        return response;
        //    }

        //    public async Task<ServiceResponse<Product>> UpdateProduct(Product product)
        //    {
        //        var dbProduct = await _context.Products
        //            .Include(p => p.Images)
        //            .FirstOrDefaultAsync(p => p.Id == product.Id);

        //        if (dbProduct == null)
        //        {
        //            return new ServiceResponse<Product>
        //            {
        //                Success = false,
        //                Message = "Product not found."
        //            };
        //        }

        //        dbProduct.Title = product.Title;
        //        dbProduct.Description = product.Description;
        //        dbProduct.ImageUrl = product.ImageUrl;
        //        dbProduct.CategoryId = product.CategoryId;
        //        dbProduct.Visible = product.Visible;
        //        dbProduct.Featured = product.Featured;

        //        var productImages = dbProduct.Images;
        //        _context.Images.RemoveRange(productImages);

        //        dbProduct.Images = product.Images;

        //        foreach (var variant in product.Variants)
        //        {
        //            var dbVariant = await _context.ProductVariants
        //                .SingleOrDefaultAsync(v => v.ProductId == variant.ProductId &&
        //                    v.ProductTypeId == variant.ProductTypeId);
        //            if (dbVariant == null)
        //            {
        //                variant.ProductType = null;
        //                _context.ProductVariants.Add(variant);
        //            }
        //            else
        //            {
        //                dbVariant.ProductTypeId = variant.ProductTypeId;
        //                dbVariant.Price = variant.Price;
        //                dbVariant.OriginalPrice = variant.OriginalPrice;
        //                dbVariant.Visible = variant.Visible;
        //                dbVariant.Deleted = variant.Deleted;
        //            }
        //        }

        //        await _context.SaveChangesAsync();
        //        return new ServiceResponse<Product> { Data = product };
        //    }

        //    private async Task<List<Product>> FindProductsBySearchText(string searchText)
        //    {
        //        return await _context.Products
        //                            .Where(p => p.Title.ToLower().Contains(searchText.ToLower()) ||
        //                                p.Description.ToLower().Contains(searchText.ToLower()) &&
        //                                p.Visible && !p.Deleted)
        //                            .Include(p => p.Variants)
        //                            .ToListAsync();
        //    }
        //}


    }
}
