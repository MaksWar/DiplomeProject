using System.Collections.Generic;

namespace Infrastructure.Services.DataAnalitycsService
{
	public interface IAnalyticsService
	{
		int LastMark { get; }

		GradationOfMark AnaliseData(List<MinigameAnalyticData> data);
	}
}