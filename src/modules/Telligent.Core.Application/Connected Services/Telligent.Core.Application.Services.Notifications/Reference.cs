﻿//------------------------------------------------------------------------------
// <auto-generated>
//     這段程式碼是由工具產生的。
//
//     變更此檔案可能會導致不正確的行為，而且若已重新產生
//     程式碼，則會遺失變更。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Telligent.Core.Application.Services.Notifications
{
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="Telligent.Core.Application.Services.Notifications.ISMS_API")]
    internal interface ISMS_API
    {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISMS_API/Send_SMS", ReplyAction="http://tempuri.org/ISMS_API/Send_SMSResponse")]
        System.Threading.Tasks.Task<string> Send_SMSAsync(string GroupID, string PhoneNumber, string Message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISMS_API/Send_SMSForCode", ReplyAction="http://tempuri.org/ISMS_API/Send_SMSForCodeResponse")]
        System.Threading.Tasks.Task<string> Send_SMSForCodeAsync(string GroupID, string PhoneNumber, string Message);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISMS_API/Send_SMSByOneAPI", ReplyAction="http://tempuri.org/ISMS_API/Send_SMSByOneAPIResponse")]
        System.Threading.Tasks.Task<string> Send_SMSByOneAPIAsync(System.Xml.XmlElement xData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISMS_API/Query_CategoryByOneAPI", ReplyAction="http://tempuri.org/ISMS_API/Query_CategoryByOneAPIResponse")]
        System.Threading.Tasks.Task<System.Xml.XmlElement> Query_CategoryByOneAPIAsync(System.Xml.XmlElement xData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISMS_API/Query_CategoryMsgByOneAPI", ReplyAction="http://tempuri.org/ISMS_API/Query_CategoryMsgByOneAPIResponse")]
        System.Threading.Tasks.Task<System.Xml.XmlElement> Query_CategoryMsgByOneAPIAsync(System.Xml.XmlElement xData);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISMS_API/Query_History_CostomerIDByOneAPI", ReplyAction="http://tempuri.org/ISMS_API/Query_History_CostomerIDByOneAPIResponse")]
        System.Threading.Tasks.Task<System.Xml.XmlElement> Query_History_CostomerIDByOneAPIAsync(System.Xml.XmlElement xData);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    internal interface ISMS_APIChannel : Telligent.Core.Application.Services.Notifications.ISMS_API, System.ServiceModel.IClientChannel
    {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Tools.ServiceModel.Svcutil", "2.0.3")]
    internal partial class SMS_APIClient : System.ServiceModel.ClientBase<Telligent.Core.Application.Services.Notifications.ISMS_API>, Telligent.Core.Application.Services.Notifications.ISMS_API
    {
        
        /// <summary>
        /// 實作此部分方法來設定服務端點。
        /// </summary>
        /// <param name="serviceEndpoint">要設定的端點</param>
        /// <param name="clientCredentials">用戶端認證</param>
        static partial void ConfigureEndpoint(System.ServiceModel.Description.ServiceEndpoint serviceEndpoint, System.ServiceModel.Description.ClientCredentials clientCredentials);
        
        public SMS_APIClient() : 
                base(SMS_APIClient.GetDefaultBinding(), SMS_APIClient.GetDefaultEndpointAddress())
        {
            this.Endpoint.Name = EndpointConfiguration.BasicHttpBinding_ISMS_API.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SMS_APIClient(EndpointConfiguration endpointConfiguration) : 
                base(SMS_APIClient.GetBindingForEndpoint(endpointConfiguration), SMS_APIClient.GetEndpointAddress(endpointConfiguration))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SMS_APIClient(EndpointConfiguration endpointConfiguration, string remoteAddress) : 
                base(SMS_APIClient.GetBindingForEndpoint(endpointConfiguration), new System.ServiceModel.EndpointAddress(remoteAddress))
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SMS_APIClient(EndpointConfiguration endpointConfiguration, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(SMS_APIClient.GetBindingForEndpoint(endpointConfiguration), remoteAddress)
        {
            this.Endpoint.Name = endpointConfiguration.ToString();
            ConfigureEndpoint(this.Endpoint, this.ClientCredentials);
        }
        
        public SMS_APIClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress)
        {
        }
        
        public System.Threading.Tasks.Task<string> Send_SMSAsync(string GroupID, string PhoneNumber, string Message)
        {
            return base.Channel.Send_SMSAsync(GroupID, PhoneNumber, Message);
        }
        
        public System.Threading.Tasks.Task<string> Send_SMSForCodeAsync(string GroupID, string PhoneNumber, string Message)
        {
            return base.Channel.Send_SMSForCodeAsync(GroupID, PhoneNumber, Message);
        }
        
        public System.Threading.Tasks.Task<string> Send_SMSByOneAPIAsync(System.Xml.XmlElement xData)
        {
            return base.Channel.Send_SMSByOneAPIAsync(xData);
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlElement> Query_CategoryByOneAPIAsync(System.Xml.XmlElement xData)
        {
            return base.Channel.Query_CategoryByOneAPIAsync(xData);
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlElement> Query_CategoryMsgByOneAPIAsync(System.Xml.XmlElement xData)
        {
            return base.Channel.Query_CategoryMsgByOneAPIAsync(xData);
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlElement> Query_History_CostomerIDByOneAPIAsync(System.Xml.XmlElement xData)
        {
            return base.Channel.Query_History_CostomerIDByOneAPIAsync(xData);
        }
        
        public virtual System.Threading.Tasks.Task OpenAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndOpen));
        }
        
        public virtual System.Threading.Tasks.Task CloseAsync()
        {
            return System.Threading.Tasks.Task.Factory.FromAsync(((System.ServiceModel.ICommunicationObject)(this)).BeginClose(null, null), new System.Action<System.IAsyncResult>(((System.ServiceModel.ICommunicationObject)(this)).EndClose));
        }
        
        private static System.ServiceModel.Channels.Binding GetBindingForEndpoint(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ISMS_API))
            {
                System.ServiceModel.BasicHttpBinding result = new System.ServiceModel.BasicHttpBinding();
                result.MaxBufferSize = int.MaxValue;
                result.ReaderQuotas = System.Xml.XmlDictionaryReaderQuotas.Max;
                result.MaxReceivedMessageSize = int.MaxValue;
                result.AllowCookies = true;
                return result;
            }
            throw new System.InvalidOperationException(string.Format("找不到名為 \'{0}\' 的端點。", endpointConfiguration));
        }
        
        private static System.ServiceModel.EndpointAddress GetEndpointAddress(EndpointConfiguration endpointConfiguration)
        {
            if ((endpointConfiguration == EndpointConfiguration.BasicHttpBinding_ISMS_API))
            {
                return new System.ServiceModel.EndpointAddress("http://192.168.1.160:8080/SMS_IRouter_PD/SMS_API.svc");
            }
            throw new System.InvalidOperationException(string.Format("找不到名為 \'{0}\' 的端點。", endpointConfiguration));
        }
        
        private static System.ServiceModel.Channels.Binding GetDefaultBinding()
        {
            return SMS_APIClient.GetBindingForEndpoint(EndpointConfiguration.BasicHttpBinding_ISMS_API);
        }
        
        private static System.ServiceModel.EndpointAddress GetDefaultEndpointAddress()
        {
            return SMS_APIClient.GetEndpointAddress(EndpointConfiguration.BasicHttpBinding_ISMS_API);
        }
        
        public enum EndpointConfiguration
        {
            
            BasicHttpBinding_ISMS_API,
        }
    }
}
