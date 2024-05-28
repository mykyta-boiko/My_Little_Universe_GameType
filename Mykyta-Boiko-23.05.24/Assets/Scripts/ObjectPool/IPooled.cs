namespace ObjectPool
{
    public interface IPooled
    {
        ObjectPooler.ObjectInfo.ObjectTypes Type { get; }
    }
}