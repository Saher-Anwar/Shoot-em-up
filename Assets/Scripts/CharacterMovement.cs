using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 4f;

    private CharacterController characterController;
    private float horizontal;
    private float vertical;
    private Vector3 mousePos;
    private const float angleOffsetInDeg = 90f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // get mouse position for player rotation
        mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        #region PlayerMovement
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        characterController.SimpleMove(new Vector3(horizontal, 0, vertical) * speed);
        #endregion
    }

    private void FixedUpdate()
    {
        Vector3 direction = mousePos - Camera.main.WorldToViewportPoint(transform.position);
        FaceMouse(direction);
    }

    private void FaceMouse(Vector3 mouseDirection)
    {
        // get angle between mouse and player and apply rotation
        float angle = -Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg + angleOffsetInDeg;
        transform.rotation = Quaternion.Euler(0, angle, 0);
    }
}
