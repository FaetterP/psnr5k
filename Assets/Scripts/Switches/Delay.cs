namespace Assets.Scripts.Switches
{
    class Delay : HandleStep
    {
        public enum EStatus
        {
            D0 = 0, D5, D10, K
        }

        public EStatus Status => (EStatus)Value;
    }
}
