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

		public IQueryable<CategoryDTO> GetQuery()
		{
			return Manager.Database.Categories.Select(val => new CategoryDTO()
			{
				CategoryId = val.CategoryId,
				CategoryName = val.CategoryName,
				CategoryCode = val.CategoryCode
			});
		}

		public List<CategoryDTO> Transform()
		{
			return GetQuery().ToList();

		}

		public CategoryDTO TransformId(long id)
		{
			return GetQuery().FirstOrDefault(x => x.CategoryId == id);
		}
	}
}
