using Assets.Src.Scripts.Bootstrap;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Src.Scripts.Game
{
    public class LevelTask : MonoBehaviour
    {
        public readonly List<string> UsedTaskList = new List<string>();
        public Action OnCompleteLevelHandler { get; set; }

        private string _correctIdentifier;
        private int _level = 0;
        private int _quantity = 0;

        private void Start()
        {
            Invoke(nameof(StartLevel), 2f);
        }

        private void StartLevel()
        {
            _quantity = BootstrapInstaller.Instance.SymbolSpawner.GetCurrentBundle()
                .SymbolData.LevelUpAdditionalCellsCount;

            BootstrapInstaller.Instance.GameMenu.SetInputLimiter(true);
            BootstrapInstaller.Instance.SymbolSpawner.StartSpawn(_quantity);
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
            BootstrapInstaller.Instance.GameAudio.PlayWinSound();
            BootstrapInstaller.Instance.GameMenu.HideTask();
            BootstrapInstaller.Instance.SymbolSpawner.ClearLevel();

            _level++;
            _quantity += BootstrapInstaller.Instance.SymbolSpawner.GetCurrentBundle()
                .SymbolData.LevelUpAdditionalCellsCount;

            Invoke(nameof(StartSpawn), 3f);
        }

        public void SetTask(string identifier)
        {
            UsedTaskList.Add(identifier);
            _correctIdentifier = identifier;

            BootstrapInstaller.Instance.GameMenu.SetTextTask(identifier);
        }

        private void StartSpawn()
        {
            if (_level < BootstrapInstaller.Instance.SymbolSpawner.GetCurrentBundle().SymbolData.LevelsCount)
                BootstrapInstaller.Instance.SymbolSpawner.StartSpawn(_quantity);
            else
                OnCompleteLevelHandler?.Invoke();
        }
    }
}