using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen.NoiseStrategies
{
    class StateNoise : INoiseStrategy
    {
        private static System.Random rnd = new System.Random();

        public void generateNoise(Vector3[] noiseLayer, float sensitivity)
        {
            int countNodes = noiseLayer.Length;

            for (int i = 0; i < countNodes; i += 3)
            {
                if (rnd.NextDouble() < 0.5) continue;

                float noiseValue = ((float)rnd.NextDouble() * 1.1f - 0.1f) * sensitivity;

                Vector3 currentPoint = noiseLayer[i];
                currentPoint.y = noiseValue;
                noiseLayer[i] = currentPoint;
            }
            for (int i = 1; i < countNodes; i += 3)
            {
                float noiseValue = ((float)rnd.NextDouble() * 0.2f - 0.2f) * sensitivity;

                Vector3 currentPoint = noiseLayer[i];
                currentPoint.y = noiseValue;
                noiseLayer[i] = currentPoint;
            }
        }
    }
}
