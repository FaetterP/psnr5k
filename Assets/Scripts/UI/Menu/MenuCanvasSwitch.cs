using UnityEngine;

namespace Assets.Scripts.UI.Menu
{
    public class MenuCanvasSwitch : CanvasSwitch
    {
        public enum CanvasName { Main = 0, Settings, Info }

        public static MenuCanvasSwitch Instance;

        [SerializeField] private CanvasName _startCanvas;

        public void EnableCanvas(CanvasName name)
        {
            EnableCanvas((int)name);
        }

        protected override int GetStartIndex()
        {
            return (int)_startCanvas;
        }

        private void Awake()
        {
            Instance = this;
        }
    }
}
