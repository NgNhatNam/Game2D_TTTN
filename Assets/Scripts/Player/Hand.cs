using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public Transform PlayerTransform; // Tham chiếu đến Player
    public Animator attackAnim; // Tham chiếu đến Animator của AttackAnim
    public Collider2D attackTrigger; // Trigger collider để phát hiện Enemy
    public float knockbackTime = 0.5f;

    private Camera mainCam;
    private Vector3 mousePos;
    private PlayerMovement playerFlip;

    // Start is called before the first frame update
    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        playerFlip = PlayerTransform.GetComponent<PlayerMovement>(); // Lấy script PlayerMovement từ Player
        attackTrigger.enabled = false; // Ban đầu, trigger collider bị vô hiệu hóa
    }

    // Update is called once per frame
    void Update()
    {
        // Lấy vị trí chuột trong không gian thế giới
        mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);

        // Tính toán góc xoay của tay
        Vector3 rotation = mousePos - transform.position;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        // Áp dụng hướng flip của nhân vật vào tay
        if (playerFlip.GetFacingDirection() < 0)
        {
            // Nếu nhân vật đang nhìn trái, điều chỉnh góc xoay
            rotZ += 180;
        }

        // Áp dụng góc xoay
        transform.rotation = Quaternion.Euler(0, 0, rotZ);
    }

    // Coroutine để xử lý animation và trigger collider
    private IEnumerator Attack()
    {
        // Bật trigger collider
        attackTrigger.enabled = true;

        // Kích hoạt animation attack
        attackAnim.SetBool("isAttack", true);

        // Chờ trong thời gian animation attack
        yield return new WaitForSeconds(knockbackTime); // Điều chỉnh thời gian phù hợp với animation

        // Tắt trigger collider
        attackTrigger.enabled = false;

        // Kết thúc animation attack
        attackAnim.SetBool("isAttack", false);
        //yield return new WaitForSeconds(knockbackTime);
    }

  
    //-------------------------------------------------------------------------
    // Hàm công khai để kích hoạt animation attack
    public void isAttack()
    {
        //attackAnim.SetBool("isAttack", true); // Đặt "Attack" thành true
        StartCoroutine (Attack());
    }
    public void FinishAttack()
    {
            attackAnim.SetBool("isAttack", false); // Đặt "Attack" thành false
    }







}
