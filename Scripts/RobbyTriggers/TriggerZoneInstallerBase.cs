using Zenject;

namespace KotletaGames.RobbyTriggersModule
{
    public abstract class TriggerZoneInstallerBase : MonoInstaller
    {
        public override void InstallBindings()
        {

        }

        protected void BindPlayerMarker(PlayerTriggerZoneMarker marker)
        {
            Container
                .BindInstance(marker)
                .AsCached();
        }
    }
}