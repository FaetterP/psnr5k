using UnityEngine;

namespace Assets.Scripts.Research
{
    public class BigObjectsSpawn : MonoBehaviour
    {
        private static BigObjectsSpawn s_instance;

        private GameObject _spawned;

        public static BigObjectsSpawn Instance
        {
            get
            {
                if (s_instance == null)
                {
                    s_instance = FindObjectOfType<BigObjectsSpawn>();
                }
                return s_instance;
            }
        }

        public void SpawnModel(GameObject model)
        {
            if (_spawned)
            {
                Destroy(_spawned);
            }

            _spawned = Instantiate(model, transform);
        }
    }
}
