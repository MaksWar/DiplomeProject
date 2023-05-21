using Infrastructure.Data;

namespace Infrastructure.Services.DBSyncService
{
	public interface IDatabaseSyncService
	{
		void RegisterPlayer(string uniqueId);

		void SendData(MinigameProgress progress);

		void SyncData();
	}
}