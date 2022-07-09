using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class titlebgmStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.In.PlaySound("titleBGM", SoundType.BGM, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
