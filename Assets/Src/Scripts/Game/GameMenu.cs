using Assets.Src.Scripts.Bootstrap;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Src.Scripts.Game
{
    public class GameMenu : MonoBehaviour
    {
        [field: SerializeField] public Transform GameBoard { get; private set; }
        [field: SerializeField] public UiParticles UiParticles { get; private set; }
        [SerializeField] private Image _endLevelUI;
        [SerializeField] private Button _restartButton;
        [SerializeField] private Text _taskText;
        [SerializeField] private Image _inputLimiter;
        private GridLayoutGroup _gridLayoutGroup;

        private void Awake()
        {
            _gridLayoutGroup = GameBoard.GetComponent<GridLayoutGroup>();
        }

        private void Start()
        {
            _restartButton.onClick.AddListener(Restart);
            BootstrapInstaller.Instance.LevelTask.OnCompleteLevelHandler += ShowMenu;
            _restartButton.gameObject.SetActive(false);

            _gridLayoutGroup.constraintCount = BootstrapInstaller.Instance.SymbolSpawner
                .GetCurrentBundle().SymbolData.ColumnsCount;
        }

        public void HideTask()
        {
            _inputLimiter.gameObject.SetActive(true);
            _taskText.DOFade(0, 0.25f);
        }

        public void SetTextTask(string task)
        {
            _taskText.text = "Find " + task;

            _taskText.DOFade(1, 0.25f)
                .OnComplete(() => _inputLimiter.gameObject.SetActive(false));
        }

        public void SetInputLimiter(bool isAble)
        => _inputLimiter.gameObject.SetActive(isAble);

        private void ShowMenu()
        {
            _endLevelUI.gameObject.SetActive(true);
            _endLevelUI.DOFade(0.5f, 0.5f)
                .OnComplete(() => _restartButton.gameObject.SetActive(true));
        }

        public void Restart()
        => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}