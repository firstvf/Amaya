using UnityEngine;

namespace Assets.Src.Scripts.Game
{
    public class GameAudio : MonoBehaviour
    {
        public static GameAudio Instance { get; private set; }

        [SerializeField] 
        private AudioClip _instantiateSound, _winSound, _fadeSound, _wrongAnswerSound;
        private AudioSource _audioSource;

        private void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);

            Instance = this;
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayInstantiateSound() => _audioSource.PlayOneShot(_instantiateSound, 0.1f);
        public void PlayWinSound() => _audioSource.PlayOneShot(_winSound, 0.1f);
        public void PlayFadeSound() => _audioSource.PlayOneShot(_fadeSound, 0.1f);
        public void PlayWrongAnswerSound() => _audioSource.PlayOneShot(_wrongAnswerSound, 0.1f);
    }
}