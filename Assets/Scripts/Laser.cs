using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
  // state
  private Vector2 velocity;
  private float yMax;

  // references
  private Camera gameCamera;

  // Start is called before the first frame update
  void Start()
  {
    gameCamera = Camera.main;
    SetUpMoveBoundaries();
  }
  private void SetUpMoveBoundaries()
  {
    var maxPoints = gameCamera.ViewportToWorldPoint(new Vector2(1, 1));
    var spriteSize = GetComponent<SpriteRenderer>().size;
    var yOffset = spriteSize.y / 2;
    yMax = maxPoints.y + yOffset;
  }

  // Update is called once per frame
  void Update()
  {
    if (IsOutside())
    {
      Destroy(gameObject);
    }
    else
    {
      Move();
    }

  }

  private bool IsOutside()
  {
    return transform.position.y >= yMax;
  }

  private void Move()
  {
    var xDelta = velocity.x * Time.deltaTime;
    var yDelta = velocity.y * Time.deltaTime;
    var xNext = transform.position.x + xDelta;
    var yNext = transform.position.y + yDelta;

    transform.position = new Vector2(xNext, yNext);
  }

  public void SetSpeed(float laserSpeed)
  {
    var defaultX = 0;
    var defaultY = laserSpeed;

    var angleRadians = transform.rotation.eulerAngles.z * Mathf.Deg2Rad;
    var sin = Mathf.Sin(angleRadians);
    var cos = Mathf.Cos(angleRadians);

    var x = cos * defaultX - sin * defaultY;
    var y = sin * defaultX + cos * defaultY;

    velocity = new Vector2(x, y);
  }
}
