using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject[] enemies;
    public Scene scene;
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");


        if (enemies.Length == 0)
        {
            Win();
        }

    }


    void Win()
    {
        Debug.Log("You Win");
    }


    public void GameOver()
    {
        Debug.Log("Game Over");
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);



    }
}
