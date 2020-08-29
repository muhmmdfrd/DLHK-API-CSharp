using Repository;
using System.Collections.Generic;
using System.Linq;

namespace Core.Manager.CategoryManager
{
	public class CategoryQuery : AsistanceBase<CategoryAdapter, Category>
	{
		public CategoryQuery(CategoryAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IQueryable<Category> Get()
		{
			return Manager.Database.Categories;
		}

		public List<CategoryDTO> Transform()
		{
			return (from val in Get()
					select new CategoryDTO()
					{
						CategoryId = val.CategoryId,
						CategoryName = val.CategoryName,
						CategoryCode = val.CategoryCode
					}).ToList();

		}

		public CategoryDTO TransformId(long id)
		{
			return (from val in Get()
					where val.CategoryId == id
					select new CategoryDTO()
					{
						CategoryId = val.CategoryId,
						CategoryName = val.CategoryName,
						CategoryCode = val.CategoryCode
					}).FirstOrDefault();
		}
	}
}
