using Repository;
using System.Collections.Generic;
using System.Linq;

namespace Core.Manager.ItemManager
{
	public class ItemQuery : AsistanceBase<ItemAdapter, Item>
	{
		public ItemQuery(ItemAdapter manager) : base(manager)
		{
			// do nothing
		}

		public IQueryable<Item> Get(bool withDetail = false)
		{
			return Manager.Database.Items;
		}

		public List<ItemDTO> Transform()
		{
			return (from val in Get()
					join category in Manager.Database.Categories
					on val.CategoryId equals category.CategoryId
					select new ItemDTO()
					{
						ItemId = val.ItemId,
						ItemName = val.ItemName,
						ItemCode = val.ItemCode,
						ItemPhoto = val.ItemPhoto,
						ItemQty = val.ItemQty,
						Note = val.Note,
						CategoryId = category.CategoryId,
						CategoryName = category.CategoryName,
					}).ToList();
		}

		public ItemDTO TransformId(long id)
		{
			return (from val in Get()
					join category in Manager.Database.Categories
					on val.CategoryId equals category.CategoryId
					where val.ItemId == id
					select new ItemDTO()
					{
						ItemId = val.ItemId,
						ItemName = val.ItemName,
						ItemPhoto = val.ItemPhoto,
						ItemCode = val.ItemCode,
						ItemQty = val.ItemQty,
						Note = val.Note,
						CategoryId = category.CategoryId,
						CategoryName = category.CategoryName,
					}).FirstOrDefault();
		}
	}
}
