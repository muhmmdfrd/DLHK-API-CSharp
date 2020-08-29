using Repository;
using System.Transactions;

namespace Core.Manager.CategoryManager
{
	public class CategoryCreator : AsistanceBase<CategoryAdapter, Category>
	{
		public CategoryCreator(CategoryAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Category Save(CategoryDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var newEntity = new Category
				{
					CategoryName = dto.CategoryName,
				};

				Manager.Database.Categories.Add(newEntity);
				Manager.Database.SaveChanges();

				transac.Complete();

				return newEntity;
			}
		}
	}
}
