using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollObject : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    public enum ScrollDirection
    {
        Left,
        Right
    }
    public ScrollDirection scrollDirection;

    void Update()
    {
        switch (scrollDirection)
        {
            case ScrollDirection.Left:
                float posX = transform.position.x - scrollSpeed * Time.deltaTime;
                transform.position = new Vector3(posX, transform.position.y, transform.position.z);
                break;
            case ScrollDirection.Right:
                posX = transform.position.x + scrollSpeed * Time.deltaTime;
                transform.position = new Vector3(posX, transform.position.y, transform.position.z);
                break;
        }
    }
}