using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

public class CloudController : MonoBehaviour
{
    [Header("Speed Values")]
    [SerializeField]
    public Speed horizontalSpeedRange;

    [SerializeField]
    public Speed verticalSpeedRange;

    public float verticalSpeed;
    public float horizontalSpeed;

    [SerializeField]
    public Boundary boundary;

    // Start is called before the first frame update
	private void Start()
    {
        Reset();
    }

    // Update is called once per frame
	private void Update()
    {
        Move();
        CheckBounds();
    }

    /// <summary>
    /// This method moves the ocean down the screen by verticalSpeed
    /// </summary>
	private void Move()
    {
        Vector2 newPosition = new Vector2(horizontalSpeed * Time.deltaTime, verticalSpeed * Time.deltaTime);
        Vector2 currentPosition = transform.position;

        currentPosition -= newPosition;
        transform.position = currentPosition;
    }

    /// <summary>
    /// This method resets the ocean to the resetPosition
    /// </summary>
	private void Reset()
    {
        horizontalSpeed = Random.Range(horizontalSpeedRange.min, horizontalSpeedRange.max);
        verticalSpeed = Random.Range(verticalSpeedRange.min, verticalSpeedRange.max);

        float randomXPosition = Random.Range(boundary.left, boundary.right);
        transform.position = new Vector2(randomXPosition, Random.Range(boundary.top, boundary.top + 2.0f));
    }

    /// <summary>
    /// This method checks if the ocean reaches the lower boundary
    /// and then it Resets it
    /// </summary>
	private void CheckBounds()
    {
        if (transform.position.y <= boundary.bottom)
        {
            Reset();
        }
    }
}
