using Repository;
using System;
using System.Transactions;

namespace Core.Manager.CategoryManager
{
	public class CategoryDeleter : AsistanceBase<CategoryAdapter, Category>
	{
		public CategoryDeleter(CategoryAdapter manager) : base(manager)
		{
			// do nothing
		}

		public void Delete(long id)
		{
			using (var transac = new TransactionScope())
			{
				var exist = Manager.Database.Categories.Find(id);

				if (exist == null)
					throw new Exception("data not found");

				Manager.Database.Categories.Remove(exist);
				Manager.Database.SaveChanges();

				transac.Complete();
			}
		}
	}
}
