using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Src.Scripts.Game
{
    public class LoadingWindow : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Text _text;
        private Image _loadingScreen;

        private void Awake()
        {
            _loadingScreen = GetComponent<Image>();
        }

        private void OnEnable()
        {
            ShowLoadingWindow();
        }

        public void ShowLoadingWindow()
        {
            _loadingScreen.DOFade(0.9f, 2f).SetEase(Ease.Linear)
                .OnComplete(() => Load());
        }

        public void Load()
        {
            _slider.gameObject.SetActive(true);
            _text.gameObject.SetActive(true);

            _slider.DOValue(1f, 2.5f).SetEase(Ease.InSine)
                .OnComplete(() => Restart());
        }

        private void Restart()
        => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}