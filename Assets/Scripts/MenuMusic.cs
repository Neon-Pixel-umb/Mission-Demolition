using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusic : MonoBehaviour
{
    private void Awake()
    {
        // Ensure the music object persists between scene loads
        DontDestroyOnLoad(gameObject);
    }
}
