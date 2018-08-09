namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class SAMonthSalary
    {
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (object.ReferenceEquals(this, obj))
            {
                return true;
            }
            if (base.GetType() != obj.GetType())
            {
                return false;
            }
            return (this.Id == ((SAMonthSalary) obj).Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }

        public virtual decimal Cost { get; set; }

        public virtual string Id { get; set; }

        public virtual string ItemId { get; set; }

        public virtual int Month { get; set; }

        public virtual string UserCode { get; set; }

        public virtual int Year { get; set; }
    }
}

