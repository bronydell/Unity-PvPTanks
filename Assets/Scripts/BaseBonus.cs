using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseBonus : MonoBehaviour {

    public float EffectTime;
    public AudioClip PlayBonus;

    private GameManager _gameManager;
    private AudioSource _audio;
    protected Player Player;

    void Start()
    {
        _gameManager = FindObjectOfType<GameManager>();
        _audio = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (_audio != null)
            {
                _audio.clip = PlayBonus;
                _audio.Play();
            }
            Player = other.GetComponent<Player>();
            Effect();
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(Reset());
        }
    }

    public virtual void Effect()
    {
    }

    public virtual void Deffect()
    {
    }

    IEnumerator Reset()
    {
        yield return new WaitForSeconds(EffectTime);
        Deffect();
        _gameManager.BonusFound();
        Destroy(gameObject);
    }
}
