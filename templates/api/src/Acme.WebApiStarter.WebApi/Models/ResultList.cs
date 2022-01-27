using System.Collections.Generic;

namespace Cortside.Rebate.WebApi.Models {
    /// <summary>
    /// List of results
    /// </summary>
    /// <typeparam name="T">model</typeparam>
    public class ResultList<T> {
        /// <summary>
        /// Results
        /// </summary>
        public IList<T> Results { get; set; }
    }
}
