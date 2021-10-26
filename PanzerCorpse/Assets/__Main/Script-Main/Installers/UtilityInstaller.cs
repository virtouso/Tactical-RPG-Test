using UnityEngine;
using Zenject;

public class UtilityInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IUtilityAssetLoader>().To<UtilityAddressables>().FromNew().AsSingle();
        Container.Bind<IUtilityFile>().To<UtilitySimplePLayerPrefs>().FromNew().AsSingle();
        Container.Bind<IUtilitySerializer>().To<UtilityNewtonSoftJson>().FromNew().AsSingle();

    }
}