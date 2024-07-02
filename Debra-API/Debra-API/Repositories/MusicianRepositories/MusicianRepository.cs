using Debra_API.Data;
using Debra_API.Entities;

namespace Debra_API.Repositories.MusicianRepositories
{
	public class MusicianRepository : IMusicianRepository
	{
		private AppDBContext _dbContext;

		public MusicianRepository(AppDBContext dBContext)
		{
			_dbContext = dBContext;
		}

		public bool Add(List<Musician> musicians)
		{
			foreach (var musician in musicians)
			{
				_dbContext.Musicians.Add(musician);

				if (!Save())
				{
					return false;
				}
			}

			return true;
		}

		private bool Save()
		{
			return _dbContext.SaveChanges() > 0;
		}
	}
}
