﻿using ErrorHandling.UseCases;

namespace ErrorHandling.Gateways
{
    public class FailingUserGateway : IUserGateway
    {
        private static bool userNameExists = false;

        public bool DoesExist(string userName)// Expected sql unique name constraint exception handled by business requirements analysis.
        {
            userNameExists = !userNameExists;
            return userNameExists;
        }

        public void Save(User user)
        {
            try
            {
                SaveOrUpdate(user);
            }
            catch (DeadlockException)// Expected exception handled by gateway (use case doesn't even know it).
            {
                SaveOrUpdate(user);
            }

            throw new UnexpectedSqlException();// Unexpected exception to be handled globally.
        }

        private void SaveOrUpdate(User user)
        {

        }
    }
}