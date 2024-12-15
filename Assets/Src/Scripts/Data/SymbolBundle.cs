using UnityEngine;

namespace Assets.Src.Scripts.Data
{
    [CreateAssetMenu(fileName = "new SymbolData", menuName = "Symbol Bundle Data")]
    public class SymbolBundle : ScriptableObject
    {
        [field: SerializeField]
        public SymbolData SymbolData { get; private set; }
    }
}