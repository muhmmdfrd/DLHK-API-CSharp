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
			var db = Manager.Database;
			var transac = db.Transacs;

			return (from val in Get()
					join category in db.Categories
					on val.CategoryId equals category.CategoryId
					orderby val.CategoryId
					select new ItemDTO()
					{
						ItemId = val.ItemId,
						ItemName = val.ItemName,
						ItemCode = val.ItemCode,
						ItemPhoto = val.ItemPhoto,
						ItemQty = val.ItemQty,
						Note = val.Note,
						In = (from tr in db.Transacs
							  where tr.TypeOfTransac.Equals("IN") &&
							  tr.ItemName.Equals(val.ItemName)
							  select tr.Qty).Sum(),
						Out = (from tr in db.Transacs
							   where tr.TypeOfTransac.Equals("OUT") &&
							   tr.ItemName.Equals(val.ItemName)
							   select tr.Qty).Sum(),
						FirstQty = (from tr in db.Transacs
									orderby tr.DateTransac ascending
									select tr).FirstOrDefault(x => x.ItemName.Equals(val.ItemName)).Qty,
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