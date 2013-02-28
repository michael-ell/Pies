using System;
using AutoMapper;

namespace Codell.Pies.Testing.FluentFixtures
{
    public class Creator<T> : FixtureContextAware where T: class
    {
        private T _creation;

        public T Creation
        {
            get { return _creation; }
            protected set 
            {
                if (Current.Get<T>() != value)
                    Current.Push(value);
                _creation = value; 
            }
        }

        public Creator(IFixtureContext context, T creation) : base(context)
        {
            Creation = creation;
        }

        public T Clone()
        {
            if (Mapper.FindTypeMapFor<T, T>() == null)
            {
                Mapper.CreateMap<T, T>();
            }
            return Mapper.Map<T, T>(Creation);
        }

        public static implicit operator T(Creator<T> creator)
        {
            if (creator._creation == null) 
                throw new Exception(String.Format("Creation of {0} is null, it probably shouldn't be.", typeof(T)));
            creator.Current.Pop<T>();
            return creator._creation;
        }
    }
}