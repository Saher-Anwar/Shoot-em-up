using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class CharacterMovement : MonoBehaviour
{
    public float walkingSpeed = 4f;
    public float runningSpeed = 6f;
    public float dashDistance = 3f;
    public Animator animator;
    public AudioClip walkingAudioClip;
    public AudioClip runningAudioClip;

    private CharacterController characterController;
    private Character character;
    private float horizontal;
    private float vertical;
    private Vector3 mousePos;
    private const float angleOffsetInDeg = 90f;
    private new AudioSource audio;
    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        character = GetComponent<Character>();
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // get mouse position for player rotation
        mousePos = Camera.main.ScreenToViewportPoint(Input.mousePosition);

        #region PlayerMovement
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        animator.SetFloat("Speed", Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        // states are updated here. Based on the state, the functionality is handled by Character class
        if (Input.GetKey(KeyCode.LeftShift) && character.currentStamina > 0)
        {
            characterController.SimpleMove(new Vector3(horizontal, 0, vertical) * runningSpeed);
            character.staminaState = Character.STAMINA_STATE.ReducingStamina;
            if (!audio.isPlaying)
            {
                audio.clip = runningAudioClip;
                audio.PlayOneShot(audio.clip);
            }
        }
        else
        {
            characterController.SimpleMove(new Vector3(horizontal, 0, vertical) * walkingSpeed);
            character.staminaState = Character.STAMINA_STATE.RegeningStamina;
            if (!audio.isPlaying && Mathf.Abs(horizontal) + Mathf.Abs(vertical) > 0.01)
            {
                audio.clip = walkingAudioClip;
                audio.PlayOneShot(audio.clip);
            }
        }
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