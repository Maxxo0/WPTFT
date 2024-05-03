using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {

                Debug.Log("GameManager is null!");
            }
            return instance;
        }
    }

    public enum ElementStatus { fire, water, air, earth, fw, fa, fe, wa, we, ae}

    public ElementStatus actualElement = ElementStatus.fire;

    int actualAttack;

    bool oneTime;

    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    // Start is called before the first frame update
    void Start()
    {
        actualAttack = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (actualAttack == 1) {  actualElement = ElementStatus.fire; }
        if (actualAttack == 2) {  actualElement = ElementStatus.water; }
        if (actualAttack == 3) {  actualElement = ElementStatus.air; }
        if (actualAttack == 4) {  actualElement = ElementStatus.earth; }
        if (actualAttack == 5) {  actualElement = ElementStatus.fw; }
        if (actualAttack == 6) {  actualElement = ElementStatus.fa; }
        if (actualAttack == 7) {  actualElement = ElementStatus.fe; }
        if (actualAttack == 8) {  actualElement = ElementStatus.wa; }
        if (actualAttack == 9) {  actualElement = ElementStatus.we; }
        if (actualAttack == 10) {  actualElement = ElementStatus.ae; }

        if (actualAttack > 10) { actualAttack = 1; }
        if (actualAttack < 1) { actualAttack = 10; }

        
    }

    




    public void NextAttack(InputAction.CallbackContext context)
    {
        if(context.started) { actualAttack++; }
    }

    public void PrevAttack(InputAction.CallbackContext context) 
    {
        if (context.started) { actualAttack--; }
    }

   

}
