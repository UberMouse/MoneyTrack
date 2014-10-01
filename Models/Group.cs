using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MoneyTrack.Models
{
    public class Group
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        protected bool Equals(Group other)
        {
            return string.Equals(Name, other.Name) && string.Equals(Color, other.Color);
        }

        public override string ToString()
        {
            return string.Format("Name: {0}, Color: {1}, Id: {2}", Name, Color, Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Group) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Name.GetHashCode()*397) ^ Color.GetHashCode();
            }
        }
    }
}