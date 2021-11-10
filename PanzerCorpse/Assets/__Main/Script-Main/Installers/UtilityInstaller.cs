using UnityEngine;
using Zenject;

public class UtilityInstaller : MonoInstaller
{
    [SerializeField] private LoggerChannels _loggerChannels;
    
    public override void InstallBindings()
    {
        Container.Bind<ILogger>().To<Logger>().FromInstance(new Logger(_loggerChannels)).AsSingle();
        Container.Bind<IUtilityAssetLoader>().To<UtilityAddressables>().FromNew().AsSingle();
        Container.Bind<IUtilityFile>().To<UtilitySimplePLayerPrefs>().FromNew().AsSingle();
        Container.Bind<IUtilitySerializer>().To<UtilityNewtonSoftJson>().FromNew().AsSingle();
        Container.Bind<IUtilityMatchQueries>().To<UtilityMatchQueries>().FromNew().AsSingle();
        Container.Bind<IUtilityMatchGeneral>().To<UtilityMatchGeneral>().FromNew().AsSingle();

    }
}