using UnityEngine;

namespace Assets.Src.Scripts.Game
{
    public class UiParticles : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _starParticles;        

        public void UseStarParticles(Vector2 position)
        {
            _starParticles.transform.position = position;
            _starParticles.Play();
        }
    }
}