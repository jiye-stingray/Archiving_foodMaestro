using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Model
{
    public enum Direction : int
    {
        South,
        West,
        North,
        East,
    }

    [System.Serializable]
    public class Furniture : BaseUnit
    {
        public GameObject[] ListDirectionItem = new GameObject[4];
        public SpriteRenderer _selectedDirectionItemSprite;

        public Direction direction = Direction.South;
        public HistoricalData previous { get; private set; }
        private List<GameObject> blocks;

        private const string Unit_LAYER = "Unit";
        private const string PREVIEW_LAYER = "Preview";

        public FurnitureData _furnitureData;

        public int x = 0;
        public int y = 0;

        private void Awake()
        {
            _selectedDirectionItemSprite = GetComponentInChildren<SpriteRenderer>();
        }

        public void Rotate()
        {
            direction = (Direction)(((int)direction + 1) % 4);

            foreach (var dir in ListDirectionItem)
                dir.SetActive(false);

            ListDirectionItem[(int)direction].SetActive(true);
            var temp = width;
            width = length;
            length = temp;


            // 현재 선택된 객체의 sprite 받기 
            _selectedDirectionItemSprite = ListDirectionItem[(int)direction].GetComponent<SpriteRenderer>();
        }

        public void Rotate(Direction dir)
        {

            if (Mathf.Abs(dir - direction) % 2 == 1)
            {
                var temp = width;
                width = length;
                length = temp;
            }
            direction = dir;

            foreach (var diritem in ListDirectionItem)
                diritem.SetActive(false);

            ListDirectionItem[(int)dir].SetActive(true);

            // 현재 선택된 객체의 sprite 받기 
            _selectedDirectionItemSprite = ListDirectionItem[(int)direction].GetComponent<SpriteRenderer>();


        }

        public void Move(Tile tile)
        {
            gameObject.transform.position = tile.transform.position;
            origin = tile;
            x = tile.x;
            y = tile.y;
        }

        public void SetColor(Color color)
        {
            foreach (var dir in ListDirectionItem)
                dir.GetComponent<SpriteRenderer>().color = color;
        }

        public void Place(List<Tile> tiles)
        {
            base.tiles = tiles;
            base.tiles.ForEach(tile => tile.isBlock = true);

            foreach (var dir in ListDirectionItem)          // layer set
                dir.GetComponent<Renderer>().sortingLayerName = Unit_LAYER;

            _selectedDirectionItemSprite = ListDirectionItem[(int)direction].GetComponent<SpriteRenderer>();

            previous = new HistoricalData(origin, direction);
            //create box collider in 3D.
            Block(tiles);
        }

        public void Unplaced()
        {
            tiles.ForEach(tile => tile.isBlock = false);
            tiles = new List<Tile>();

            foreach (var dir in ListDirectionItem)
                dir.GetComponent<Renderer>().sortingLayerName = PREVIEW_LAYER;

            UnBlock();
        }

        private void Block(List<Tile> tiles)
        {
            blocks = new List<GameObject>();
            foreach (var tile in tiles)
            {
                var block = GameObject.CreatePrimitive(PrimitiveType.Cube);
                block.transform.SetParent(GameObject.Find("AIManager").transform);
                block.transform.localEulerAngles = new Vector3(0, 0, 0);
                block.transform.localScale = new Vector3(2.8f, 1f, 2.8f);
                block.transform.position = new Vector3(tile.gameObject.transform.position.x, 0, tile.gameObject.transform.position.y * 2);
                block.AddComponent<NavMeshObstacle>().carving = true;
                block.GetComponent<Renderer>().enabled = false;
                blocks.Add(block);
            }
        }

        private void UnBlock()
        {
            if (blocks != null)
            {
                blocks.ForEach(block => DestroyImmediate(block));
                blocks = null;
            }
        }
    }
}