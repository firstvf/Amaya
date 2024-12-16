using Assets.Src.Scripts.Game;
using Assets.Src.Scripts.Pool;
using UnityEngine;

namespace Assets.Src.Scripts.Bootstrap
{
    public class BootstrapInstaller : MonoBehaviour
    {
        [SerializeField] private GameAudio _gameAudio;
        [SerializeField] private GameMenu _gameMenu;
        [SerializeField] private LevelTask _levelTask;
        [SerializeField] private SymbolSpawner _symbolSpawner;

        private void Awake()
        {
            InitBootstrap();
        }

        private void InitBootstrap()
        {
            _gameAudio = Instantiate(_gameAudio, transform);
            _gameMenu = Instantiate(_gameMenu, transform);
            _symbolSpawner = Instantiate(_symbolSpawner, transform);
            _levelTask = Instantiate(_levelTask, transform);

            _gameMenu.Construct(_levelTask);
            _symbolSpawner.Construct(_levelTask, _gameMenu, _gameAudio);
            _levelTask.Construct(_gameAudio, _gameMenu, _symbolSpawner);

            EnableComponents();
        }

        private void EnableComponents()
        {
            _gameAudio.gameObject.SetActive(true);
            _gameMenu.gameObject.SetActive(true);
            _symbolSpawner.gameObject.SetActive(true);
            _levelTask.gameObject.SetActive(true);
        }
    }
}