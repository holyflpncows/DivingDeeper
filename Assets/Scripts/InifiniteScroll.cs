using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InifiniteScroll : MonoBehaviour
{
    private Image _image;
    [SerializeField] private Vector2 speed;
    
    // Start is called before the first frame update
    void Start()
    {
        _image = GetComponent<Image>();
        _image.material = new Material(_image.material); //Clone the original material

    }

    // Update is called once per frame
    void Update()
    {
        _image.material.mainTextureOffset += speed * Time.deltaTime;
    }

    private void Reposition()
    {

    }
}
