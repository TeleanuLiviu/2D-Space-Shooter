using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    private int score;
    private Player _p;
    private Animator _anim;
    private AudioSource audio;
    Player player;
    [SerializeField]
    public GameObject _laser;
    public bool backwards;
    bool isshooting = true;
    // Start is called before the first frame update
    void Start()
    {

        _p = GameObject.Find("Player").GetComponent<Player>();
        _anim = GetComponent<Animator>();
        audio = GetComponent<AudioSource>();

        StartCoroutine(Shot());
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.tag == "Enemy")
        {
            transform.Translate(Vector3.down * Time.deltaTime * _speed);
        }
        else if(gameObject.tag == "Enemy2")
        {
            transform.Translate(Vector3.left * Time.deltaTime * _speed);
        }
        if (gameObject.tag == "Enemy")
        {
            if (transform.position.y < -5f)
            {
                float randomX = Random.Range(-9f, 9f);
                transform.position = new Vector3(randomX, 7f, 0f);
            }
        }
        else if (gameObject.tag == "Enemy2")
        {
            if (transform.position.x < -10f)
            {
                float randomy = Random.Range(-2.5f, 6f);
                transform.position = new Vector3(11f, randomy, 0f);
            }
        }


      
            
        


    }

    IEnumerator Shot()
    {
       
        while (isshooting)
        {
           
            Instantiate(_laser, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(2.0f);


        }
        

    }

  

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            if (player !=null)
            {
                player.Damage();
                player.startShake = true;
            }
            _anim.SetTrigger("OnEnemyDeath");
            
            _speed = 0.2f;
            Destroy(this.gameObject, 2.2f);
            audio.Play();
        }


        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_p != null)
            {
                _p.AdScore();
            }
            _anim.SetTrigger("OnEnemyDeath");
            isshooting = false;
            StopCoroutine(Shot());
            _speed = 0.2f;
            audio.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject,2.2f);
            
        }
    }

   

}
