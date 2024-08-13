using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Text _scoreText;

    [SerializeField]
    private Sprite[] _livesSprite;

    [SerializeField]
    private Image _livesImg;

    [SerializeField]
    private Text _gameOverTxt;

    [SerializeField]
    private Text _restartText;

    [SerializeField]
    private Text _ammoCountDisplay;

    [SerializeField]
    private Text _outOfAmmoDisplay;

    private GameManager _gameManager;

    [SerializeField]
    Scrollbar _scrollbar;

    private Player _player;

    [SerializeField]
    private Text _thrusterWarningText;

    public int wave = 1;

    [SerializeField]
    private Text _waveText;

    [SerializeField]
    Text _enemiesText;

    public int enemies;

    SpawnManager _spawn;
   
    void Start()
    {
        _spawn = GameObject.Find("spawnmanager").GetComponent<SpawnManager>();
        _player = GameObject.Find("Player").GetComponent<Player>();

         _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        _scoreText.text = "Score: " + 0;

        _ammoCountDisplay.text = "Ammo: " + 15;

        _outOfAmmoDisplay.text = "Out Of Ammo!";

        _gameOverTxt.gameObject.SetActive(false);

        _restartText.gameObject.SetActive(false);

        _thrusterWarningText.gameObject.SetActive(false);

        
       

        
       _enemiesText.text = "Enemies: " + 10;
        WaveUpdate();

        StartCoroutine(WaveDisplay());

    }

    private void Update()
    {
        UpdateScrollbar();

        if(_scrollbar.size == 1.0f)
        {
            Player.ThrustersOverheat();
            StartCoroutine(ThrusterWarningText());
        }
    }

    public void UpdateScrollbar()
    {
        _scrollbar.size = Player.scrollBar;
    }

    public void NewScore(int playerscore)
    {
        _scoreText.text = "Score: " + playerscore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _livesImg.sprite = _livesSprite[currentLives];
        


        if (currentLives == 0)
        {

            _gameManager.GameOver();

            GameOverSequence();
        }
    }

    public void GameOverSequence()
    {
        
        StartCoroutine(GameOverFlicker());
        _gameOverTxt.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
    }

    public IEnumerator GameOverFlicker()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            _gameOverTxt.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameOverTxt.gameObject.SetActive(false);
        }
    }

    public void AmmoCount(int ammo)
    {
        _ammoCountDisplay.text = "Ammo: " + ammo;

        if(ammo == 0)
        {
            _outOfAmmoDisplay.gameObject.SetActive(true);
        }
        else if(ammo != 0)
        {
            _outOfAmmoDisplay.gameObject.SetActive(false);
        }
    }

    IEnumerator ThrusterWarningText()
    {
        if(_scrollbar.size == 1.0f)
        {
            yield return new WaitForSeconds(0.5f);
            _thrusterWarningText.gameObject.SetActive(true);
            yield return new WaitForSeconds(3.0f);
            _thrusterWarningText.gameObject.SetActive(false);
            yield return new WaitForSeconds(1.0f);
            StartCoroutine(StopWarningText());
        }
    }

    IEnumerator StopWarningText()
    {
        yield return new WaitForSeconds(3.0f);
        StopCoroutine(ThrusterWarningText());
    }

    IEnumerator WaveDisplay()
    {
        _waveText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        _waveText.gameObject.SetActive(false);
        yield return new WaitForSeconds(1.0f);
        StopCoroutine(WaveDisplay());
    }

    public void EnemyUpdate()
    {
        enemies = _spawn.enemiesLeft;
        _enemiesText.text = "Enemies" + enemies.ToString();
    }

    public void WaveUpdate()
    {
        wave += 1;
        _waveText.text = "WAVE: " + wave.ToString();
    }
}
