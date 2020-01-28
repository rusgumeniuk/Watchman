using System.Collections.Generic;

namespace Watchman.BusinessLogic.Models.Data
{
    public class OperationResult
    {
        public bool Succeeded { get; protected set; }
        public ISet<OperationError> Errors { get; set; } = new HashSet<OperationError>();

        public static OperationResult Success => new OperationResult() { Succeeded = true };

        public static OperationResult Failed(IEnumerable<OperationError> errors)
        {
            return new OperationResult()
            {
                Succeeded = false,
                Errors = errors == null ? new HashSet<OperationError>() : new HashSet<OperationError>(errors)
            };
        }
    }

    public class OperationError
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
