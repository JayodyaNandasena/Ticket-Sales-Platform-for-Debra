using Debra_API.Data;
using Debra_API.Entities;

namespace Debra_API.Repositories.BandRepository
{
	public class BandRepository : IBandRepository
	{
		private AppDBContext _dbContext;

		public BandRepository(AppDBContext dBContext)
		{
			_dbContext = dBContext;
		}

		public bool Add(List<Band> bands)
		{
            foreach (var band in bands)
            {
				_dbContext.Bands.Add(band);

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
