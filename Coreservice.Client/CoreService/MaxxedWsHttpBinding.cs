﻿using System;
using System.ServiceModel;
using System.Xml;

namespace Coreservice.Client.CoreService
{
    public class MaxxedWsHttpBinding : WSHttpBinding
    {
        public MaxxedWsHttpBinding(SecurityMode securityMode)
            : base(securityMode)
        {
            initializeMaxSettings();
        }

        public MaxxedWsHttpBinding()
            : base()
        {
            initializeMaxSettings();
        }

        private void initializeMaxSettings()
        {
            //this.MaxBufferSize = Int32.MaxValue;
            this.MaxBufferPoolSize = Int32.MaxValue;
            this.MaxReceivedMessageSize = Int32.MaxValue;
            this.ReaderQuotas = new XmlDictionaryReaderQuotas
            {
                MaxDepth = Int32.MaxValue,
                MaxStringContentLength = Int32.MaxValue,
                MaxArrayLength = Int32.MaxValue
            };
        }

    }
}
