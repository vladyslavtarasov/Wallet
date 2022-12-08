namespace BLL.BusinessModels;

public class BusinessIncomeStatement
{
    public int Id { get; set; }
    //public int AccountId { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    public int IncomeCategoryId { get; set; }
    public string IncomeCategoryName { get; set; }
    
    public override string ToString()
    {
        return $"Date - {DateTime} Amount - {Amount} Category - {IncomeCategoryName}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        
        if (ReferenceEquals(this, obj))
            return true;
        
        if (this.GetType() != obj.GetType()) 
            return false;

        return this.Equals(obj as BusinessIncomeStatement);
    }

    protected bool Equals(BusinessIncomeStatement other)
    {
        return Id == other.Id && Amount == other.Amount && DateTime.Equals(other.DateTime) &&
               IncomeCategoryId == other.IncomeCategoryId && IncomeCategoryName == other.IncomeCategoryName;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Amount, DateTime, IncomeCategoryId, IncomeCategoryName);
    }
}