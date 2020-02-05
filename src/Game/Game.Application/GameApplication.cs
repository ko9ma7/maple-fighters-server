﻿using CommonTools.Coroutines;
using CommonTools.Log;
using ServerCommon.Application;
using ServerCommon.Configuration;
using ServerCommon.Logging;
using ServerCommunicationInterfaces;

namespace Game.Application
{
    public class GameApplication : ServerApplicationBase
    {
        public GameApplication(IServerConnector serverConnector, IFiberProvider fiberProvider)
            : base(serverConnector, fiberProvider)
        {
            ServerConfiguration.Setup();
            ServerSettings.InboundPeer.LogEvents = true;
            ServerSettings.InboundPeer.Operations.LogRequests = true;
            ServerSettings.InboundPeer.Operations.LogResponses = true;
            ServerSettings.OutboundPeer.LogEvents = true;
            ServerSettings.OutboundPeer.Operations.LogRequests = true;
            ServerSettings.OutboundPeer.Operations.LogResponses = true;
            ServerSettings.Databases.Mongo.Url = "mongodb://localhost:27017/maple_fighters";
        }

        protected override void OnStartup()
        {
            base.OnStartup();

            AddCommonComponents();

            LogUtils.Log("OnStartup");
        }

        protected override void OnShutdown()
        {
            base.OnShutdown();

            LogUtils.Log("OnShutdown");
        }

        protected override ILogger GetLogger()
        {
            return new Logger();
        }

        protected override ITimeProvider GetTimeProvider()
        {
            return new TimeProvider();
        }
    }
}