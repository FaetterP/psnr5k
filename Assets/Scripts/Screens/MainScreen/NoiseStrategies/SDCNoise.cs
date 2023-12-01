using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen.NoiseStrategies
{
    class SDCNoise : INoiseStrategy
    {
        private static System.Random rnd = new System.Random();

        public void generateNoise(Vector3[] noiseLayer, float sensitivity)
        {
            for (int i = 0; i < noiseLayer.Length; i++)
            {
                float stable = 0.7f * sensitivity;
                float random = 0.3f * sensitivity * (float)rnd.NextDouble();
                float noiseValue = stable + random;

                Vector3 currentPoint = noiseLayer[i];
                currentPoint.y = noiseValue;
                noiseLayer[i] = currentPoint;
            }
        }
    }
}
