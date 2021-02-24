using System;
using UnityEngine;
using UnityEngine.UI;

public class MapObject : MonoBehaviour
{
    [Header("Drawing")]
    [SerializeField] private Sprite sprite;
    [SerializeField] private RectTransform minimap;
    [SerializeField] private Canvas canvas;

    [Header("Mapping")] 
    [SerializeField] private float xOffset = 68.0f;
    [SerializeField] private float zOffset = 74.0f;
    [SerializeField] private float mapLength = 130.0f;

    private GameObject _newSprite;
    
    private void Start()
    {
        _newSprite = new GameObject(); // make a new object for sprite
        Image newImage = _newSprite.AddComponent<Image>(); // give it an image component to draw sprite
        newImage.sprite = sprite; // set the sprite of the image
        _newSprite.GetComponent<RectTransform>().SetParent(canvas.transform); // put the sprite in the canvas
        _newSprite.SetActive(true); // make sure the sprite is enabled in the scene
    }

    private void Update()
    {
        Vector3 currentObjectPosition = transform.position;
        
        Vector2 normalizedObjectPosition = new Vector2(
            (currentObjectPosition.x + xOffset) / mapLength,
            (currentObjectPosition.z + zOffset) / mapLength);
        
        Vector3[] corners = new Vector3[4];
        minimap.GetWorldCorners(corners);

        float rectWidth = corners[3].x - corners[0].x; // width = bottom right corner x - bottom left corner x
        
        // calculate position in minimap by
        // multiplying normalized object position * the map size + the bottom left corner
        _newSprite.GetComponent<RectTransform>().position = new Vector3(
            normalizedObjectPosition.x * rectWidth + corners[0].x,
            normalizedObjectPosition.y * rectWidth + corners[0].y,
            0);
    }
}
