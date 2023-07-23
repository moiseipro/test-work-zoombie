using Enemy;
using GameStates;
using LevelManagement;
using Player;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Weapon;

public class GameLifetimeScope : LifetimeScope
{
    private GameInput _gameInput;
    private GameStateChanger _gameStateChanger;
    [SerializeField] private GameData _gameData;
    [SerializeField] private EnemyAI _enemyAIPrefab;

    protected override void Configure(IContainerBuilder builder)
    {
        _gameInput = new GameInput();
        _gameInput.Enable();
        _gameStateChanger = new GameStateChanger();

        builder.RegisterInstance(_gameInput);
        builder.Register(constructor =>
        {
            constructor.Inject(_gameData);
            return _gameData;
        }, Lifetime.Singleton);
        builder.RegisterInstance(_gameStateChanger);

        builder.RegisterFactory<Transform, EnemyAI>(container =>
        {
            return parentTransform => container.Instantiate(_enemyAIPrefab, parentTransform);
        }, Lifetime.Scoped);
    }
}
