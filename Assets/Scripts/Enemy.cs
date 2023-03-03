using UnityEngine.Events;

public class Enemy : Entity
{
    public UnityAction OnDieEvent;

    private void OnEnable()
    {
        OnDieEvent += OnDie;
    }

    private void OnDisable()
    {
        OnDieEvent -= OnDie;
    }

    protected override void OnDie()
    {
        GameController.Instance.enemies.Remove(this);
        base.OnDie();
    }
}
