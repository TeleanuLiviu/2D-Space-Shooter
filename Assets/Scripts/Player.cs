using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  //serializefield = acces a private variable in unity editor 
    [SerializeField]
    private float _speed ;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    public int _lives =3;
    private Spawn_Manager _spawnManager;
    [SerializeField]
    private bool _laserpower = false;
    [SerializeField]
    private GameObject _laserPrefab1;
    [SerializeField]
    private bool _pspeed = false;
    private UI_Manager uimanager;
    private bool _isShieldActive = false;
    [SerializeField]
    private GameObject ShieldVisualizer;
    [SerializeField]
    private int score = 0;
    [SerializeField]
    private GameObject _rightEngine,_leftEngine;
    [SerializeField]
    private AudioClip laser;
    [SerializeField]
    private AudioSource audio;
    private int _shieldhit;
    [SerializeField]
    public GameObject Shield;
    public int ammo;
    [SerializeField]
    public GameObject AmmoFail;
    private bool specialshot;
    [SerializeField]
    public GameObject _laserPrefab2;
    [SerializeField]
    public GameObject Camera;
    public float duration ;
    public float elapsedTime;
    public bool startShake;
    // Start is called before the first frame update
    void Start()
    {

        transform.position = new Vector3(0, 0, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<Spawn_Manager>();
        startShake = false;


        if (_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is null");
        }

        uimanager = GameObject.Find("Canvas").GetComponent<UI_Manager>();

        if(audio == null)
        {
            Debug.LogError("Audio Source is null");
        }

        audio = GetComponent<AudioSource>();
        audio.clip = laser;

        _shieldhit = 0;
        ammo = 15;
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            ShootLaser();
        }

        if(startShake)
        {
            StartCoroutine(CameraShake());
        }
        else
        {
            StopCoroutine(CameraShake());
        }

        if(Input.GetKey(KeyCode.C))
        {
            FindObjectOfType<Powerup>().transform.position = Vector3.MoveTowards(FindObjectOfType<Powerup>().transform.position,transform.position , Time.deltaTime*100);
        }

    }

    void ShootLaser()
    {

        _canFire = Time.time + _fireRate;
        //Ammo Count●Limit the lasers firedby the player to only15 shots.●When the player is outof ammo, providefeedback throughon-screen elements orsound effects. (ie:beep or ammo countdisplayed on screen)

        if (ammo>0)
        {
            if (specialshot)
            {
                Instantiate(_laserPrefab2, transform.position, Quaternion.identity);
            }

            if (_laserpower == true)
            {
                Instantiate(_laserPrefab1, transform.position, Quaternion.identity);
            }

            else
            {
                Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);

            }
            ammo--;
            uimanager.UpdateAmmo(ammo);
            audio.Play();
        }

        else
        {
            AmmoFail.SetActive(true);
            FindObjectOfType<GameManager>().GameOver();
        }
       
    }

    public void NegativePickUp()
    {
        //Removes one Heart from the Player

        _lives -= 1;
        if (_lives == 3)
        {
            _leftEngine.SetActive(false);
            _rightEngine.SetActive(false);
        }

        if (_lives == 2)
        {
            _leftEngine.SetActive(true);
        }

        if (_lives == 1)
        {
            _rightEngine.SetActive(true);
        }
        uimanager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);

        }


    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);

        // Thrusters - Move the player at anincreased rate whenthe ‘Left Shift’ key ispressed down●Reset back to normalspeed when the ‘LeftShift’ key is released

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (_pspeed == false)
            {
                transform.Translate(direction * _speed * Time.deltaTime * 10.0f);
            }
            else
            {
                transform.Translate(direction * _speed * 1.5f * Time.deltaTime*10.0f);
            }
            Debug.Log("Shift was pressed.");
        }

        else 
        {
            if (_pspeed == false)
            {
                transform.Translate(direction * _speed * Time.deltaTime );
            }
            else
            {
                transform.Translate(direction * _speed * 1.5f * Time.deltaTime);
            }
        }

        


        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.8f, 0), transform.position.z);

        if (transform.position.x >= 11)
        {
            transform.position = new Vector3(-11, transform.position.y, transform.position.z);
        }

        else if (transform.position.x <= -11)
        {
            transform.position = new Vector3(11, transform.position.y, transform.position.z);

        }
    }

    public void AdScore()
    {
        int x = Random.Range(5, 30);
        score = score + x;
        uimanager.UpdateScore(score);
    }

    

    public void Damage()
    {
        //Shield Strength - ●Visualize the strengthof the shield. This canbe done through UI onscreen or colorchanging of theshield.●Allow for 3 hits on theshield toaccommodatevisualization
        if (_isShieldActive == true)
        {
            _shieldhit++;
            if (_shieldhit == 0)
            {
                Shield.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
            else if (_shieldhit == 1)
            {
                Shield.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0.55f, 1f);
            }
            else if (_shieldhit == 2)
            {
                Shield.GetComponent<SpriteRenderer>().color = new Color(1f, 0f, 0.02f, 1f);
            }

            else 
            {
                _isShieldActive = false;
                ShieldVisualizer.SetActive(false);
                _shieldhit = 0;


            }
           
        }
        else
        {
            _lives -= 1;
            if (_lives == 2)
            {
                _leftEngine.SetActive(true);
            }

            if (_lives == 1)
            {
                _rightEngine.SetActive(true);
            }

            uimanager.UpdateLives(_lives);
            if (_lives < 1)
            {
                _spawnManager.OnPlayerDeath();
                Destroy(this.gameObject);

            }
        }

       
    }

    public IEnumerator CameraShake()
    {
        Vector3 CameraInitialPosition = Camera.transform.position;
        duration = 0.3f;
        elapsedTime = 0;
        while (elapsedTime < duration)
        {
            float xPosition = Random.Range(-1.0f, 1.0f);
            float yPosition = Random.Range(-1.0f, 1.0f);
            Camera.transform.position = new Vector3(xPosition*0.4f, yPosition*0.4f, Camera.transform.position.z);
            elapsedTime += Time.deltaTime;
            yield return 0;
        }

        startShake = false;
        Camera.transform.position = CameraInitialPosition;
        
        
    }


    public void TripleShotActive()
    {
        _laserpower = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _laserpower = false;
    }

    public void AddAmmo()
    {
        ammo = 15;
        uimanager.UpdateAmmo(ammo);
        AmmoFail.SetActive(false);
    }

    public void SpeedActive()
    {
        _pspeed = true;
        StartCoroutine(SpeedPowerDownRoutine());
    }

    public void SpecialShot()
    {
       specialshot = true;
        StartCoroutine(SpecialShotDownRoutine());
    }

    //Health Collectable●Create a healthcollectable that healsthe player by 1.Update the visuals ofthe Player to reflectthis.
    public void AddHeart()
    {
        if(_lives<=3)
        {
            _lives += 1;
        }

       
        if (_lives == 3)
        {
            _leftEngine.SetActive(false);
            _rightEngine.SetActive(false);
        }

        if (_lives == 2)
        {
            _leftEngine.SetActive(true);
        }

        if (_lives == 1)
        {
            _rightEngine.SetActive(true);
        }

        uimanager.UpdateLives(_lives);
        if (_lives < 1)
        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);

        }
    }

    IEnumerator SpeedPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _pspeed = false;
    }

    IEnumerator SpecialShotDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        specialshot = false;
    }

    public void ShieldActive()
    {
        _shieldhit = 0;
        _isShieldActive = true;
        ShieldVisualizer.SetActive(true);
    }

   
    
}
