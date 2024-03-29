﻿namespace DataAccessLayer.Contracts
{
    public interface IUserRequestRepository
    {
        DateTime? GetLatestRequestTime(string userId);
        void SetLatestRequestTime(string userId, DateTime timestamp);
        void DeleteLatestRequestTime(string userId);
    }
}
