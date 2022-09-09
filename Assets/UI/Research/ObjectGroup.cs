using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.UI.Research
{
    class ObjectGroup : MonoBehaviour
    {
        [SerializeField] private List<ObjectWithMessage> _objects;

        private void Awake()
        {
            //_objects = FindObjectsOfType<ObjectWithMessage>();
        }

        public void ActivateDetail(ObjectWithMessage detail)
        {
            foreach(var t in _objects)
            {
                if(t!=detail)
                    t.Deactivate();
            }
        }

        public void AddElement(ObjectWithMessage obj)
        {
            _objects.Add(obj);
        }
    }
}
