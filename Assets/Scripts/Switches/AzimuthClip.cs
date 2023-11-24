namespace Assets.Scripts.Switches
{
    class AzimuthClip : Lever
    {
        public void Disclip()
        {
            if (_isPressed == false)
                return;

            _isPressed = false;
            _center.transform.localEulerAngles = _angles[_isPressed];
            _thisAudioSource.PlayOneShot(_audioClick);

            e_onValueChanged.Invoke();
        }
    }
}
