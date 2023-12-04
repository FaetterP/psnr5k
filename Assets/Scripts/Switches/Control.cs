namespace Assets.Scripts.Switches
{
    class Control : HandleStep
    {
        public enum EStatus
        {
            Voltage24 = 0, Voltage6, APCh, Currency1, Currency2, M, Epsilon
        }

        public EStatus Status => (EStatus)Value;
    }
}
