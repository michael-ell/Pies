using System;
using Codell.Pies.Common.Security;
using Codell.Pies.Common;

namespace Codell.Pies.Web.Security
{
    public class User : IUser
    {
        public static IUser Anonymous = new User("", "");

        public User(string data)
        {
            Parse(data);
        }
        
        public User(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; private set; }

        public string Name { get; private set; }

        private void Parse(string data)
        {
            if (data.IsNotEmpty() && data.Contains(";"))
            {
                var parts = data.Split(';');
                if (parts.Length > 0) Id = parts[0];
                if (parts.Length > 1) Name = parts[1];
            }            
        }

        public override string ToString()
        {
            return String.Format("{0};{1}", Id, Name);
        }
    }
}