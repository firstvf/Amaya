using Assets.Src.Scripts.Bootstrap;
using Assets.Src.Scripts.Game;
using Assets.Src.Scripts.Pool;
using VContainer;
using VContainer.Unity;

public class GameLifetimeScope : LifetimeScope
{
    //[SerializeField] private GameMenu _gameMenu;
    //[SerializeField] private GameAudio _gameAudio;
    //[SerializeField] private SymbolSpawner _symbolSpawner;
    //[SerializeField] private LevelTask _levelTask;    

    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<GameWorld>();

        builder.UseEntryPoints(points =>
        {
            points.Add<GameWorld>();
        });

        RegisterBuilder(builder);
    }

    private void RegisterBuilder(IContainerBuilder builder)
    {
        builder.Register<GameAudio>(Lifetime.Singleton).WithParameter(1);            
        builder.Register<SymbolSpawner>(Lifetime.Singleton).WithParameter(2);
        builder.Register<GameMenu>(Lifetime.Singleton).WithParameter(3);            
        builder.Register<LevelTask>(Lifetime.Singleton).WithParameter(4);

        //builder.RegisterComponent(_gameAudio);
        //builder.RegisterComponent(_symbolSpawner);
        //builder.RegisterComponent(_gameMenu);
        //builder.RegisterComponent(_levelTask);
    }
}
