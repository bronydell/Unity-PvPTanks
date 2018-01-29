using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class MovementController : MonoBehaviour
{
    public float MaxSpeed = 0.10f;
    public float ShootCooldown;
    public int Player = 1;
    public bool CanMove;
    public List<AudioClip> ShootingAudioClips;

    private Gunner _gun;
    private Animator _animator;
    private AudioSource _audio;
    private Rigidbody2D _rigidbody2D;
    private bool _isMoving;
    private float _lastShootTimer;
    private int _axisX;
    private int _axisY;

    private int _inputX = 0;
    private int _inputY = 1;

    // Use this for initialization
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audio = GetComponent<AudioSource>();
        _gun = GetComponentInChildren<Gunner>();
        _lastShootTimer = ShootCooldown;
        CanMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_animator.GetBool("isDead") || !CanMove)
            return;
        ProccesInput();
        ChangeInputFromMultipleKeyPresses();
        Move();
        if (_lastShootTimer > ShootCooldown && Input.GetButton("P" + Player + "Fire"))
        {
            _gun.Shoot(Player);
            _audio.clip = ShootingAudioClips[Random.Range(0, ShootingAudioClips.Count)];
            _audio.Play();
            _lastShootTimer = 0;
        }
        _lastShootTimer += Time.deltaTime;

    }

    private void ProccesInput()
    {
        if (Input.GetAxis("P" + Player + "Vertical") > 0)
            _axisY = 1;
        else if (Input.GetAxis("P" + Player + "Vertical") < 0)
            _axisY = -1;
        else
            _axisY = 0;
        if (Input.GetAxis("P" + Player + "Horizontal") > 0)
            _axisX = 1;
        else if (Input.GetAxis("P" + Player + "Horizontal") < 0)
            _axisX = -1;
        else
            _axisX = 0;
    }

    private void ChangeInputFromMultipleKeyPresses()
    {
        // Movement changing when pressing keys for both directions
        if (_axisX != 0 && _axisY != 0)
        {
            if (_inputX == 0)
            {
                _inputX = _axisX;
                _inputY = 0;
            }
            if (_inputY == 0)
            {
                _inputX = 0;
                _inputY = _axisY;
            }
        }
        // If at least one key pressed
        else if (_axisX != 0 || _axisY != 0)
        {
            _inputX = _axisX;
            _inputY = _axisY;
        }
        else
        {
            _inputX = 0;
            _inputY = 0;

        }
    }

    private void Move()
    {
        if (_axisX != 0 || _axisY != 0)
        {
            _isMoving = true;
            if (_inputX == 0)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, _inputY < 0 ? 180 : 0);
                if (Mathf.Abs(Mathf.Round(transform.position.x) - transform.position.x) < 0.3f)
                    transform.position = new Vector3(Mathf.Round(transform.position.x), transform.position.y, 0);

            }
            if (_inputY == 0)
            {
                gameObject.transform.rotation = Quaternion.Euler(0, 0, _inputX < 0 ? 90 : -90);
                if(Mathf.Abs(Mathf.Round(transform.position.y) - transform.position.y ) < 0.3f)
                    transform.position = new Vector3(transform.position.x, Mathf.Round(transform.position.y), 0);
            }
            _rigidbody2D.MovePosition(_rigidbody2D.position + new Vector2(MaxSpeed * _inputX, MaxSpeed * _inputY) * Time.deltaTime);
        }
        else
        {
            _isMoving = false;
        }
        _animator.speed = _isMoving || _animator.GetCurrentAnimatorStateInfo(0).IsName("Death") ? 1 : 0;
    }
    

}
