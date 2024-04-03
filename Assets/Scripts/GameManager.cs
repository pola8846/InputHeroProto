using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //ΩÃ±€≈Ê
    private static GameManager instance;
    public static GameManager Instance => instance;

    private static Player player;
    public static Player Player => player;

    private static Player2D player2D;
    public static Player2D Player2D => player2D;
    private void Awake()
    {
        //ΩÃ±€≈Ê
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
            instance = this;
    }


    public static void SetPlayer(Player player)
    {
        GameManager.player = player;
    }
    public static void SetPlayer(Player2D player)
    {
        GameManager.player2D = player;
    }
}

