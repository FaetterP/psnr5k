using UnityEngine;

namespace Assets.UI
{
    class ExitButton : MonoBehaviour
    {
        private void OnMouseDown()
        {
            Application.Quit();
        }
    }
}
