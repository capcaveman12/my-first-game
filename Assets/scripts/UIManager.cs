using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private Text _scoreText;
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        
    }

    public void NewScore(int playerscore)
    {
        _scoreText.text = "Score: " + playerscore.ToString();
    }
}
