﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MassiveTest.Wcf.Client.MassiveTest.DataManagementService {
    using System.Runtime.Serialization;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DataOperationResult", Namespace="http://schemas.datacontract.org/2004/07/MassiveTest.Interface")]
    public enum DataOperationResult : int {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Ok = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Error = 1,
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MassiveTest.DataManagementService.IDataManagementService")]
    public interface IDataManagementService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataManagementService/AddNode", ReplyAction="http://tempuri.org/IDataManagementService/AddNodeResponse")]
        MassiveTest.Wcf.Client.MassiveTest.DataManagementService.DataOperationResult AddNode(string id, string label, string[] adjacentIds);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataManagementService/AddNode", ReplyAction="http://tempuri.org/IDataManagementService/AddNodeResponse")]
        System.Threading.Tasks.Task<MassiveTest.Wcf.Client.MassiveTest.DataManagementService.DataOperationResult> AddNodeAsync(string id, string label, string[] adjacentIds);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataManagementService/Clear", ReplyAction="http://tempuri.org/IDataManagementService/ClearResponse")]
        MassiveTest.Wcf.Client.MassiveTest.DataManagementService.DataOperationResult Clear();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDataManagementService/Clear", ReplyAction="http://tempuri.org/IDataManagementService/ClearResponse")]
        System.Threading.Tasks.Task<MassiveTest.Wcf.Client.MassiveTest.DataManagementService.DataOperationResult> ClearAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDataManagementServiceChannel : MassiveTest.Wcf.Client.MassiveTest.DataManagementService.IDataManagementService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DataManagementServiceClient : System.ServiceModel.ClientBase<MassiveTest.Wcf.Client.MassiveTest.DataManagementService.IDataManagementService>, MassiveTest.Wcf.Client.MassiveTest.DataManagementService.IDataManagementService {
        
        public DataManagementServiceClient() {
        }
        
        public DataManagementServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DataManagementServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DataManagementServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DataManagementServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public MassiveTest.Wcf.Client.MassiveTest.DataManagementService.DataOperationResult AddNode(string id, string label, string[] adjacentIds) {
            return base.Channel.AddNode(id, label, adjacentIds);
        }
        
        public System.Threading.Tasks.Task<MassiveTest.Wcf.Client.MassiveTest.DataManagementService.DataOperationResult> AddNodeAsync(string id, string label, string[] adjacentIds) {
            return base.Channel.AddNodeAsync(id, label, adjacentIds);
        }
        
        public MassiveTest.Wcf.Client.MassiveTest.DataManagementService.DataOperationResult Clear() {
            return base.Channel.Clear();
        }
        
        public System.Threading.Tasks.Task<MassiveTest.Wcf.Client.MassiveTest.DataManagementService.DataOperationResult> ClearAsync() {
            return base.Channel.ClearAsync();
        }
    }
}
