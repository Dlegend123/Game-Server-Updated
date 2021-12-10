using System.ComponentModel.DataAnnotations;

namespace GameServer
{
    public class NewItem
    {
		public DateTime CreationDate { get; set; }
		public Guid Id { get; set; }
		public string Name { get; set; }
		public int price { get; set; }
		[Range(1, 99)]
		public int ItemLevel { get; set; }
		[EnumDataType(typeof(ItemType))]
		public ItemType Type { get; set; }
		public Guid OwnerId { get; set; }
	}
}
