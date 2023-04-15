using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen.NoiseStrategies
{
    interface NoiseStrategy
    {
        public void generateNoise(Vector3[] noiseLayer, float sensitivity);
    }
}
