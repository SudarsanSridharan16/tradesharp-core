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
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Spring.Context.Support;
using TraceSourceLogger;
using TradeHub.Common.Core.DomainModels;
using TradeHub.DataDownloader.Common.ConcreteImplementation;
using TradeHub.DataDownloader.UserInterface.Common;
using TradeHub.DataDownloader.UserInterface.Common.Messages;
using TradeHub.DataDownloader.UserInterface.DataModule.View;

namespace TradeHub.DataDownloader.UserInterface.DataModule.ViewModel
{
    public class SecurityStatisticsViewModel : Security, INotifyPropertyChanged
    {
        #region Fields

        private Type _oType = typeof(SecurityStatisticsViewModel);

        /// <summary>
        /// Request Bars From Provider
        /// </summary>
        public ICommand RequestHistoricBars { get; set; }

        /// <summary>
        /// Saves The request Id
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// Saves The Name of Provider
        /// </summary>
        public string ProviderName { get; set; }

        /// <summary>
        /// Reference Of Bar setting View.
        /// </summary>
        public BarSettingView BarSettingView;

        /// <summary>
        /// Reference of Historic Bar Setting view
        /// </summary>
        public HistoricBarSettingView HistoricBarSettingView;

        #endregion
        
        public SecurityStatisticsViewModel()
        {
            try
            {
                var context = ContextRegistry.GetContext();
                BarSettingView = context.GetObject("BarSettingView") as BarSettingView;
                HistoricBarSettingView = context.GetObject("HistoricBarSettingView") as HistoricBarSettingView;
                if (BarSettingView != null) BarSettingView.BarSettingViewModel.StatisticsViewModel = this;
                var historicBarSettingView = this.HistoricBarSettingView;
                if (historicBarSettingView != null)
                    historicBarSettingView.HistoricBarViewModel.StatisticsViewModel = this;
                RequestHistoricBars = new DelegateCommand(RequestHistoricBarsFromProvider);
            }
            catch (Exception exception)
            {
                Logger.Error(exception, _oType.FullName, "SecurityStatisticsViewModel");
            }
        }

        /// <summary>
        /// Opens Window which Takes Information of Historic Bars.
        /// </summary>
        private void RequestHistoricBarsFromProvider()
        {
            HistoricBarSettingView.ShowDialog();
        }

        #region INotify Properties

        #region Check Box Binding

        /// <summary>
        /// Allow Quotes to be saved in Database
        /// </summary>
        private bool _quoteChecked;

        public bool QuoteChecked
        {
            get { return _quoteChecked; }
            set
            {
                _quoteChecked = value;
                RaisePropertyChanged("QuoteChecked");
                PublishSecurityPermissionsChangeMessage();
            }
        }

        /// <summary>
        /// Allow Trades to be saved in Database
        /// </summary>
        private bool _tradeChecked;

        public bool TradeChecked
        {
            get { return _tradeChecked; }
            set
            {
                _tradeChecked = value;
                RaisePropertyChanged("TradeChecked");
                PublishSecurityPermissionsChangeMessage();

            }
        }

        /// <summary>
        /// Allow Trades to be saved in Bars
        /// </summary>
        private bool _barChecked;

        public bool BarChecked
        {
            get { return _barChecked; }
            set
            {
                if (value)
                {
                    BarSettingView.ShowDialog();
                }
                _barChecked = value;
                RaisePropertyChanged("BarChecked");
                PublishSecurityPermissionsChangeMessage();

            }
        }

        #endregion


        /// <summary>
        /// Saves The number of Trades
        /// </summary>
        private int _numberOfTrades;
        public int NumberOfTrades
        {
            get { return _numberOfTrades; }
            set
            {
                _numberOfTrades = value;
                RaisePropertyChanged("NumberOfTrades");
            }
        }

        /// <summary>
        /// Saves The number of Trades
        /// </summary>
        private int _numberOfQuotes;
        public int NumberOfQuotes
        {
            get { return _numberOfQuotes; }
            set
            {
                _numberOfQuotes = value;
                RaisePropertyChanged("NumberOfQuotes");
            }
        }

        /// <summary>
        /// Saves The Number Of Bars
        /// </summary>
        private int _numberOfBars;
        public int NumberOfBars
        {
            get { return _numberOfBars; }
            set
            {
                _numberOfBars = value;
                RaisePropertyChanged("NumberOfBars");
            }
        }

        #endregion


        #region INotifyPropertyChanged Methords

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        /// <summary>
        /// Methord Fired When ever Permission Changes.
        /// </summary>
        private void PublishSecurityPermissionsChangeMessage()
        {
            SecurityPermissions permissions = new SecurityPermissions
                {
                    Id = Id,
                    MarketDataProvider = ProviderName,
                    Security = new Security {Symbol = Symbol},
                    WriteBars = true,
                    WriteQuote = QuoteChecked,
                    WriteTrade = TradeChecked
                };
            ChangeSecurityPermissionsMessage changeSecurityPermissionsMessage=new ChangeSecurityPermissionsMessage();
            changeSecurityPermissionsMessage.Permissions = permissions;
            EventSystem.Publish<ChangeSecurityPermissionsMessage>(changeSecurityPermissionsMessage);
        }

    }
}
