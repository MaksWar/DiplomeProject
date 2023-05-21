using Infrastructure.Services.DBSyncService;
using Infrastructure.Services.PersistentProgress;
using Infrastructure.Services.SaveLoad;
using MainMenu;

namespace Infrastructure.Services
{
	public interface IMinigameDataAnalyticsSaver
	{
		public void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService, IDatabaseSyncService databaseSyncService);

		public void Save(Category category, float timeComplete, int mistakesCount, int mark);
	}
}