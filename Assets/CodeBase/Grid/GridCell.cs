using UnityEngine;

namespace CodeBase.Grid {
    public class GridCell
    {
        public bool isEmpty;
        public GameObject item;

        public GridCell()
        {
            isEmpty = true;
            item = null;
        }

        public void PlaceItem(GameObject newItem)
        {
            isEmpty = false;
            item = newItem;
        }

        public void RemoveItem()
        {
            isEmpty = true;
            item = null;
        }
    }
}