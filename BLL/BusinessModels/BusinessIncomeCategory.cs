namespace BLL.BusinessModels;

public class BusinessIncomeCategory
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    //public int UserId { get; set; }
    public int AccountId { get; set; }
    public string AccountName { get; set; } = null!;

    public override string ToString()
    {
        return $"Name: {Name}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        
        if (ReferenceEquals(this, obj))
            return true;
        
        if (this.GetType() != obj.GetType()) 
            return false;

        return this.Equals(obj as BusinessIncomeCategory);
    }

    protected bool Equals(BusinessIncomeCategory other)
    {
        return Id == other.Id && Name == other.Name && AccountId == other.AccountId && AccountName == other.AccountName;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, AccountId, AccountName);
    }
}