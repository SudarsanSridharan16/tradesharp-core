/***************************************************************************** 
* Copyright 2016 Aurora Solutions 
* 
*    http://www.aurorasolutions.io 
* 
* Aurora Solutions is an innovative services and product company at 
* the forefront of the software industry, with processes and practices 
* involving Domain Driven Design(DDD), Agile methodologies to build 
* scalable, secure, reliable and high performance products.
* 
* TradeSharp is a C# based data feed and broker neutral Algorithmic 
* Trading Platform that lets trading firms or individuals automate 
* any rules based trading strategies in stocks, forex and ETFs. 
* TradeSharp allows users to connect to providers like Tradier Brokerage, 
* IQFeed, FXCM, Blackwood, Forexware, Integral, HotSpot, Currenex, 
* Interactive Brokers and more. 
* Key features: Place and Manage Orders, Risk Management, 
* Generate Customized Reports etc 
* 
* Licensed under the Apache License, Version 2.0 (the "License"); 
* you may not use this file except in compliance with the License. 
* You may obtain a copy of the License at 
* 
*    http://www.apache.org/licenses/LICENSE-2.0 
* 
* Unless required by applicable law or agreed to in writing, software 
* distributed under the License is distributed on an "AS IS" BASIS, 
* WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
* See the License for the specific language governing permissions and 
* limitations under the License. 
*****************************************************************************/


﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradeHub.MarketDataProvider.Fxcm.ValueObject
{
    public class ConnectionParameters
    {
        private readonly string _loginId;
        private readonly string _password;
        private readonly string _account;
        private readonly string _connection;
        private readonly string _url;
        private string _sessionId;
        private string _pin;

        /// <summary>
        /// Argument constructor
        /// </summary>
        /// <param name="loginId"></param>
        /// <param name="password"></param>
        /// <param name="account"></param>
        /// <param name="connection"></param>
        /// <param name="url"></param>
        public ConnectionParameters(string loginId, string password, string account, string connection, string url)
        {
            _loginId = loginId;
            _password = password;
            _account = account;
            _connection = connection;
            _url = url;
        }

        public string LoginId
        {
            get { return _loginId; }
        }

        public string Password
        {
            get { return _password; }
        }

        public string Account
        {
            get { return _account; }
        }

        public string Connection
        {
            get { return _connection; }
        }

        public string SessionId
        {
            get { return _sessionId; }
            set { _sessionId = value; }
        }

        public string Pin
        {
            get { return _pin; }
            set { _pin = value; }
        }

        public string Url
        {
            get { return _url; }
        }
    }
}
