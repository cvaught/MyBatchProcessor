using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBatchProcessor
{
    public static class Constants
    {
        public const int RESULT_SUCCESS = 0;
        public const int RESULT_FAILED = 1;
        public const int RESULT_MISSING_INPUT = 2;
        public const int RESULT_CANCELED = 3;
        public const int RESULT_ERROR_SHOWN = -1;

        public const int RESULT_NO_CONNECTION = 4;
        public const int RESULT_PARTIAL_FAILURE = 5;

        public const String DELIMETER = "; ";
    }
}
