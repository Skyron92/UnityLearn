using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PoissonDie : MonoBehaviour
   
{
    public TextMeshProUGUI Scoring; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PoissonDeath();
    }

    void PoissonDeath()
    {
        if (transform.position.y < -3)
        {
            Destroy(gameObject);
            int Score=int.Parse(Scoring.text);
            Score++; 
            Scoring.text = Score.ToString();


        }
    }
}
