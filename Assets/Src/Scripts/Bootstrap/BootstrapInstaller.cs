using Assets.Src.Scripts.Game;
using Assets.Src.Scripts.Pool;
using UnityEngine;

namespace Assets.Src.Scripts.Bootstrap
{
    public class BootstrapInstaller :MonoBehaviour
    {
        public static BootstrapInstaller Instance { get; private set; }

        [field: SerializeField] public GameAudio GameAudio { get; private set; }        
        [field: SerializeField] public GameMenu GameMenu { get; private set; }             
        [field: SerializeField] public LevelTask LevelTask { get; private set; }     
        [field: SerializeField] public SymbolSpawner SymbolSpawner { get; private set; }

        private void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);

            Instance = this;

            GameAudio = Instantiate(GameAudio);
            GameMenu = Instantiate(GameMenu);
            SymbolSpawner = Instantiate(SymbolSpawner);
            LevelTask = Instantiate(LevelTask);
        }
    }
}