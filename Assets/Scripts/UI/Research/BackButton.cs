using Assets.Scripts.Research;
using Assets.Scripts.Utilities;
using UnityEngine;

namespace Assets.Scripts.UI.Research
{
    public class BackButton : MonoBehaviour
    {
        private void OnMouseDown()
        {
            MessageScreen.Instance.Disable();
            if (CameraWithPositions.Instance.GoBack() == false)
            {
                Loading.Instance.Load(Scenes.Menu);
            }
        }
    }
}
