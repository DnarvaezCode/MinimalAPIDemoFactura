﻿using System.Linq.Expressions;

namespace APIFacturaV1.Specification.Evaluator
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> Includes { get; }
    }
}
