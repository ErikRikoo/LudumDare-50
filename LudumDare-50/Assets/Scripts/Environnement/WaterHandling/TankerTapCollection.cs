using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Environnement
{
    [CreateAssetMenu(fileName = "TankerTapCollection", menuName = "Water/Tanker Tap Collection", order = 0)]
    public class TankerTapCollection : ScriptableObject, IEnumerable<TankerTap>
    {
        [SerializeField]
        private List<TankerTap> m_Elements = new();
        
        void Reset()
        {
            Debug.Log("TankerTapCollection cleared");
            m_Elements.Clear();
        }

        public void AddElement(TankerTap _tap)
        {
            m_Elements.Add(_tap);
        }
        
        public void RemoveElement(TankerTap _item)
        {
            m_Elements.Remove(_item);
        }

        public IEnumerable<TankerTap> GetValidTankers()
        {
            return this.Where(element => !element.IsBlocked);
        }


        //FIXME: je n'ai pas voulu modifier GetValidTankers pour le rampage des poulets,
        //       car je ne voulais avoir que les tankers qui ne se remplissent pas
        public IEnumerable<TankerTap> GetFillableTankers()
        {
            return this.Where(element => !element.IsFilling && !element.IsBlocked);
        }

        public IEnumerator<TankerTap> GetEnumerator()
        {
            return m_Elements.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}