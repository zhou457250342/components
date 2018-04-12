namespace CPTS.Common.Socketing.BufferManagement
{
    public interface IBufferPool : IPool<byte[]>
    {
        int BufferSize { get; }
    }
}
