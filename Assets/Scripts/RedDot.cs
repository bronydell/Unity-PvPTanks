using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(SpriteRenderer))]
public class RedDot : MonoBehaviour
{

    public float FallTime;
    public GameObject Destructor;
    public AudioClip FallDown;

    private float _fallTimer;
    private SpriteRenderer _spriteRender;
    private AudioSource _audio;


	// Use this for initialization
	void Start ()
	{
	    _fallTimer = 0;
	    _spriteRender = GetComponent<SpriteRenderer>();
	    _audio = GetComponent<AudioSource>();
	    _audio.clip = FallDown;
	    _audio.Play();
        _spriteRender.color = new Color(255, 255, 255, (int)(255 * (_fallTimer / FallTime)));


	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    _fallTimer += Time.fixedDeltaTime;
        if (_fallTimer >= FallTime)
	    {
            for(float i = transform.position.x - 1; i < transform.position.x + 2; i++)
                for (float j = transform.position.y - 1; j < transform.position.y + 2; j++)
                {
                    Instantiate(Destructor, new Vector2(i, j), Quaternion.identity);
                }
            Destroy(gameObject);
	    }
	    var color = _spriteRender.color;
        color.a = _fallTimer / FallTime;
	    _spriteRender.color = color;
    }
}
