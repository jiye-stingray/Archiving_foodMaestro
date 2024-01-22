using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuestManager : Singleton<GuestManager>
{
    SoundManager _soundManager => SoundManager.Instance;

    public Transform _entranceAndExitTrans;
    [SerializeField] private GameObject[] _guestPrefab;

    [SerializeField] List<Guest> _guestList = new List<Guest>();
    public Queue<Guest> _waitingGuestQueue = new Queue<Guest>();

    [SerializeField] AudioClip _eatAudioClip;

    [SerializeField] Transform _guestParentTrans;

    /// <summary>
    /// Guest 생성
    /// </summary>
    public void CreateGuest()
    {
        StartCoroutine(CreateGuestCor());
    }

    bool _createGuestCor = false;
    IEnumerator CreateGuestCor()
    {
        while(_createGuestCor)      //  무한 대기
        {
            yield return new WaitForEndOfFrame();
        }

        _createGuestCor = true;

        Guest g = Instantiate(_guestPrefab[Random.Range(0, _guestPrefab.Length)], _entranceAndExitTrans.position, Quaternion.identity).GetComponent<Guest>();
        g.transform.SetParent(_guestParentTrans);
        _guestList.Add(g);

        _createGuestCor = false;
    }

    public void VisitGuest(Guest guest)
    {
        _guestList.Add(guest);
        _waitingGuestQueue.Enqueue(guest);
    }

    public void ExitGuest(Guest guest)
    {
        GameManager.Instance.SoundPlay(_eatAudioClip, SceneType.GameScene);
        _guestList.Remove(guest);
    }
}
