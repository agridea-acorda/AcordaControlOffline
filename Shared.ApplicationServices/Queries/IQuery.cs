﻿namespace Agridea.Acorda.AcordaControlOffline.Shared.ApplicationServices.Queries
{
    public interface IQuery<TResult>
    {
    }

    public interface IQueryHandler<TQuery, TResult>
        where TQuery : IQuery<TResult>
    {
        TResult Handle(TQuery query);
    }
}