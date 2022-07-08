using System;

namespace OsosApp.Bo
{
    public class ResponseBo<TBo>
    {
        public ResponseBo()
        {
            IsSuccess = false;
            HasException = false;
            Message = null;
            ReturnedId = null;
            Ex = null;
            Bo = default(TBo);
        }

        public bool IsSuccess { get; internal set; }
        public bool HasException { get; internal set; }
        public string Message { get; internal set; }
        public long? ReturnedId { get; internal set; }
        public TBo Bo { get; set; }

        public object Log { get; internal set; }

        public Exception Ex { get; internal set; }

        public void Success(TBo bo, long? returnedId = null, object log = null)
        {
            Bo = bo;
            IsSuccess = true;
            Message = null;
            ReturnedId = returnedId;
            Ex = null;
            Log = log;
        }

        public void Set(TBo bo, bool isSuccess, string message, long? returnedId = null, object log = null)
        {
            Bo = bo;
            IsSuccess = isSuccess;
            Message = message;

            Ex = null;
            ReturnedId = returnedId;
            Log = log;
        }

        public void Failed(string message, object log = null)
        {
            IsSuccess = false;
            HasException = true;
            Message = message;
            Ex = null;

            Log = log;
            //return this;
        }

        public void FailedWithException(Exception ex, object log = null)
        {
            IsSuccess = false;
            HasException = true;
            Message = ex.Message;
            Ex = ex;

            Log = log;
        }

        public ResponseBo ToResponse()
        {
            return new ResponseBo()
            {
                IsSuccess = this.IsSuccess,
                Message = this.Message,
                ReturnedId = this.ReturnedId,
                Ex = this.Ex,
                HasException = this.HasException
            };
        }

        public ResponseBo<T> ToResponse<T>(T bo)
        {
            return new ResponseBo<T>()
            {
                IsSuccess = this.IsSuccess,
                Message = this.Message,
                ReturnedId = this.ReturnedId,
                Ex = this.Ex,
                HasException = this.HasException,
                Bo = bo
            };
        }
    }

    public class ResponseBo
    {
        public ResponseBo()
        {
            IsSuccess = false;
            HasException = false;
            Message = null;
            ReturnedId = null;
            Ex = null;
        }

        public bool IsSuccess { get; internal set; }
        public bool HasException { get; internal set; }
        public string Message { get; internal set; }

        public long? ReturnedId { get; internal set; }

        public Exception Ex { get; internal set; }

        public object Log { get; internal set; }

        public ResponseBo<T> ToResponse<T>()
        {
            return new ResponseBo<T>()
            {
                IsSuccess = this.IsSuccess,
                Message = this.Message,
                ReturnedId = this.ReturnedId,
                Ex = this.Ex,
                HasException = this.HasException,
                Bo = default(T)
            };
        }
        public ResponseBo<T> ToResponse<T>(T dto)
        {
            return new ResponseBo<T>()
            {
                IsSuccess = this.IsSuccess,
                Message = this.Message,
                ReturnedId = this.ReturnedId,
                Ex = this.Ex,
                HasException = this.HasException,
                Bo = dto
            };
        }

        public void Set(bool isSuccess, string message, long? returnedId, object log = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            ReturnedId = returnedId;

            Log = log;
        }

        public void Success(long? returnedId = null, object log = null)
        {
            IsSuccess = true;
            Message = null;
            ReturnedId = returnedId;
            Ex = null;

            Log = log;
        }

        public void Failed(string message, object log = null)
        {
            IsSuccess = false;
            HasException = true;
            Message = message;
            Ex = null;

            Log = log;
            //return this;
        }

        public void FailedWithException(Exception ex, object log = null)
        {
            IsSuccess = false;
            HasException = true;
            Message = ex.Message;
            Ex = ex;

            Log = log;
            //return this;
        }

        public void FailedWithException(Exception ex, string message, object log = null)
        {
            IsSuccess = false;
            HasException = true;
            Message = message;
            Ex = ex;

            Log = log;
            //return this;
        }
    }
}
