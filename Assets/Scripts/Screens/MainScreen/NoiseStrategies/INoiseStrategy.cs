using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen.NoiseStrategies
{
    interface INoiseStrategy
    {
        public void generateNoise(Vector3[] noiseLayer, float sensitivity);
    }
}
