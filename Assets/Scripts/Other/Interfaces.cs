using System.Collections;

public interface IKillable
{
    void Die();
}

public interface IDamagable<T>
{
    void TakeDamage(T damage);
}

public interface ICollectable
{
    void OnCollectItem();

    IEnumerator OnPowerUpCollected();
}

public interface IRestartable
{
    void Restart();
}