using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Enemy : MonoBehaviour, IKillable, IDamagable<float>
{
    [Header("Stats")]
    public float health;
    public float maxHealth;
    [HideInInspector] public float enemySpeed;
    public float weakEnemySpeed;
    public float flyingEnemySpeed;
    public float heavyEnemySpeed;


    public enum EnemyType { Weak, Flying, Heavy}
    [Header("Enemy type")]
    public EnemyType enemyType = EnemyType.Weak;

    void Start()
    {
        if (enemyType == EnemyType.Weak) maxHealth = 1; health = 1; enemySpeed = weakEnemySpeed; 
        if (enemyType == EnemyType.Flying) maxHealth = 2; health = 2; enemySpeed = flyingEnemySpeed;
        if (enemyType == EnemyType.Heavy) maxHealth = 3; health = 3; enemySpeed = heavyEnemySpeed;
    }

    public void Die()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void TakeDamage(float damage)
    {
        health -= 1;
    }
}