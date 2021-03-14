// using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
  // config params
  [SerializeField] float moveSpeed;
  [SerializeField] float laserSpeed;
  [Range(0f, 360f)] [SerializeField] float laserAngle;
  [Range(0f, 1f)] [SerializeField] float offset;
  [SerializeField] Laser laserPrefab;
  [SerializeField] int autoShootFrequency;

  // state
  private float xMin;
  private float yMin;
  private float xMax;
  private float yMax;

  // Start is called before the first frame update
  void Start()
  {
    SetUpMoveBoundaries();
  }

  private void SetUpMoveBoundaries()
  {
    var minPoints = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
    var maxPoints = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
    var spriteSize = GetComponent<SpriteRenderer>().size;
    var xOffset = spriteSize.x / 2 + offset;
    var yOffset = spriteSize.y / 2 + offset;
    xMin = minPoints.x + xOffset;
    yMin = minPoints.y + yOffset;
    xMax = maxPoints.x - xOffset;
    yMax = maxPoints.y - yOffset;
  }

  // Update is called once per frame
  void Update()
  {
    Move();
    if (Random.Range(0, autoShootFrequency) == 0)
    {
      FireLaser();
    }
  }
  private void FireLaser()
  {
    var laser = Instantiate(laserPrefab, transform.position, Quaternion.Euler(0, 0, laserAngle));
    laser.SetSpeed(laserSpeed);
  }

  private void Move()
  {
    var deltaFactor = Time.deltaTime * moveSpeed;
    var deltaX = Input.GetAxis("Horizontal") * deltaFactor;
    var deltaY = Input.GetAxis("Vertical") * deltaFactor;
    var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax);
    var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax);
    transform.position = new Vector2(newXPos, newYPos);
  }


}
