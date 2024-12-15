using System;
using UnityEngine;

namespace Assets.Src.Scripts.Data
{
    [Serializable]
    public class SymbolData
    {
        [field: SerializeField] public Sprite[] SpriteArray { get; private set; }
        [field: SerializeField] public string[] IdentifierArray { get; private set; }
        [field: SerializeField] public int ColumnsCount { get; private set; }
        [field: SerializeField] public int LevelUpAdditionalCellsCount { get; private set; }
        [field: SerializeField] public int LevelsCount { get; private set; }
    }
}