using System.ComponentModel.DataAnnotations;

namespace Debra_API.DTOs.EventDTOs
{
	public class BandReadDTO
	{
		public int Id { get; set; }
		public string Name { get; set; } = null!;
		public string Image { get; set; }
	}
}
