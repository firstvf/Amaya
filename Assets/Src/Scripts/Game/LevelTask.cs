using Assets.Src.Scripts.Pool;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Src.Scripts.Game
{
    public class LevelTask : MonoBehaviour
    {
        public readonly List<string> UsedTaskList = new List<string>();
        public Action OnCompleteLevelHandler { get; set; }

        private GameAudio _gameAudio;
        private GameMenu _gameMenu;
        private SymbolSpawner _symbolSpawner;
        private string _correctIdentifier;
        private int _level = 0;
        private int _quantity = 0;

        private void Start()
        {
            Invoke(nameof(StartLevel), 1.5f);
        }

        public void Construct(GameAudio gameAudio, GameMenu gameMenu, SymbolSpawner symbolSpawner)
        {
            _gameAudio = gameAudio;
            _gameMenu = gameMenu;
            _symbolSpawner = symbolSpawner;
        }

        public bool CheckCorrectAnswer(string identifier)
        => identifier.Equals(_correctIdentifier);

        public bool CheckUsedTask(string task)
        {
            foreach (var item in UsedTaskList)
                if (item.Equals(task))
                    return true;

            return false;
        }

        public void Win()
        {
            _gameAudio.PlayWinSound();
            _gameMenu.HideTask();
            _symbolSpawner.ClearLevel();

            _level++;
            _quantity += _symbolSpawner.GetCurrentBundle()
                .SymbolData.LevelUpAdditionalCellsCount;

            Invoke(nameof(StartSpawn), 2.5f);
        }

        public void SetTask(string identifier)
        {
            UsedTaskList.Add(identifier);
            _correctIdentifier = identifier;

            _gameMenu.SetTextTask(identifier);
        }

        private void StartLevel()
        {
            _quantity = _symbolSpawner.GetCurrentBundle()
                .SymbolData.LevelUpAdditionalCellsCount;

            _gameMenu.SetInputLimiter(true);
            _symbolSpawner.StartSpawn(_quantity);
        }

        private void StartSpawn()
        {
            if (_level < _symbolSpawner.GetCurrentBundle().SymbolData.LevelsCount)
                _symbolSpawner.StartSpawn(_quantity);
            else
                OnCompleteLevelHandler?.Invoke();
        }
    }
}