using System;

namespace Codell.Pies.Testing.BDD
{
    public class TimeThisAttribute : Attribute
    {
        public TimeThisAttribute() : this("Time to exercise system under test")
        {
        }

        public TimeThisAttribute(string message) : this(message, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimeThisAttribute"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="failingTime">The max execution time as a string (hh:mm:ss).</param>
        public TimeThisAttribute(string message, string failingTime)
        {
            Message = message;
            FailingTime = failingTime;
        }

        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the max execution time as a string (hh:mm:ss).
        /// If the test runs beyond this time, it will fail.
        /// </summary>
        /// <value>The failing time.</value>
        public string FailingTime { get; set; }
    }
}