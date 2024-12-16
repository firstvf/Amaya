using DG.Tweening;
using UnityEngine;
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
        [SerializeField] private LoadingWindow _loadingWindow;
        private GridLayoutGroup _gridLayoutGroup;
        private LevelTask _levelTask;

        private void Awake()
        {
            _gridLayoutGroup = GameBoard.GetComponent<GridLayoutGroup>();
        }

        private void Start()
        {
            _restartButton.onClick.AddListener(ShowLoadingWindow);
            _levelTask.OnCompleteLevelHandler += ShowMenu;            
            _restartButton.gameObject.SetActive(false);
        }

        public void Construct(LevelTask levelTask)
        {
            _levelTask = levelTask;
        }

        public void SetLayoutConstrainCount(int count)
        => _gridLayoutGroup.constraintCount = count;

        public void SetInputLimiter(bool isAble)
        => _inputLimiter.gameObject.SetActive(isAble);

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

        private void ShowMenu()
        {
            _endLevelUI.gameObject.SetActive(true);
            _endLevelUI.DOFade(0.5f, 0.5f)
                .OnComplete(() => _restartButton.gameObject.SetActive(true));
        }

        public void ShowLoadingWindow()
        {
            _loadingWindow.gameObject.SetActive(true);
            _endLevelUI.gameObject.SetActive(false);
        }
    }
}