namespace BLL.BusinessModels;

public class BusinessAccount
{
    public int Id { get; set; }
    public decimal Balance { get; set; }
    public string Name { get; set; } = null!;
    
    public string UserName { get; set; } = null!;
    public int UserId { get; set; }

    public override string ToString()
    {
        return $"Name: {Name} | Balance: {Balance}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        
        if (ReferenceEquals(this, obj))
            return true;
        
        if (this.GetType() != obj.GetType()) 
            return false;

        return this.Equals(obj as BusinessAccount);
    }

    protected bool Equals(BusinessAccount other)
    {
        return Id == other.Id && Balance == other.Balance && Name == other.Name && UserId == other.UserId;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Balance, Name, UserId);
    }
}