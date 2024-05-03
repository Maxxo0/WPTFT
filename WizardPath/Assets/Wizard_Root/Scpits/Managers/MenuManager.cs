using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    [SerializeField] GameObject opt;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Options()
    {
        opt.gameObject.SetActive(true);
    }

    public void Close()
    {
        opt.gameObject.SetActive(false);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);

    }
}
