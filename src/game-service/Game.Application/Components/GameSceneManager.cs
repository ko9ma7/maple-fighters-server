using Common.ComponentModel;
using Common.Components;

namespace Game.Application.Components
{
    [ComponentSettings(ExposedState.Unexposable)]
    public class GameSceneManager : ComponentBase
    {
        private IGameSceneCollection gameSceneCollection;

        protected override void OnAwake()
        {
            var idGenerator = Components.Get<IIdGenerator>();

            gameSceneCollection = Components.Get<IGameSceneCollection>();
            gameSceneCollection.AddScene(Map.Lobby, new LobbyGameScene(idGenerator));
            gameSceneCollection.AddScene(Map.TheDarkForest, new TheDarkForestGameScene(idGenerator));
        }

        protected override void OnRemoved()
        {
            // TODO: Dispose scenes

            gameSceneCollection.RemoveScene(Map.Lobby);
            gameSceneCollection.RemoveScene(Map.TheDarkForest);
        }
    }
}