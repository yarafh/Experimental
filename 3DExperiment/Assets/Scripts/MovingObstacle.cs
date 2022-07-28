using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField] private float obstacleSpeed;
    [SerializeField] private Vector3 startPoint;
    [SerializeField] private Vector3 endPoint;
  
    void FixedUpdate()
    {
        transform.position=Vector3.Lerp(startPoint, endPoint, Mathf.PingPong(Time.time*obstacleSpeed,1));
    }
}
