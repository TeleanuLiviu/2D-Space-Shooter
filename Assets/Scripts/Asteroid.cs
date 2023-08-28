using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float rotatespeed = 3.0f;
    [SerializeField]
    private GameObject explosion;
    private Spawn_Manager _spawnmanager;
    

    private void Start()
    {
        transform.position = new Vector3(Random.Range(-6.0f, 6.0f), Random.Range(0.0f, 6.0f), 0);
        _spawnmanager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
    }

    // Update is called once per frame
    void Update()
    {
       

        
        transform.Rotate(Vector3.forward * rotatespeed * Time.deltaTime);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.tag == "Laser")
        {
            
            Destroy(collision.gameObject);
            Instantiate(explosion, transform.position, Quaternion.identity);
            _spawnmanager.startSpawning();
            Destroy(this.gameObject, 0.25f);
        }

      
    }



}
