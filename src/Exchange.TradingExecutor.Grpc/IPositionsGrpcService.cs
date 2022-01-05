using System.ServiceModel;
using System.Threading.Tasks;
using StatelessTradingExecutor.Grpc.Models;

namespace StatelessTradingExecutor.Grpc
{
    [ServiceContract]
    public interface IPositionsGrpcService
    {
        [OperationContract]
        Task OpenPositionAsync(OpenPositionGrpcCommand request);
        
        [OperationContract]
        Task ClosePositionAsync(ClosePositionGrpcCommand request);
    }
}