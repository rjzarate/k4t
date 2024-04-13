using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteRandomizer : MonoBehaviour
{
    [SerializeField] private List<Sprite> sprites = new List<Sprite>();
    private void Awake()
    {
        if (sprites.Count > 0)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Count)];
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
