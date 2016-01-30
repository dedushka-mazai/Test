﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MassiveTest.Wcf.Client.MassiveTest.DomainSpecificService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MassiveTest.DomainSpecificService.IDomainSpecificService")]
    public interface IDomainSpecificService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDomainSpecificService/GetShortestPath", ReplyAction="http://tempuri.org/IDomainSpecificService/GetShortestPathResponse")]
        string[] GetShortestPath(string startId, string endId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IDomainSpecificService/GetShortestPath", ReplyAction="http://tempuri.org/IDomainSpecificService/GetShortestPathResponse")]
        System.Threading.Tasks.Task<string[]> GetShortestPathAsync(string startId, string endId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IDomainSpecificServiceChannel : MassiveTest.Wcf.Client.MassiveTest.DomainSpecificService.IDomainSpecificService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class DomainSpecificServiceClient : System.ServiceModel.ClientBase<MassiveTest.Wcf.Client.MassiveTest.DomainSpecificService.IDomainSpecificService>, MassiveTest.Wcf.Client.MassiveTest.DomainSpecificService.IDomainSpecificService {
        
        public DomainSpecificServiceClient() {
        }
        
        public DomainSpecificServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public DomainSpecificServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DomainSpecificServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public DomainSpecificServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string[] GetShortestPath(string startId, string endId) {
            return base.Channel.GetShortestPath(startId, endId);
        }
        
        public System.Threading.Tasks.Task<string[]> GetShortestPathAsync(string startId, string endId) {
            return base.Channel.GetShortestPathAsync(startId, endId);
        }
    }
}
