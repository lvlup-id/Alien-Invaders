// Handle pooling for all explosion objects
public class ExplosionPool : ObjectPool
{
    public static ExplosionPool Instance;

    private void Awake()
    {
        Instance = this;
    }
}