using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkFollowTheCursor : MonoBehaviour
{
    public RectTransform canvasRect;
    private RectTransform rect;
    public Animator animator;

    public float speed = 18f; 
    void Start()
    {
        rect = GetComponent<RectTransform>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 mousePos;

        // Convert mouse position to Canvas local space
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRect,
            Input.mousePosition,
            null,
            out mousePos
        );

        // Target horizontal point only
        Vector2 targetPos = new Vector2(mousePos.x, rect.anchoredPosition.y);


        // Calculate horizontal movement input
        float input = targetPos.x - rect.anchoredPosition.x;

        // Move at constant speed
        rect.anchoredPosition = Vector2.MoveTowards(
            rect.anchoredPosition,
            targetPos,
            speed * Time.deltaTime
        );

        
        if (Mathf.Abs(input) > 0.01f)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (input > 0.01f)
        {
            rect.localScale = new Vector3(1, 1, 1); 
        }
        else if (input < -0.01f)
        {
            rect.localScale = new Vector3(-1, 1, 1); 
        }
    }
}
