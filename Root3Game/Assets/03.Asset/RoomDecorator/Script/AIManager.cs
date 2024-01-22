using Model;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class AIManager : MonoBehaviour {

    private Sorter sorter;
    private Tiles tiles;

    public NavMeshAgent agent;
    public GameObject idol;
    public Toggle mode;

    [Header("Custom")]
    Animator _idolAnim;
    private Vector3 _toMoveVec;

    void Awake()
    {
        sorter = GameObject.Find("Unit").GetComponent<Sorter>();
        tiles = GameObject.Find("Tiles").GetComponent<Tiles>();


       _idolAnim = idol.GetComponentInChildren<Animator>();
        _toMoveVec = idol.transform.position;
    }


    void Update ()
    {
        var pos = agent.gameObject.transform.position;
        idol.transform.position = new Vector2(pos.x, pos.z / 2f);
        var unit = idol.GetComponent<Character>();
        var tile = tiles.GetTileByPoint(idol.transform.position);
        if (unit.origin != tile)
        {
            unit.UpdateTile(tile);
            sorter.SortUnit(unit);

        }

        //idol.transform.GetChild(0).GetComponent<SpriteRenderer>().flipX = agent.gameObject.transform.eulerAngles.y < 180f;

        idol.transform.eulerAngles = agent.gameObject.transform.eulerAngles.y > 180f ? Vector3.zero : new Vector3(0, 180, 0);


        if (!mode.isOn)
        {
            if (Input.GetMouseButtonUp(0))
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                var hits = Physics.RaycastAll(ray, Mathf.Infinity);
                foreach (var hit in hits)
                {
                    var selectedTile = hit.transform.gameObject.GetComponent<Tile>();

                    if (selectedTile != null)
                    {
                        if (selectedTile.isBlock) return;       // 가구 설치 시 예외처리

                        _toMoveVec = selectedTile.gameObject.transform.position;
                        agent.SetDestination(new Vector3(_toMoveVec.x, 0, _toMoveVec.y * 2));
                        _idolAnim.SetBool("Walk", true);
                    }
                }
            } 
        }

        #region Custom 
        if(Vector2.Distance(idol.transform.position,_toMoveVec) <= 0.5)
        {
            _idolAnim.SetBool("Walk", false);
        }

        #endregion

    }


}
