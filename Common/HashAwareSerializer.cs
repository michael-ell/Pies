using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Codell.Pies.Common
{
    public class HashAwareSerializer : ISerializationSurrogate
    {
        public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
        {
            if (info == null || obj == null) return;

            var type = obj.GetType();
            var toSerialize = type.GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                  .Where(pi => !pi.GetCustomAttributes(typeof(ExcludeForHashAttribute), true).Any());
            foreach (var pi in toSerialize)
            {
                info.AddValue(pi.Name, pi.GetValue(obj, null));
            }
        }

        public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
        {
            throw new System.NotImplementedException();
        }
    }
}