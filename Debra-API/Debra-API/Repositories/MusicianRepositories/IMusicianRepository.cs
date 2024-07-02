using Debra_API.Entities;

namespace Debra_API.Repositories.MusicianRepositories
{
	public interface IMusicianRepository
	{
		bool Add(List<Musician> musicians);
	}
}
