using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Environnement
{
    [CreateAssetMenu(fileName = "TankerTapCollection", menuName = "Water/Tanker Tap Collection", order = 0)]
    public class TankerTapCollection : ScriptableObject, IEnumerable<TankerTap>
    {
        private List<TankerTap> m_Elements;

        public void AddElement(TankerTap _tap)
        {
            m_Elements.Add(_tap);
        }

        public IEnumerable<TankerTap> GetValidTankers()
        {
            return this.Where(element => !element.IsBlocked);
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