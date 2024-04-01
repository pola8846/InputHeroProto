using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //�̱���
    private static GameManager instance;
    public static GameManager Instance => instance;

    private static Player player;
    public static Player Player => player;

    private void Awake()
    {
        //�̱���
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
            instance = this;
    }


    public static void SetPlayer(Player player)
    {
        
    }
}
