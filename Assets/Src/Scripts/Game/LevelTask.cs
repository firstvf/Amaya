using Assets.Src.Scripts.Pool;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Src.Scripts.Game
{
    public class LevelTask : MonoBehaviour
    {
        public static LevelTask Instance { get; private set; }
        public readonly List<string> UsedTaskList = new List<string>();
        public Action OnCompleteLevelHandler { get; set; }

        private string _correctIdentifier;
        private int _level = 0;
        private int _quantity = 0;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            Invoke(nameof(StartLevel), 2f);
        }

        private void StartLevel()
        {
            _quantity = SymbolSpawner.Instance.GetCurrentBundle()
                .SymbolData.LevelUpAdditionalCellsCount;

            GameMenu.Instance.SetInputLimiter(true);
            SymbolSpawner.Instance.StartSpawn(_quantity);
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
            GameAudio.Instance.PlayWinSound();
            GameMenu.Instance.HideTask();
            SymbolSpawner.Instance.ClearLevel();

            _level++;
            _quantity += SymbolSpawner.Instance.GetCurrentBundle()
                .SymbolData.LevelUpAdditionalCellsCount;

            Invoke(nameof(StartSpawn), 3f);
        }

        public void SetTask(string identifier)
        {
            UsedTaskList.Add(identifier);
            _correctIdentifier = identifier;

            GameMenu.Instance.SetTextTask(identifier);
        }

        private void StartSpawn()
        {
            if (_level < SymbolSpawner.Instance.GetCurrentBundle().SymbolData.LevelsCount)
                SymbolSpawner.Instance.StartSpawn(_quantity);
            else
                OnCompleteLevelHandler?.Invoke();
        }
    }
}