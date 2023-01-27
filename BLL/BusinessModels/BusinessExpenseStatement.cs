namespace BLL.BusinessModels;

public class BusinessExpenseStatement
{
    public int Id { get; set; }
    public decimal Amount { get; set; }
    public DateTime DateTime { get; set; }
    //public int AccountId { get; set; }
    public int ExpenseCategoryId { get; set; }
    public string ExpenseCategoryName { get; set; }

    public override string ToString()
    {
        return $"Date - {DateTime} Amount - {Amount} Category - {ExpenseCategoryName}";
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        
        if (ReferenceEquals(this, obj))
            return true;
        
        if (this.GetType() != obj.GetType()) 
            return false;

        return this.Equals(obj as BusinessExpenseStatement);
    }

    protected bool Equals(BusinessExpenseStatement other)
    {
        return Id == other.Id && Amount == other.Amount && DateTime.Equals(other.DateTime) &&
               ExpenseCategoryId == other.ExpenseCategoryId && ExpenseCategoryName == other.ExpenseCategoryName;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Amount, DateTime, ExpenseCategoryId, ExpenseCategoryName);
    }
}