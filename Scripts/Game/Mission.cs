using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mission : MonoBehaviour
{
    private int nbEnemy;
    private Text txtInfos;
    private GameObject[] enemyTab;
    private GameObject statue;
    private GameObject rocExit;
    private GameObject player;
    private GameObject exit;

    //DoorOpenning
    [SerializeField]
    private GameObject rockSouth1;
    [SerializeField]
    private GameObject rockSouth2;
    [SerializeField]
    private GameObject rockNorth1;
    [SerializeField]
    private GameObject rockNorth2;

    private float maxOpenValue = 3f;
    private float currentValue = 0f;
    private float currentValue2 = 0f;
    private float speed = 1.5f;

    [SerializeField]
    private Sprite statueWin;

    public SaveManager saveManager;

    public int NbEnemy { get => nbEnemy; set => nbEnemy = value; }
    
    void Start()
    {
        txtInfos = GameObject.Find("TxtInfos").GetComponent<Text>();
        enemyTab = GameObject.FindGameObjectsWithTag("enemy");
        statue = GameObject.FindGameObjectWithTag("StatueExit");
        rocExit = GameObject.FindGameObjectWithTag("ExitRoc");
        player = GameObject.FindGameObjectWithTag("Player");
        exit = GameObject.FindGameObjectWithTag("Exit");
        

        foreach (GameObject enemy in enemyTab)
        {
            nbEnemy += 1;
        }

        txtInfos.text = nbEnemy.ToString();
    }
    
    void Update()
    {
        nbEnemy = 0;
        enemyTab = GameObject.FindGameObjectsWithTag("enemy");

        foreach (GameObject enemy in enemyTab)
        {
            nbEnemy += 1;
        }

        txtInfos.text = nbEnemy.ToString();

        if(nbEnemy == 10) OpenningNorthDoors();

        if(nbEnemy == 5) OpenningSouthDoors();

        if(nbEnemy == 0) Win();
    }

    private void OpenningNorthDoors()
    {
        float movement = speed * Time.deltaTime;
        currentValue += movement;
        if(currentValue <= maxOpenValue)
        {
            Debug.Log("La porte est censé s'ouvrir !!");
            rockNorth1.transform.position = new Vector3(rockNorth1.transform.position.x - movement, rockNorth1.transform.position.y, rockNorth1.transform.position.z);
            rockNorth2.transform.position = new Vector3(rockNorth2.transform.position.x + movement, rockNorth2.transform.position.y, rockNorth2.transform.position.z);
        }       
    }

    private void OpenningSouthDoors()
    {
        float movement = speed * Time.deltaTime;
        currentValue2 += movement;
        if (currentValue2 <= maxOpenValue)
        {
            Debug.Log("La porte est censé s'ouvrir !!");
            rockSouth1.transform.position = new Vector3(rockSouth1.transform.position.x - movement, rockSouth1.transform.position.y, rockSouth1.transform.position.z);
            rockSouth2.transform.position = new Vector3(rockSouth2.transform.position.x + movement, rockSouth2.transform.position.y, rockSouth2.transform.position.z);
        }
    }

    private void Win()
    {
        statue.GetComponent<SpriteRenderer>().sprite = statueWin;
        Destroy(rocExit);

        //Load the Win scene
        float distance = Vector2.Distance(player.transform.position, exit.transform.position);
        if (distance <= 0.5f)
        {
            saveManager.Instance.Save();
            SceneManager.LoadScene("Win");
        }
    }
}
