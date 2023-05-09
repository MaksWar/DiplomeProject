using Additions.Extensions;
using Infrastructure.States;
using Zenject;

namespace Infrastructure
{
	public class GameBootstrapper : MonoSingleton<GameBootstrapper>, ICoroutineRunner
	{
		private IGameStateMachine _stateMachine;

		[Inject]
		private void Construct(IGameStateMachine stateMachine) =>
			_stateMachine = stateMachine;

		private void Start()
		{
			_stateMachine.Enter<BootstrapState>();

			DontDestroyOnLoad(this);
		}
	}
}