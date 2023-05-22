using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen.Bulges
{
    class BulgeTriangle : Bulge
    {
        public BulgeTriangle(float size, float maxAmplitude, float range, float azimuth, Transform leftLine, AudioClip sound, AudioSource audioSource) : base(size, maxAmplitude, range, azimuth, leftLine, sound, audioSource) { }


        public override void GenerateBulge(float[] vector)
        {
            int half = (vector.Length + 1) / 2;
            for (int i = 0; i < half; i++)
            {
                vector[i] = 2f * i / vector.Length;
            }

            for (int i = half; i < vector.Length; i++)
            {
                vector[i] = -2f * i / vector.Length + 2;
            }
        }
    }
}
