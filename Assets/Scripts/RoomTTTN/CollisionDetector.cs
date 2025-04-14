using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CollisionDetector : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject; // Tham chiếu đến GameObject của player

    [SerializeField]
    private UnityEvent collisionEntered;

    [SerializeField]
    private UnityEvent collisionExit;

   

    private void OnCollisionEnter2D(Collision2D col)
    {
        // Kiểm tra nếu vật thể va chạm là player được chỉ định
        if (col.gameObject.CompareTag("Player"))
        {
            collisionEntered?.Invoke();
        }
    }

    private void OnCollisionExit2D(Collision2D col)
    {
        // Kiểm tra nếu vật thể ngừng va chạm là player được chỉ định
        if (col.gameObject.CompareTag("Player"))
        {
            collisionExit?.Invoke();
        }
    }
}
