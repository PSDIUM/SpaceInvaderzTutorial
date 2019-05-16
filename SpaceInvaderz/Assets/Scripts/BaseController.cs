using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour {

    private int health = 2;

    [SerializeField]SpriteRenderer healthSprite;
    [SerializeField] Sprite[] spriteStages;

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.tag.Equals("Enemy")) {
            Damage();
            Destroy(col.gameObject);
        }
    }

    private void Damage() {
        if (health>0) {
            health--;
            healthSprite.sprite = spriteStages[health];
        }
        if (health==0) {
            GameManager.Instance.EndGame("INFECTED!");
        }
    }
}
