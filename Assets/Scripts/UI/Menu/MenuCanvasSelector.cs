using UnityEngine;

namespace Assets.Scripts.UI.Menu
{
    public class MenuCanvasSelector : MonoBehaviour
    {
        [SerializeField] private MenuCanvasSwitch.CanvasName canvas;

        public void Switch()
        {
            MenuCanvasSwitch.Instance.EnableCanvas(canvas);
        }
    }
}
