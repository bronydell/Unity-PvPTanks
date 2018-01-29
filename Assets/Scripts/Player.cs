using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(MovementController))]
public class Player : MonoBehaviour
{
    private GameManager _gameManager;
    private Animator _animator;
    private AudioSource _audio;

    public int Lives;
    public Text ScoreText;
    public int Score = 0;
    public MovementController MovementController;
    public List<AudioClip> DeadAudioClips;

    // Use this for initialization
    void Start ()
    {
        Lives = 1;
        MovementController = GetComponent<MovementController>();
	    _animator = GetComponent<Animator>();
        _audio = GetComponent<AudioSource>();
        _gameManager = FindObjectOfType<GameManager>();

    }
    

	// Update is called once per frame
	void Update () {

	    ScoreText.text = Score + "";
    }

    public void Die()
    {
        _animator.speed = 1;
        _audio.clip = DeadAudioClips[Random.Range(0, DeadAudioClips.Count )];
        _audio.Play();
        _animator.SetBool("isDead", true);
    }

    public void DestroyOrMove()
    {
        Debug.Log("DestroyOrMove");
        _animator.SetBool("isDead", false);
        Spawn();
    }

    public void Spawn()
    {
        if (_gameManager.SpawnPoints.Count == 0)
            return;
        for(int i = 0; i < _gameManager.SpawnPoints.Count; i++)
        {
            var randomPoint = (Vector2)_gameManager.SpawnPoints[Random.Range(0, _gameManager.SpawnPoints.Count)].position;
            if (Physics2D.OverlapCircle(randomPoint, 0.4f) == null)
            {
                Debug.Log("Respawning");
                transform.position = randomPoint;

                break;
            }
            else
            { 
                Debug.Log(Physics2D.OverlapCircle(randomPoint, 0.5f).name + " POINT: " + randomPoint);
            }
        }
    }
}
