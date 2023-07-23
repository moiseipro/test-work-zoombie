using System;

namespace GameStates
{
    public class GameStateChanger
    {
        private GameState _gameState;

        public Action<GameState> OnGameStateChanged;


        public void ChangeWave()
        {
            OnGameStateChanged?.Invoke(GameState.NextWave);
            StartGame();
        }
        
        public void StartGame(){
            OnGameStateChanged?.Invoke(GameState.Play);
        }
        
        public void GameOver()
        {
            OnGameStateChanged?.Invoke(GameState.Lose);
        }

        public void GameWin()
        {
            OnGameStateChanged?.Invoke(GameState.Win);
        }
    }
}