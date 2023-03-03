public class Enemy : Entity
{
    protected override void OnDie()
    {
        base.OnDie();
        GameController.Instance.enemies.Remove(this);
    }
}
