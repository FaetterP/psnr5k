using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen.Splashes
{
    class SplashSin : Splash
    {
        public SplashSin(float size, float maxAmplitude, float range, float azimuth, Transform leftLine, AudioClip sound, AudioSource audioSource) : base(size, maxAmplitude, range, azimuth, leftLine, sound, audioSource) { }

        public override void GenerateSplash(float[] vector)
        {
            float h = Mathf.PI / (vector.Length - 1);
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] = Mathf.Sin(h * i);
            }
        }
    }
}
