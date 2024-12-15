using Assets.Src.Scripts.Game;
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

        private void Start()
        {
            _button.onClick.AddListener(CheckTask);
        }

        public void DisableSymbol()
        {
            if (!gameObject.activeInHierarchy)
                return;

            transform.DOScale(new Vector3(0.1f, 0.1f, 1), 0.25f)
                .SetEase(Ease.Linear)
                .OnStart(() => GameAudio.Instance.PlayFadeSound())
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
                .OnStart(() => GameAudio.Instance.PlayInstantiateSound());

            if (_isRotate)
            {
                _isRotate = false;
                _symbolImage.transform.DORotate(new Vector3(0, 0, 0), 0);
            }
        }

        private void CheckTask()
        {
            if (LevelTask.Instance.CheckCorrectAnswer(_identifier))
            {
                LevelTask.Instance.Win();

                _symbolImage.transform.DOScale(new Vector3(1.5f, 1.5f, 1), 0.1f)
                    .SetLoops(2,LoopType.Yoyo)
                    .SetEase(Ease.Linear);

                GameParticles.Instance.UseStarParticles(transform.position);
            }
            else
                _symbolImage.transform
                    .DOShakePosition(0.25f, new Vector2(2f, 0), randomness: 0, randomnessMode: ShakeRandomnessMode.Harmonic)
                    .OnStart(() => GameAudio.Instance.PlayWrongAnswerSound());
        }
    }
}