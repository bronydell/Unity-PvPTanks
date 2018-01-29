using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject LifeGameObject;
    
    public Text StatusText;
    public Text TimerText;
    public Text Instructions;
    public bool Paused;
    public float DistanceGap;
    public float TimeOut;
    public float BeaconTime;
    public float RedZoneTime;
    public GameObject Beacon;
    public GameObject RedDot;

    public float BonusTime;
    public List<Transform> SpawnPoints;
    public List<GameObject> BonusList;
    public AudioClip BonusClip;
    public AudioClip FoundBeaconClip;
    public AudioClip FlagClip;
    public AudioClip EndGameClip;

    private float _timer;
    private float _lastFlag; 
    private float _lastBonus; 
    private float _lastRedZone; 
    private List<Player> _players;
    private AudioSource _audio;
    private Camera _camera;
    private bool _isEnd;
    private bool _hasFlag;
    private bool _hasBonus;

    private const int FromX = -12;
    private const int ToX = 13;
    private const int FromY = -4;
    private const int ToY = 6;

    // Use this for initialization
    void Start () {
	    StatusText.enabled = false;
        Instructions.enabled = false;
        _audio = GetComponent<AudioSource>();
        _camera = Camera.main;
        _players = FindObjectsOfType<Player>().ToList();
	    _timer = 0.0f;
        _lastBonus = 0.0f;
        _lastFlag = 0.0f;
        _lastRedZone = 0.0f;
        _isEnd = false;
        _hasFlag = false;
        _hasBonus = false;
        float normalAspect = 16 / 9f;

        _camera.orthographicSize = 9.5f * normalAspect / ((float)_camera.pixelWidth / _camera.pixelHeight);
    }

    void Update()
    {
        if (!_isEnd)
        {
            _timer += Time.deltaTime;
            _lastFlag += Time.deltaTime;
            _lastBonus += Time.deltaTime;
            _lastRedZone += Time.deltaTime;
            if (TimeOut < _timer)
            {
                Player winnerPlayer = _players[0];
                foreach (var player in _players)
                {
                    player.MovementController.CanMove = false;
                    if (winnerPlayer.Score < player.Score)
                        winnerPlayer = player;
                }
                StatusText.text = "Player #" + winnerPlayer.MovementController.Player + " won!";
                StatusText.enabled = true;
                Instructions.enabled = true;
                TimerText.enabled = false;
                _audio.clip = EndGameClip;
                _audio.Play();
                _isEnd = true;

            }
            else
            {
                int minutes = (int) ((TimeOut - _timer) / 60);
                int seconds = (int) (TimeOut - _timer) - minutes * 60;
                string timerText = "";
                if (minutes < 10)
                    timerText += "0";
                timerText += minutes + ":";
                if (seconds < 10)
                    timerText += "0";
                timerText += seconds + "";
                TimerText.text = timerText;
                if (!_hasFlag && BeaconTime < _lastFlag)
                {
                    Debug.Log("Spawning Beacon!");
                    AttemptSpawn(Beacon, true);
                }
                if (!_hasBonus && BonusTime < _lastBonus && BonusList.Count > 0)
                {
                    Debug.Log("Spawning Bonus!");
                    AttemptSpawn(BonusList[Random.Range(0, BonusList.Count)], false);
                }
                if (RedZoneTime < _lastRedZone)
                    RedZoneSpawn();
            }

        }
        else
        {
            if(Input.GetButtonDown("Submit"))
                SceneManager.LoadScene(0, LoadSceneMode.Single);
        }
    }

    public void BeconFound()
    {
        _audio.clip = FoundBeaconClip;
        _audio.Play();
        _hasFlag = false;
        _lastFlag = 0;
    }

    public void BonusFound()
    {
        _hasBonus = false;
        _lastBonus = 0;
    }

    public void RedZoneSpawn()
    {
        Vector2 randomPlayerPosition = _players[Random.Range(0, _players.Count)].transform.position;
        randomPlayerPosition.x += Random.Range(-2, 2);
        randomPlayerPosition.x = Mathf.Round(randomPlayerPosition.x);
        if (randomPlayerPosition.x > ToX)
            randomPlayerPosition.x = ToX;
        if (randomPlayerPosition.x < FromX)
            randomPlayerPosition.x = FromX;
        randomPlayerPosition.y += Random.Range(-2, 2);
        randomPlayerPosition.y = Mathf.Round(randomPlayerPosition.y);
        if (randomPlayerPosition.y > ToY)
            randomPlayerPosition.y = ToY;
        if (randomPlayerPosition.y < FromY)
            randomPlayerPosition.y = FromY;
        Instantiate(RedDot, randomPlayerPosition, Quaternion.identity);
        _lastRedZone = 0;
    }

    public void RemovePlayer(Player player)
    {
        _players.Remove(player);
        if (_players.Count == 1)
        {
            StatusText.text = "Player " + _players[0].MovementController.Player + " WON!";
            _players[0].MovementController.CanMove = false;
        }
        else if (_players.Count == 0)
        {
            StatusText.text = "Draw!";
        }
        StatusText.enabled = true;  
    }

    public void AttemptSpawn(GameObject gameObject, bool isFlag)
    {
        while (true)
        {
            var randomPoint = new Vector2(Random.Range(FromX, ToX), Random.Range(FromY, ToY));
            if (Physics2D.OverlapCircle(randomPoint, 0.4f) == null)
            {
                bool shouldSpawn = true;
                foreach (var player in _players)
                {
                    if (Vector2.Distance(player.transform.position, randomPoint) < DistanceGap)
                    {
                        shouldSpawn = false;
                        break;
                    }
                }
                if(!shouldSpawn)
                    continue;
                
                Instantiate(gameObject, randomPoint, Quaternion.identity);
                if (isFlag)
                {
                    _audio.clip = FlagClip;
                    _audio.Play();
                    _hasFlag = true;
                }
                else
                {
                    _audio.clip = BonusClip;
                    _audio.Play();
                    _hasBonus = true;
                }
                break;
            }
        }
    }

}
