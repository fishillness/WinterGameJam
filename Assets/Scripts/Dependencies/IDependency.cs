using UnityEngine;

namespace WinterGameJam
{
    public interface IDependency<T>
    {
        void Construct(T obj);
    }
}
