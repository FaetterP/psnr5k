using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen.NoiseStrategies
{
    class SDCNoise : NoiseStrategy
    {
        private static System.Random rnd = new System.Random();

        public void generateNoise(Vector3[] noiseLayer, float sensitivity)
        {
            float leftValue = 0.8f * sensitivity;
            float rightValue = 1 * sensitivity;

            int countNodes = noiseLayer.Length - 1;
            for (int i = 0; i <= countNodes; i++)
            {
                float noiseValue = (float)rnd.NextDouble() * (rightValue - leftValue) + leftValue;

                Vector3 currentPoint = noiseLayer[i];
                currentPoint.y = noiseValue;
                noiseLayer[i] = currentPoint;
            }
        }
    }
}
