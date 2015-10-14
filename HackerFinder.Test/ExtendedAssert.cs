using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace HackerFinder.Test
{
    public static class ExtendedAssert
    {
        /// <summary>Check that a statement throws a specific type of exception</summary>
        /// <typeparam name="TException">Exception type inheriting from Exception</typeparam>
        /// <param name="executable">Action that should throw the exception</param>
        public static void Throws<TException>(Action executable) where TException : Exception
        {
            try
            {
                executable();
            }
            catch (Exception ex)
            {
                Assert.IsTrue(ex.GetType() == typeof(TException), String.Format("Expected exception of type {0} but got {1}", typeof(TException), ex.GetType()));
                return;
            }
            Assert.Fail(String.Format("Expected exception of type {0}, but no exception was thrown.", typeof(TException)));
        }

        /// <summary>Check that a statement throws some kind of exception</summary>
        /// <param name="executable">Action that should throw the exception</param>
        /// <param name="message">Optionally specify a message</param>
        public static void Throws(Action executable, string message = null)
        {
            try
            {
                executable();
            }
            catch
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.Fail(message ?? "Expected an exception but none was thrown.");
        }

        /// <summary>Check that a statement does not throw an exception</summary>
        /// <param name="executable">Action to execute</param>
        public static void DoesNotThrow(Action executable)
        {
            try
            {
                executable();
            }
            catch (Exception ex)
            {
                Assert.Fail(String.Format("Expected no exception, but exception of type {0} was thrown.", ex.GetType()));
            }
        }

    }
}
