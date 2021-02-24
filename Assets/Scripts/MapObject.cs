using UnityEngine;
using UnityEngine.UI;

public class MapObject : MonoBehaviour
{
    [Header("Drawing")]
    [SerializeField] private Sprite sprite;
    [SerializeField] private RectTransform minimap;
    [SerializeField] private Canvas canvas;
    [SerializeField] [Range(0.0f, 2.0f)] private float scale;
    [SerializeField] private Color color = Color.white;

    [Header("Mapping")] 
    [SerializeField] private float xOffset = 68.0f;
    [SerializeField] private float zOffset = 74.0f;
    [SerializeField] private float mapLength = 130.0f;

    private GameObject _newSprite;
    private Image _newImage;
    private RectTransform _spriteRectTransform;
    
    private void Start()
    {
        _newSprite = new GameObject(); // make a new object for sprite
        _newImage = _newSprite.AddComponent<Image>(); // give it an image component to draw sprite
        _newImage.sprite = sprite; // set the sprite of the image

        _spriteRectTransform = _newSprite.GetComponent<RectTransform>(); // get the RectTransform

        _spriteRectTransform.SetParent(canvas.transform); // put the sprite in the canvas
        _newSprite.SetActive(true); // make sure the sprite is enabled in the scene

    }

    private void Update()
    {
        Vector3 currentObjectPosition = transform.position;

        // normalize the position of the object in the map to be between 0 and 1        
        Vector2 normalizedObjectPosition = new Vector2(
            (currentObjectPosition.x + xOffset) / mapLength,
            (currentObjectPosition.z + zOffset) / mapLength);
        
        // get the coordinates of the 4 corners of the rect transform
        Vector3[] corners = new Vector3[4];
        minimap.GetWorldCorners(corners);

        float rectWidth = corners[3].x - corners[0].x; // width = bottom right corner x - bottom left corner x

        // calculate position in minimap by
        // multiplying normalized object position * the map size + the bottom left corner
        _spriteRectTransform.position = new Vector3(
            corners[2].x - normalizedObjectPosition.x * rectWidth,
            corners[2].y - normalizedObjectPosition.y * rectWidth,
            0);

        _spriteRectTransform.localScale = Vector2.one * scale;
        _newImage.color = color;
    }
}
