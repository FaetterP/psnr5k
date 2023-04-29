using UnityEngine;

namespace Assets.Scripts.Screens.MainScreen.Bulges
{
    class BulgeSin : IBulge
    {
        public void GenerateBulge(float[] vector)
        {
            float h = Mathf.PI / (vector.Length - 1);
            for (int i = 0; i < vector.Length; i++)
            {
                vector[i] =  Mathf.Sin(h * i);
            }
        }
    }
}
