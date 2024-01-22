using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace Model
{
    public class BaseUnit : MonoBehaviour
    {
        public int width = 1;
        public int length = 1;

        private int sortedOrder;
        public int order
        {
            get { return sortedOrder; }
            set
            {
                sortedOrder = value;
                //foreach (Transform child in transform)
                //{
                //    if (child.TryGetComponent<Renderer>(out Renderer render))
                //    {
                //        render.sortingOrder = value;
                //    }
                //}

                SortingGroup sortingGroup = GetComponent<SortingGroup>();
                sortingGroup.sortingOrder = value;
            }
        }

        public Tile origin { get; protected set; }
        protected List<Tile> tiles = new List<Tile>();

        public List<Tile> GetTiles()
        {
            return tiles;
        }
    }
}
