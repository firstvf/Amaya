using UnityEngine;

namespace Assets.Src.Scripts.Game
{
    public class GameParticles : MonoBehaviour
    {
        public static GameParticles Instance { get; private set; }

        [SerializeField] private ParticleSystem _particleSystem;

        private void Awake()
        {
            if (Instance != null)
                Destroy(gameObject);

            Instance = this;
        }

        public void UseStarParticles(Vector2 position)
        {
            _particleSystem.transform.position = position;
            _particleSystem.Play();
        }
    }
}