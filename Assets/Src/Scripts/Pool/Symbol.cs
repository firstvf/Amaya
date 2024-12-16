using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Src.Scripts.Pool
{
    public class Symbol : MonoBehaviour
    {
        [SerializeField] private Image _symbolImage;
        [SerializeField] private Image _symbolBackground;
        [SerializeField] private Button _button;
        private string _identifier;
        private bool _isRotate;
        private Tweener _wrongAnswerTweener;
        private SymbolSpawner _spawner;
        private bool _isSetup = false;

        private void Start()
        {
            _button.onClick.AddListener(CheckTask);
        }

        public void Construct(SymbolSpawner symbolSpawner)
        {
            _isSetup = true;
            _spawner = symbolSpawner;
        }

        public bool CheckSetup() => _isSetup;

        public void DisableSymbol()
        {
            if (!gameObject.activeInHierarchy)
                return;

            transform.DOScale(new Vector3(0.1f, 0.1f, 1), 0.25f)
                .SetEase(Ease.Linear)
                .OnStart(() => _spawner.GameAudio.PlayFadeSound())
                .OnComplete(() => gameObject.SetActive(false));
        }

        public void RotateImage()
        {
            _isRotate = true;
            _symbolImage.transform.DORotate(new Vector3(0, 0, -90), 0);
        }

        public void SetSymbol(Sprite symbol, string identifier, Color backgroundColor)
        {
            _symbolImage.sprite = symbol;
            _symbolBackground.color = backgroundColor;
            _identifier = identifier;

            transform.DOScale(new Vector3(10, 10, 1), 0.25f)
                .SetEase(Ease.OutBounce)
                .OnStart(() => _spawner.GameAudio.PlayInstantiateSound());

            if (_isRotate)
            {
                _isRotate = false;
                _symbolImage.transform.DORotate(new Vector3(0, 0, 0), 0);
            }
        }

        private void CheckTask()
        {
            if (_spawner.LevelTask.CheckCorrectAnswer(_identifier))
            {
                _spawner.LevelTask.Win();

                _symbolImage.transform.DOScale(new Vector3(1.5f, 1.5f, 1), 0.1f)
                    .SetLoops(2, LoopType.Yoyo)
                    .SetEase(Ease.Linear);

                _spawner.GameMenu
                    .UiParticles.UseStarParticles(transform.position);
            }
            else
            {
                if (_wrongAnswerTweener.IsActive())
                    _wrongAnswerTweener.Complete();

                _wrongAnswerTweener = _symbolImage.transform
                     .DOShakePosition(0.25f, new Vector2(2f, 0), randomness: 0,
                     randomnessMode: ShakeRandomnessMode.Harmonic)
                     .OnStart(() => _spawner.GameAudio.PlayWrongAnswerSound());
            }
        }
    }
}