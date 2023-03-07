public class Enemy : Entity
{
    public int score = 10;
    protected override void OnDie()
    {
        base.OnDie();
        GameController.Instance.OnEnemyDie(this);
    }
}
