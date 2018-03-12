﻿using CommonTools.Log;
using ComponentModel.Common;
using Database.Common.Components;
using Database.Common.TablesDefinition;
using ServiceStack.OrmLite;
using UserProfile.Server.Common;

namespace UserProfile.Service.Application.Components
{
    internal class DatabaseUserProfileCreator : Component, IDatabaseUserProfileCreator
    {
        private IDatabaseConnectionProvider databaseConnectionProvider;

        protected override void OnAwake()
        {
            base.OnAwake();

            databaseConnectionProvider = Components.GetComponent<IDatabaseConnectionProvider>().AssertNotNull();
        }

        public void Create(int userId)
        {
            using (var db = databaseConnectionProvider.GetDbConnection())
            {
                var user = new UserProfilesTableDefinition
                {
                    UserId = userId,
                    CurrentServer = (byte)ServerType.Login,
                    IsConnected = (byte)ConnectionStatus.Disconnected,
                    LocalId = default(int)
                };
                db.Insert(user);
            }
        }
    }
}