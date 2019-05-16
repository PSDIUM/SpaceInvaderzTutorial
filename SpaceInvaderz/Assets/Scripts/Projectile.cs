using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [Header("Physics Details")]
    [SerializeField] private float speed = 5;
    [SerializeField] private float lifeTime = 1f;

    [Header("Sprite Options")]
    [SerializeField] private Sprite[] projectileSprites;
    private SpriteRenderer spriteRenderer;

    private Vector2 dir;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        int spriteIndex = Random.Range(0, projectileSprites.Length);
        spriteRenderer.sprite = projectileSprites[spriteIndex];
        StartCoroutine(DeathTimer());
    }

    public void Initialise(Transform player, float speed) {
        dir = InputManager.Instance.GetMouseWorldPosition() - player.position;
        dir.Normalize();
        this.speed = speed;
    }

    private void Update() {
        Movement();
    }

    private void Movement() {
        Vector2 velocity = dir * speed * Time.deltaTime;
        transform.position += new Vector3(velocity.x, velocity.y, 0);
    }

    private IEnumerator DeathTimer() {
        float elapsedTime = 0;

        while (elapsedTime<lifeTime) {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Destroy(this.gameObject);
    }

}
