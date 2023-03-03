public class PlayerLaserPool : SimplePool
{
    public static PlayerLaserPool Instance;

    private void Awake()
    {
        Instance = this;
    }
}