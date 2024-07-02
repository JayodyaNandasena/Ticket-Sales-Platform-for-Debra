using Debra_API.Entities;

namespace Debra_API.Repositories.BandRepository
{
	public interface IBandRepository
	{
		bool Add(List<Band> bands);
	}
}
