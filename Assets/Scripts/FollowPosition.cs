using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPosition : MonoBehaviour
{
    [SerializeField] private Transform followPos;
    [SerializeField] private float followSpeed;

    public void SetFollowPos(Transform followPos)
    {
        this.followPos = followPos;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, followPos.position, followSpeed * Time.deltaTime);
    }
}