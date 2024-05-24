using ResourceLogic;
namespace ObjectPool
{
    public interface IPooled
    {
        ResourceType Type { get; }
    }
}