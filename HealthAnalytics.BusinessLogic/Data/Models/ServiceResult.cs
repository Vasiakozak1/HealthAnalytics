namespace HealthAnalytics.BusinessLogic.Data.Models
{
    public class ServiceResult<T>
    {
        public bool IsSuccess { get; }
        public T Data { get; }

        public ServiceResult(bool IsSuccess, T Data)
        {
            this.IsSuccess = IsSuccess;
            this.Data = Data;
        }
    }
}
