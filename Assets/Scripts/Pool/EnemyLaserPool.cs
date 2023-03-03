public class EnemyLaserPool : SimplePool
{
    public static EnemyLaserPool Instance;

    private void Awake()
    {
        Instance = this;
    }
}