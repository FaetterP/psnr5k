namespace Assets.Scripts.Screens.MainScreen.Bulges
{
    abstract class Bulge
    {
        public readonly float Width;
        public readonly float MaxAmplitude;
        public readonly float Range;
        public readonly float Azimuth;

        public Bulge(float width, float maxAmplitude, float range, float azimuth)
        {
            Width = width;
            MaxAmplitude = maxAmplitude;
            Range = range;
            Azimuth = azimuth;
        }

        public abstract void GenerateBulge(float[] vector);
    }
}
