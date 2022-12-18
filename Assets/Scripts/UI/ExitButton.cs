using UnityEngine;

namespace Assets.Scripts.UI
{
    class ExitButton : MonoBehaviour
    {
        private void OnMouseDown()
        {
            Application.Quit();
        }
    }
}
