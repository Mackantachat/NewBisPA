﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewBisPA.ReceiptSvcRef {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="MyResponse", Namespace="urn:ReceiptWcfSvc")]
    [System.SerializableAttribute()]
    public partial class MyResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool BoolValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StringValueField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool BoolValue {
            get {
                return this.BoolValueField;
            }
            set {
                if ((this.BoolValueField.Equals(value) != true)) {
                    this.BoolValueField = value;
                    this.RaisePropertyChanged("BoolValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StringValue {
            get {
                return this.StringValueField;
            }
            set {
                if ((object.ReferenceEquals(this.StringValueField, value) != true)) {
                    this.StringValueField = value;
                    this.RaisePropertyChanged("StringValue");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ReceiptSvcRef.IService")]
    public interface IService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetData", ReplyAction="http://tempuri.org/IService/GetDataResponse")]
        string GetData(int value);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IService/GetDataUsingDataContract", ReplyAction="http://tempuri.org/IService/GetDataUsingDataContractResponse")]
        NewBisPA.ReceiptSvcRef.MyResponse GetDataUsingDataContract(NewBisPA.ReceiptSvcRef.MyResponse composite);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/GenerateReceipt")]
        void GenerateReceipt(string appRegID, string channelType, string PolicyNumber, string PolicyHolder, System.Nullable<System.DateTime> billingDate, System.Nullable<long> PK_UAPP_ID, string UserID);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/GenerateReceiptHalfProcess")]
        void GenerateReceiptHalfProcess(string appRegID, string channelType, string PolicyNumber, string PolicyHolder, System.Nullable<System.DateTime> billingDate, System.Nullable<long> PK_UAPP_ID, string UserID);
        
        [System.ServiceModel.OperationContractAttribute(IsOneWay=true, Action="http://tempuri.org/IService/SendStatusToIsis")]
        void SendStatusToIsis(string appNo, string appRegisterID, string statusCode, string subStatusCode, string detail, System.DateTime responseDate, int gracePeriod, long isisDocID);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceChannel : NewBisPA.ReceiptSvcRef.IService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceClient : System.ServiceModel.ClientBase<NewBisPA.ReceiptSvcRef.IService>, NewBisPA.ReceiptSvcRef.IService {
        
        public ServiceClient() {
        }
        
        public ServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string GetData(int value) {
            return base.Channel.GetData(value);
        }
        
        public NewBisPA.ReceiptSvcRef.MyResponse GetDataUsingDataContract(NewBisPA.ReceiptSvcRef.MyResponse composite) {
            return base.Channel.GetDataUsingDataContract(composite);
        }
        
        public void GenerateReceipt(string appRegID, string channelType, string PolicyNumber, string PolicyHolder, System.Nullable<System.DateTime> billingDate, System.Nullable<long> PK_UAPP_ID, string UserID) {
            base.Channel.GenerateReceipt(appRegID, channelType, PolicyNumber, PolicyHolder, billingDate, PK_UAPP_ID, UserID);
        }
        
        public void GenerateReceiptHalfProcess(string appRegID, string channelType, string PolicyNumber, string PolicyHolder, System.Nullable<System.DateTime> billingDate, System.Nullable<long> PK_UAPP_ID, string UserID) {
            base.Channel.GenerateReceiptHalfProcess(appRegID, channelType, PolicyNumber, PolicyHolder, billingDate, PK_UAPP_ID, UserID);
        }
        
        public void SendStatusToIsis(string appNo, string appRegisterID, string statusCode, string subStatusCode, string detail, System.DateTime responseDate, int gracePeriod, long isisDocID) {
            base.Channel.SendStatusToIsis(appNo, appRegisterID, statusCode, subStatusCode, detail, responseDate, gracePeriod, isisDocID);
        }
    }
}