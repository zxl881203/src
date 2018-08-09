namespace cn.justwin.Domain.Entities
{
    using System;
    using System.Runtime.CompilerServices;

    public class SASalaryBooks
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
            return (this.Id == ((SASalaryBooks) obj).Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        public override string ToString()
        {
            return this.Id.ToString();
        }

        public virtual string Id { get; set; }

        public virtual DateTime InputDate { get; set; }

        public virtual string InputUser { get; set; }

        public virtual bool IsValid { get; set; }

        public virtual string Name { get; set; }

        public virtual string Note { get; set; }
    }
}

