using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

namespace UserManagementApi.BLModule.AuthorizationModule
{
    public class AuthorizeAttribute : Attribute, IParameterInspector, IOperationBehavior
    {
        public void AddBindingParameters(OperationDescription operationDescription, BindingParameterCollection bindingParameters)
        {}

        public void AfterCall(string operationName, object[] outputs, object returnValue, object correlationState)
        {}

        public void ApplyClientBehavior(OperationDescription operationDescription, ClientOperation clientOperation)
        {}

        public void ApplyDispatchBehavior(OperationDescription operationDescription, DispatchOperation dispatchOperation)
        {
            dispatchOperation.ParameterInspectors.Add(this);
        }

        public object BeforeCall(string operationName, object[] inputs)
        {
            try
            {
                AuthorizationService AuthorizationService = new AuthorizationService();
                AuthorizationService.AuthorizeUser();
            }
            catch(Exception e)
            {
                GlobalException.ThrowError(e);
            }
            return null;
        }

        public void Validate(OperationDescription operationDescription)
        {}
    }
}