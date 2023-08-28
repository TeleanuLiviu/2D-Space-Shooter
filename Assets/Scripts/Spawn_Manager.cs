using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Spawn_Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab, _enemyPrefab2;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] powerups;
    [SerializeField]
    private GameObject Ammo;
    [SerializeField]
    public GameObject SpecialShot;
    public bool _stopSpawning = false;
    public bool _stopSpawningEnemies = false;
    [SerializeField]
    public GameObject IncomingWave;
    public long spawnedEnemies;
    [SerializeField]
    public GameObject boss;
    [SerializeField]
    public GameObject BossIncoming;
    public int k = 1;


    public void startSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerRoutine());
        StartCoroutine(SpawnAmmo());
        StartCoroutine(SpawnSpecialShot());
        StartCoroutine(SpawnWave());
    }


    public void OnPlayerDeath()
    {
        _stopSpawning = true;
        _stopSpawningEnemies = true;


    }

    IEnumerator SpawnEnemyRoutine()
    {
        while(_stopSpawningEnemies == false)
        {
            yield return new WaitForSeconds(5.0f);
            Vector3 PosToSpawn = new Vector3(Random.Range(-9f, 9f), 7, 0);
            GameObject newenemy = Instantiate(_enemyPrefab, PosToSpawn, Quaternion.identity);
            
            yield return new WaitForSeconds(5.0f);
            Vector3 PosToSpawn2 = new Vector3(Random.Range(-9f, 9f), 7, 0);
            GameObject newenemy2 = Instantiate(_enemyPrefab, PosToSpawn2, Quaternion.Euler(PosToSpawn2.x, PosToSpawn2.y, Random.Range(-50.0f,50.0f)));
            
            yield return new WaitForSeconds(5.0f);
            Vector3 PosToSpawn3 = new Vector3(Random.Range(-9f, 9f), 5, 0);
            GameObject newenemy3 = Instantiate(_enemyPrefab2, PosToSpawn, Quaternion.identity);
            spawnedEnemies ++;

            if (spawnedEnemies > 1)
             {

                 BossIncoming.SetActive(true);
                 StartCoroutine(TextCoroutineBoss());
                 yield return new WaitForSeconds(5.0f);
                 StopCoroutine(TextCoroutineBoss());
                 BossIncoming.SetActive(false);
                 GameObject newenemy4 = Instantiate(boss, new Vector3(0f,3f,0f), Quaternion.identity);
                  _stopSpawningEnemies = true;
                 spawnedEnemies = 0;
                StopCoroutine(SpawnWave());
                StopCoroutine(SpawnEnemyRoutine());
             }
        }





    }



    IEnumerator TextCoroutine()
    {
        while (true)
        {
            IncomingWave.GetComponent<Text>().text = "INCOMING WAVE";
            yield return new WaitForSeconds(0.5f);
            IncomingWave.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(0.5f);
        }


    }

    IEnumerator TextCoroutineBoss()
    {
        while (true)
        {
            BossIncoming.GetComponent<Text>().text = "BOSS";
            yield return new WaitForSeconds(0.5f);
            BossIncoming.GetComponent<Text>().text = "";
            yield return new WaitForSeconds(0.5f);
        }


    }

    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(10.0f);

       
        while (_stopSpawningEnemies == false)
        {
            
            if (k<3 && _stopSpawningEnemies == false)
            {
               
                IncomingWave.SetActive(true);
                StartCoroutine(TextCoroutine());
                yield return new WaitForSeconds(1.0f);
                IncomingWave.SetActive(false);
                StopCoroutine(TextCoroutine());
                for (int i=0; i<k;i++)
                {
                    
                    GameObject newenemy = Instantiate(_enemyPrefab,new Vector3(k + i*5, 8f,0f), Quaternion.identity);
                    GameObject newenemy2 = Instantiate(_enemyPrefab, new Vector3(k - i*5, 8f, 0f), Quaternion.identity);
                }
                k++;
                if (k >= 3) k = 0;
                yield return new WaitForSeconds(5.0f);
               
            }

            

        }
    }


    IEnumerator SpawnPowerRoutine()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(3.0f);
            Vector3 PosToSpawn1 = new Vector3(Random.Range(-9f, 9f), 7, 0);
            int randomPowerup = Random.Range(0, 5);
            GameObject newpow = Instantiate(powerups[randomPowerup], PosToSpawn1, Quaternion.identity);
            newpow.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(Random.Range(10.0f,30.0f));
        }

        

    }
    //Ammo Collectable●Create a powerup thatrefills the ammo countallowing the player tofire again

    IEnumerator SpawnAmmo()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(10.0f);
            Vector3 PosToSpawn1 = new Vector3(Random.Range(-9f, 9f), 7, 0);
            GameObject newpow = Instantiate(Ammo, PosToSpawn1, Quaternion.Euler(transform.position.x,transform.position.y, 90.0f));
            yield return new WaitForSeconds(1.0f);
        }
    }

    IEnumerator SpawnSpecialShot()
    {
        while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(10.0f);
            Vector3 PosToSpawn1 = new Vector3(Random.Range(-9f, 9f), 7, 0);
            GameObject newpow = Instantiate(SpecialShot, PosToSpawn1, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(10.0f, 15.0f));
        }
    }


}
