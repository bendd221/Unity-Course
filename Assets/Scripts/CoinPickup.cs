using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int pointsPerCoin = 200;
    GameSession gameSession;

    private void Start() {
        gameSession = FindObjectOfType<GameSession>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.layer == LayerMask.NameToLayer("Player") && other.GetType() == typeof(CapsuleCollider2D))
        {
            gameSession.increaseScore(pointsPerCoin);
            FindObjectOfType<AudioManager>().playCoinSoundEffect();
            Destroy(gameObject);
        }
    }
}
