﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace NewBisPA.SecuritySvcRef {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Application", Namespace="http://schemas.datacontract.org/2004/07/AppSecurityProvider")]
    [System.SerializableAttribute()]
    public partial class Application : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StartCommandField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UpdateByField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime UpdateDateField;
        
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
        public string Code {
            get {
                return this.CodeField;
            }
            set {
                if ((object.ReferenceEquals(this.CodeField, value) != true)) {
                    this.CodeField = value;
                    this.RaisePropertyChanged("Code");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StartCommand {
            get {
                return this.StartCommandField;
            }
            set {
                if ((object.ReferenceEquals(this.StartCommandField, value) != true)) {
                    this.StartCommandField = value;
                    this.RaisePropertyChanged("StartCommand");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UpdateBy {
            get {
                return this.UpdateByField;
            }
            set {
                if ((object.ReferenceEquals(this.UpdateByField, value) != true)) {
                    this.UpdateByField = value;
                    this.RaisePropertyChanged("UpdateBy");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime UpdateDate {
            get {
                return this.UpdateDateField;
            }
            set {
                if ((this.UpdateDateField.Equals(value) != true)) {
                    this.UpdateDateField = value;
                    this.RaisePropertyChanged("UpdateDate");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ProcessResult", Namespace="http://schemas.datacontract.org/2004/07/CenterProvider")]
    [System.SerializableAttribute()]
    public partial class ProcessResult : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorMessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool SuccessedField;
        
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
        public string ErrorMessage {
            get {
                return this.ErrorMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorMessageField, value) != true)) {
                    this.ErrorMessageField = value;
                    this.RaisePropertyChanged("ErrorMessage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Successed {
            get {
                return this.SuccessedField;
            }
            set {
                if ((this.SuccessedField.Equals(value) != true)) {
                    this.SuccessedField = value;
                    this.RaisePropertyChanged("Successed");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Program", Namespace="http://schemas.datacontract.org/2004/07/AppSecurityProvider")]
    [System.SerializableAttribute()]
    public partial class Program : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long ApplicationIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool DisplayField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long ProgramGroupIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ProgramTypeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string StartCommandField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UpdateByField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime UpdateDateField;
        
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
        public long ApplicationID {
            get {
                return this.ApplicationIDField;
            }
            set {
                if ((this.ApplicationIDField.Equals(value) != true)) {
                    this.ApplicationIDField = value;
                    this.RaisePropertyChanged("ApplicationID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Code {
            get {
                return this.CodeField;
            }
            set {
                if ((object.ReferenceEquals(this.CodeField, value) != true)) {
                    this.CodeField = value;
                    this.RaisePropertyChanged("Code");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool Display {
            get {
                return this.DisplayField;
            }
            set {
                if ((this.DisplayField.Equals(value) != true)) {
                    this.DisplayField = value;
                    this.RaisePropertyChanged("Display");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long ProgramGroupID {
            get {
                return this.ProgramGroupIDField;
            }
            set {
                if ((this.ProgramGroupIDField.Equals(value) != true)) {
                    this.ProgramGroupIDField = value;
                    this.RaisePropertyChanged("ProgramGroupID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ProgramType {
            get {
                return this.ProgramTypeField;
            }
            set {
                if ((object.ReferenceEquals(this.ProgramTypeField, value) != true)) {
                    this.ProgramTypeField = value;
                    this.RaisePropertyChanged("ProgramType");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string StartCommand {
            get {
                return this.StartCommandField;
            }
            set {
                if ((object.ReferenceEquals(this.StartCommandField, value) != true)) {
                    this.StartCommandField = value;
                    this.RaisePropertyChanged("StartCommand");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UpdateBy {
            get {
                return this.UpdateByField;
            }
            set {
                if ((object.ReferenceEquals(this.UpdateByField, value) != true)) {
                    this.UpdateByField = value;
                    this.RaisePropertyChanged("UpdateBy");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime UpdateDate {
            get {
                return this.UpdateDateField;
            }
            set {
                if ((this.UpdateDateField.Equals(value) != true)) {
                    this.UpdateDateField = value;
                    this.RaisePropertyChanged("UpdateDate");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ProgramGroup", Namespace="http://schemas.datacontract.org/2004/07/AppSecurityProvider")]
    [System.SerializableAttribute()]
    public partial class ProgramGroup : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long ApplicationIDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private NewBisPA.SecuritySvcRef.Program[] ProgramsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private NewBisPA.SecuritySvcRef.ProgramGroup[] SubGroupField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UpdateByField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime UpdateDateField;
        
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
        public long ApplicationID {
            get {
                return this.ApplicationIDField;
            }
            set {
                if ((this.ApplicationIDField.Equals(value) != true)) {
                    this.ApplicationIDField = value;
                    this.RaisePropertyChanged("ApplicationID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Code {
            get {
                return this.CodeField;
            }
            set {
                if ((object.ReferenceEquals(this.CodeField, value) != true)) {
                    this.CodeField = value;
                    this.RaisePropertyChanged("Code");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Description {
            get {
                return this.DescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DescriptionField, value) != true)) {
                    this.DescriptionField = value;
                    this.RaisePropertyChanged("Description");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public NewBisPA.SecuritySvcRef.Program[] Programs {
            get {
                return this.ProgramsField;
            }
            set {
                if ((object.ReferenceEquals(this.ProgramsField, value) != true)) {
                    this.ProgramsField = value;
                    this.RaisePropertyChanged("Programs");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public NewBisPA.SecuritySvcRef.ProgramGroup[] SubGroup {
            get {
                return this.SubGroupField;
            }
            set {
                if ((object.ReferenceEquals(this.SubGroupField, value) != true)) {
                    this.SubGroupField = value;
                    this.RaisePropertyChanged("SubGroup");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string UpdateBy {
            get {
                return this.UpdateByField;
            }
            set {
                if ((object.ReferenceEquals(this.UpdateByField, value) != true)) {
                    this.UpdateByField = value;
                    this.RaisePropertyChanged("UpdateBy");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.DateTime UpdateDate {
            get {
                return this.UpdateDateField;
            }
            set {
                if ((this.UpdateDateField.Equals(value) != true)) {
                    this.UpdateDateField = value;
                    this.RaisePropertyChanged("UpdateDate");
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SecuritySvcRef.ISecurityService")]
    public interface ISecurityService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISecurityService/GetWebApplicationByPermit", ReplyAction="http://tempuri.org/ISecurityService/GetWebApplicationByPermitResponse")]
        NewBisPA.SecuritySvcRef.Application[] GetWebApplicationByPermit(out NewBisPA.SecuritySvcRef.ProcessResult processResult, string UserID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISecurityService/GetWinApplicationByPermit", ReplyAction="http://tempuri.org/ISecurityService/GetWinApplicationByPermitResponse")]
        NewBisPA.SecuritySvcRef.Application[] GetWinApplicationByPermit(out NewBisPA.SecuritySvcRef.ProcessResult processResult, string UserID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISecurityService/GetProgramByPermit", ReplyAction="http://tempuri.org/ISecurityService/GetProgramByPermitResponse")]
        NewBisPA.SecuritySvcRef.Program[] GetProgramByPermit(out NewBisPA.SecuritySvcRef.ProcessResult processResult, string UserID, long ApplicationID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISecurityService/GetWebProgramByPermit", ReplyAction="http://tempuri.org/ISecurityService/GetWebProgramByPermitResponse")]
        NewBisPA.SecuritySvcRef.Program[] GetWebProgramByPermit(out NewBisPA.SecuritySvcRef.ProcessResult processResult, string UserID, long ApplicationID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISecurityService/GetWinProgramByPermit", ReplyAction="http://tempuri.org/ISecurityService/GetWinProgramByPermitResponse")]
        NewBisPA.SecuritySvcRef.Program[] GetWinProgramByPermit(out NewBisPA.SecuritySvcRef.ProcessResult processResult, string UserID, long ApplicationID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISecurityService/VerifyPermit", ReplyAction="http://tempuri.org/ISecurityService/VerifyPermitResponse")]
        bool VerifyPermit(out NewBisPA.SecuritySvcRef.ProcessResult processResult, string userID, long applicationID, string programCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISecurityService/VerifyPermitByCode", ReplyAction="http://tempuri.org/ISecurityService/VerifyPermitByCodeResponse")]
        bool VerifyPermitByCode(out NewBisPA.SecuritySvcRef.ProcessResult processResult, string userID, string applicationCode, string programCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISecurityService/VerifyPassword", ReplyAction="http://tempuri.org/ISecurityService/VerifyPasswordResponse")]
        bool VerifyPassword(out NewBisPA.SecuritySvcRef.ProcessResult processResult, string userID, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISecurityService/ChangePassword", ReplyAction="http://tempuri.org/ISecurityService/ChangePasswordResponse")]
        bool ChangePassword(out NewBisPA.SecuritySvcRef.ProcessResult processResult, string userID, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISecurityService/GetNUserId", ReplyAction="http://tempuri.org/ISecurityService/GetNUserIdResponse")]
        string GetNUserId(out NewBisPA.SecuritySvcRef.ProcessResult processResult, string inputUserLogin);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISecurityService/GetProgramGroupByPermit", ReplyAction="http://tempuri.org/ISecurityService/GetProgramGroupByPermitResponse")]
        NewBisPA.SecuritySvcRef.ProgramGroup[] GetProgramGroupByPermit(out NewBisPA.SecuritySvcRef.ProcessResult processResult, string UserID, long ApplicationID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISecurityService/GetProgramGroup", ReplyAction="http://tempuri.org/ISecurityService/GetProgramGroupResponse")]
        NewBisPA.SecuritySvcRef.ProgramGroup[] GetProgramGroup(out NewBisPA.SecuritySvcRef.ProcessResult processResult, string userId, System.Nullable<long> applicationId, string programType, System.Nullable<bool> display);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISecurityService/GetProgramGroupById", ReplyAction="http://tempuri.org/ISecurityService/GetProgramGroupByIdResponse")]
        NewBisPA.SecuritySvcRef.ProcessResult GetProgramGroupById(out NewBisPA.SecuritySvcRef.ProgramGroup programGroup, long programGroupId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISecurityService/VerifyDomainUser", ReplyAction="http://tempuri.org/ISecurityService/VerifyDomainUserResponse")]
        NewBisPA.SecuritySvcRef.ProcessResult VerifyDomainUser(out bool verified, out string oldUserId, out string newUserId, string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISecurityService/VerifyUserId", ReplyAction="http://tempuri.org/ISecurityService/VerifyUserIdResponse")]
        NewBisPA.SecuritySvcRef.ProcessResult VerifyUserId(out bool verified, string userId, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISecurityService/GetApplication", ReplyAction="http://tempuri.org/ISecurityService/GetApplicationResponse")]
        NewBisPA.SecuritySvcRef.Application[] GetApplication(out NewBisPA.SecuritySvcRef.ProcessResult processResult, long[] applicationId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISecurityService/VerifyLdapUser", ReplyAction="http://tempuri.org/ISecurityService/VerifyLdapUserResponse")]
        NewBisPA.SecuritySvcRef.ProcessResult VerifyLdapUser(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISecurityService/VerifyLdapCenterUser", ReplyAction="http://tempuri.org/ISecurityService/VerifyLdapCenterUserResponse")]
        NewBisPA.SecuritySvcRef.ProcessResult VerifyLdapCenterUser(string userName, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISecurityService/TerminateUser", ReplyAction="http://tempuri.org/ISecurityService/TerminateUserResponse")]
        ITUtility.ProcessResult TerminateUser(string ADUserName, System.DateTime TerminateDate, string UserId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISecurityService/DeleteAppUser", ReplyAction="http://tempuri.org/ISecurityService/DeleteAppUserResponse")]
        ITUtility.ProcessResult DeleteAppUser();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISecurityService/RemoveApplicationUser", ReplyAction="http://tempuri.org/ISecurityService/RemoveApplicationUserResponse")]
        ITUtility.ProcessResult RemoveApplicationUser(string nUserId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISecurityService/MarkProcessedTerminatedUser", ReplyAction="http://tempuri.org/ISecurityService/MarkProcessedTerminatedUserResponse")]
        ITUtility.ProcessResult MarkProcessedTerminatedUser(string nUserId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISecurityServiceChannel : NewBisPA.SecuritySvcRef.ISecurityService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SecurityServiceClient : System.ServiceModel.ClientBase<NewBisPA.SecuritySvcRef.ISecurityService>, NewBisPA.SecuritySvcRef.ISecurityService {
        
        public SecurityServiceClient() {
        }
        
        public SecurityServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SecurityServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SecurityServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SecurityServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public NewBisPA.SecuritySvcRef.Application[] GetWebApplicationByPermit(out NewBisPA.SecuritySvcRef.ProcessResult processResult, string UserID) {
            return base.Channel.GetWebApplicationByPermit(out processResult, UserID);
        }
        
        public NewBisPA.SecuritySvcRef.Application[] GetWinApplicationByPermit(out NewBisPA.SecuritySvcRef.ProcessResult processResult, string UserID) {
            return base.Channel.GetWinApplicationByPermit(out processResult, UserID);
        }
        
        public NewBisPA.SecuritySvcRef.Program[] GetProgramByPermit(out NewBisPA.SecuritySvcRef.ProcessResult processResult, string UserID, long ApplicationID) {
            return base.Channel.GetProgramByPermit(out processResult, UserID, ApplicationID);
        }
        
        public NewBisPA.SecuritySvcRef.Program[] GetWebProgramByPermit(out NewBisPA.SecuritySvcRef.ProcessResult processResult, string UserID, long ApplicationID) {
            return base.Channel.GetWebProgramByPermit(out processResult, UserID, ApplicationID);
        }
        
        public NewBisPA.SecuritySvcRef.Program[] GetWinProgramByPermit(out NewBisPA.SecuritySvcRef.ProcessResult processResult, string UserID, long ApplicationID) {
            return base.Channel.GetWinProgramByPermit(out processResult, UserID, ApplicationID);
        }
        
        public bool VerifyPermit(out NewBisPA.SecuritySvcRef.ProcessResult processResult, string userID, long applicationID, string programCode) {
            return base.Channel.VerifyPermit(out processResult, userID, applicationID, programCode);
        }
        
        public bool VerifyPermitByCode(out NewBisPA.SecuritySvcRef.ProcessResult processResult, string userID, string applicationCode, string programCode) {
            return base.Channel.VerifyPermitByCode(out processResult, userID, applicationCode, programCode);
        }
        
        public bool VerifyPassword(out NewBisPA.SecuritySvcRef.ProcessResult processResult, string userID, string password) {
            return base.Channel.VerifyPassword(out processResult, userID, password);
        }
        
        public bool ChangePassword(out NewBisPA.SecuritySvcRef.ProcessResult processResult, string userID, string password) {
            return base.Channel.ChangePassword(out processResult, userID, password);
        }
        
        public string GetNUserId(out NewBisPA.SecuritySvcRef.ProcessResult processResult, string inputUserLogin) {
            return base.Channel.GetNUserId(out processResult, inputUserLogin);
        }
        
        public NewBisPA.SecuritySvcRef.ProgramGroup[] GetProgramGroupByPermit(out NewBisPA.SecuritySvcRef.ProcessResult processResult, string UserID, long ApplicationID) {
            return base.Channel.GetProgramGroupByPermit(out processResult, UserID, ApplicationID);
        }
        
        public NewBisPA.SecuritySvcRef.ProgramGroup[] GetProgramGroup(out NewBisPA.SecuritySvcRef.ProcessResult processResult, string userId, System.Nullable<long> applicationId, string programType, System.Nullable<bool> display) {
            return base.Channel.GetProgramGroup(out processResult, userId, applicationId, programType, display);
        }
        
        public NewBisPA.SecuritySvcRef.ProcessResult GetProgramGroupById(out NewBisPA.SecuritySvcRef.ProgramGroup programGroup, long programGroupId) {
            return base.Channel.GetProgramGroupById(out programGroup, programGroupId);
        }
        
        public NewBisPA.SecuritySvcRef.ProcessResult VerifyDomainUser(out bool verified, out string oldUserId, out string newUserId, string userName, string password) {
            return base.Channel.VerifyDomainUser(out verified, out oldUserId, out newUserId, userName, password);
        }
        
        public NewBisPA.SecuritySvcRef.ProcessResult VerifyUserId(out bool verified, string userId, string password) {
            return base.Channel.VerifyUserId(out verified, userId, password);
        }
        
        public NewBisPA.SecuritySvcRef.Application[] GetApplication(out NewBisPA.SecuritySvcRef.ProcessResult processResult, long[] applicationId) {
            return base.Channel.GetApplication(out processResult, applicationId);
        }
        
        public NewBisPA.SecuritySvcRef.ProcessResult VerifyLdapUser(string userName, string password) {
            return base.Channel.VerifyLdapUser(userName, password);
        }
        
        public NewBisPA.SecuritySvcRef.ProcessResult VerifyLdapCenterUser(string userName, string password) {
            return base.Channel.VerifyLdapCenterUser(userName, password);
        }
        
        public ITUtility.ProcessResult TerminateUser(string ADUserName, System.DateTime TerminateDate, string UserId) {
            return base.Channel.TerminateUser(ADUserName, TerminateDate, UserId);
        }
        
        public ITUtility.ProcessResult DeleteAppUser() {
            return base.Channel.DeleteAppUser();
        }
        
        public ITUtility.ProcessResult RemoveApplicationUser(string nUserId) {
            return base.Channel.RemoveApplicationUser(nUserId);
        }
        
        public ITUtility.ProcessResult MarkProcessedTerminatedUser(string nUserId) {
            return base.Channel.MarkProcessedTerminatedUser(nUserId);
        }
    }
}
