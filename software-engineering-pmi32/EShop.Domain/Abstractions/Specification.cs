using System.Linq.Expressions;

namespace EShop.Domain.Abstractions;

public abstract class Specification<T> 
{
    public int PageSize { get; private set; }
    public int PageNumber { get; private set; }
    public bool? IsSplitQuery { get; set; }
    public virtual Expression<Func<T, bool>>? Criteria { get; }
    public List<Expression<Func<T, object>>> Includes { get; } = new();
    public List<string> IncludeStrings { get; } = new();
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }
    
    protected Specification(Expression<Func<T, bool>>? criteria)
    {
        Criteria = criteria;
    }
    
    protected Specification(Specification<T>  specification)
    {
        Criteria = specification.Criteria;
        Includes = specification.Includes;
        OrderBy = specification.OrderBy;
        OrderByDescending = specification.OrderByDescending;
    }
    
    protected void AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
    }
    
    protected void AddInclude(string includeString)
    {
        IncludeStrings.Add(includeString);
    }
    
    protected void AddOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
    }
    
    protected void AddOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
    {
        OrderByDescending = orderByDescendingExpression;
    }
    
    protected void AddPagination(int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
    }
}