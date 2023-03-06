using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public Image some;
    public Sprite sprite;
    
    // Start is called before the first frame update
    void Start()
    {
        some.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
