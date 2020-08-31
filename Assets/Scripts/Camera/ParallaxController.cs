using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    Transform cameraTransform;
    Vector3 lastPosition;

    public Vector2 parallaxMultiplier = new Vector2(.8f, 0);

    Sprite backgroundSprite;
    Texture2D backgroundTexture;
    float textureUnitSizeX;
    float textureUnitSizeY;

    float offset;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = Camera.main.transform;
        lastPosition = cameraTransform.position;

        backgroundSprite = GetComponent<SpriteRenderer>().sprite;
        backgroundTexture = backgroundSprite.texture;
        textureUnitSizeX = backgroundTexture.width / backgroundSprite.pixelsPerUnit;
        textureUnitSizeY = backgroundTexture.height / backgroundSprite.pixelsPerUnit;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //make background follow camera
        Vector3 positionDifference = cameraTransform.position - lastPosition;
        transform.position += new Vector3(positionDifference.x * parallaxMultiplier.x, positionDifference.y * parallaxMultiplier.y, positionDifference.z);
        lastPosition = cameraTransform.position;

        // Infinite scroll
        if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
        {
            offset = (cameraTransform.transform.position.x - transform.position.x) % textureUnitSizeX;
            transform.position = new Vector3(cameraTransform.position.x + offset, transform.position.y);
        }

        if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX)
        {
            offset = (cameraTransform.transform.position.y - transform.position.y) % textureUnitSizeY;
            transform.position = new Vector3(cameraTransform.position.x + offset, transform.position.y);
        }
    }
}