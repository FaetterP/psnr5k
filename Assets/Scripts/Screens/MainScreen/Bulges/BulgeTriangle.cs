namespace Assets.Scripts.Screens.MainScreen.Bulges
{
    class BulgeTriangle : IBulge
    {
        public void GenerateBulge(float[] vector)
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
