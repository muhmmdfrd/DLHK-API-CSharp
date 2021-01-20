using Repository;
using System;
using System.Transactions;

namespace Core.Manager.CategoryManager
{
	public class CategoryUpdater : AsistanceBase<CategoryAdapter, Category>
	{
		public CategoryUpdater(CategoryAdapter manager) : base(manager)
		{
			// do nothing
		}

		public Category Update(CategoryDTO dto)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Database.Categories.Find(dto.CategoryId);

				if (exist == null)
					throw new Exception("data not found");

				exist.CategoryName = dto.CategoryName;

				Manager.Database.SaveChanges();

				transac.Complete();

				return exist;
			}
		}
	}
}
