using Infrastructure.Data;

namespace Infrastructure.Services.SaveLoad
{
	public interface ISaveLoadService : IService
	{
		void SaveProgress();
		void SaveLocal();
		PlayerProgress LoadProgress();
	}
}