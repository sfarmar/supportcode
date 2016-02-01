﻿using NServiceBus;
using NServiceBus.Log4Net;
using Swiftness.Test.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swiftness.Test.Sender
{
    public class EndpointConfig : IConfigureThisEndpoint, AsA_Server
    {
        public EndpointConfig()
        {
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
        }

        public void Customize(BusConfiguration configuration)
        {
            //ConventionsBuilder conventions = configuration.Conventions();
            //conventions.DefiningEventsAs(t => typeof(IEvent).IsAssignableFrom(t) || t.Namespace != null && t.Namespace.StartsWith("ServiceControl.Contracts"));

            configuration.UsePersistence<NHibernatePersistence>();
            configuration.UseSerialization<JsonSerializer>();
            configuration.Transactions().Enable();
            //configuration.Transactions().EnableDistributedTransactions();
            //configuration.DisableFeature<Audit>();

            NServiceBus.Logging.LogManager.Use<Log4NetFactory>();

            configuration.UseContainer<StructureMapBuilder>();

            Log4NetConfig logConfig = new Log4NetConfig();
            logConfig.Start();
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            System.Diagnostics.EventLog.WriteEntry(System.Diagnostics.Process.GetCurrentProcess().ProcessName, String.Format("Unhandled exception - {0}", (e.ExceptionObject as Exception).ToString()), System.Diagnostics.EventLogEntryType.Error);
        }
    }
}
