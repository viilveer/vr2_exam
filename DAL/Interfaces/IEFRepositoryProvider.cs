﻿using System;


namespace DAL.Interfaces
{
    public interface IEFRepositoryProvider
    {
        IDbContext DbContext { get; set; }
        IBaseRepository<T> GetRepositoryForEntityType<T>() where T : class;
        T GetRepository<T>(Func<IDbContext, object> factory = null) where T : class;
        void SetRepository<T>(T repository);

    }
}