using Assets.Src.Scripts.Data;
using Assets.Src.Scripts.Game;
using System.Collections;
using UnityEngine;

namespace Assets.Src.Scripts.Pool
{
    public class SymbolSpawner : MonoBehaviour
    {
        public LevelTask LevelTask { get; private set; }
        public GameAudio GameAudio { get; private set; }
        public GameMenu GameMenu { get; private set; }

        [SerializeField] private SymbolBundle[] _symbolBundle;
        [SerializeField] private Color32[] _colors;
        [SerializeField] private Symbol _symbol;
        private Transform _container;
        private ObjectPooler<Symbol> _pooler;
        private readonly string[] _rotateSymbols = { "7", "8" };
        private readonly WaitForSeconds _spawnDelay = new WaitForSeconds(0.1f);
        private int _bundleId;

        private void Start()
        {
            _container = GameMenu.GameBoard;
            _pooler = new ObjectPooler<Symbol>(_symbol, _container, 3);
            _bundleId = Random.Range(0, _symbolBundle.Length);

            foreach (var symbol in _pooler.GetList())
                symbol.Construct(this);
        }

        public void Construct(LevelTask levelTask, GameMenu gameMenu, GameAudio gameAudio)
        {
            LevelTask = levelTask;
            GameMenu = gameMenu;
            GameAudio = gameAudio;
        }

        public void ClearLevel()
        => StartCoroutine(DisableSymbols());

        public SymbolBundle GetCurrentBundle()
        => _symbolBundle[_bundleId];

        public void StartSpawn(int quantity)
        => StartCoroutine(SpawnSymbol(quantity));

        private IEnumerator DisableSymbols()
        {
            var symbolsList = _pooler.GetList();

            for (int i = symbolsList.Count - 1; i >= 0; i--)
            {
                symbolsList[i].DisableSymbol();
                yield return _spawnDelay;
            }
        }

        private IEnumerator SpawnSymbol(int quantity)
        {
            if (quantity > _symbolBundle[_bundleId].SymbolData.IdentifierArray.Length)
                throw new System.Exception("Bundle less than symbols quantity");

            int[] usedId = new int[quantity];

            for (int k = 0; k < usedId.Length; k++)
                usedId[k] = -1;

            for (int i = 0; i < quantity; i++)
            {
                int symbolId = UnusedIdentifier(usedId, _bundleId);

                usedId[i] = symbolId;

                int colorId = Random.Range(0, _colors.Length);
                var symbol = _pooler.GetFreeObjectFromPool();

                if (symbol.CheckSetup() == false)
                    symbol.Construct(this);

                symbol.SetSymbol(
                    _symbolBundle[_bundleId].SymbolData.SpriteArray[symbolId],
                    _symbolBundle[_bundleId].SymbolData.IdentifierArray[symbolId],
                    _colors[colorId]);

                for (int j = 0; j < _rotateSymbols.Length; j++)
                    if (_symbolBundle[_bundleId].SymbolData.IdentifierArray[symbolId].Equals(_rotateSymbols[j]))
                        symbol.RotateImage();

                yield return _spawnDelay;
            }

            SendTask(quantity, _bundleId, usedId);
        }

        private void SendTask(int quantity, int bundleId, int[] usedId)
        {
            int randomId = Random.Range(0, quantity);

            while (LevelTask.CheckUsedTask
                (_symbolBundle[bundleId].SymbolData.IdentifierArray[usedId[randomId]]))
            {
                randomId = Random.Range(0, quantity);
            }

            LevelTask.SetTask(_symbolBundle[bundleId].SymbolData.IdentifierArray[usedId[randomId]]);
        }

        private int UnusedIdentifier(int[] usedId, int bundleId)
        {
            int symbolId = Random.Range(0, _symbolBundle[bundleId].SymbolData.IdentifierArray.Length);

            bool isArrayContainUsedId = true;

            while (isArrayContainUsedId == true)
            {
                isArrayContainUsedId = false;

                for (int i = 0; i < usedId.Length; i++)
                    if (symbolId == usedId[i])
                    {
                        isArrayContainUsedId = true;
                        symbolId = Random.Range(0, _symbolBundle[bundleId].SymbolData.IdentifierArray.Length);
                        break;
                    }
            }

            return symbolId;
        }
    }
}