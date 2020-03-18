﻿using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Quantic.Core.Test.Core
{
    public class QueryResultTest
    {
       [Fact]
        public void ShouldThrowArgumentNullExpcetionForCode()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => 
            {
                // Act
                var result = new QueryResult<int>(1, code: null, "message");
            });

            Assert.Throws<ArgumentNullException>(() => 
            {
                // Act
                var result = new QueryResult<int>(1, code: "", "message");
            });  
        }

        [Fact]
        public void ShouldThrowArgumentNullExpcetionForFailures()
        {
            // Assert
            Assert.Throws<ArgumentNullException>(() => 
            {
                // Act
                var failureList = new List<Failure>(null);
                var result = new QueryResult<int>(failureList);
            }); 

            // Assert
            Assert.Throws<ArgumentNullException>(() => 
            {
                // Act
                var failureList = new List<Failure>();
                var result = new QueryResult<int>(failureList);
            });             
        }

        [Fact]
        public void ShouldSuccessWithData()
        {
            // Arrange
            int result = 1;

            // Act
            var queryResult = new QueryResult<int>(result);

            // Assert
            Assert.True(queryResult.IsSuccess);
            Assert.Equal(Messages.Success, queryResult.Code);
            Assert.False(queryResult.HasError);
            Assert.False(queryResult.Retry);
        }

        [Fact]
        public void ShouldSuccessWithNullResult()
        {
            // Arrange
            int? result = null;

            // Act
            var queryResult = new QueryResult<int?>(result);

            // Assert
            Assert.True(queryResult.IsSuccess);
            Assert.Equal(Messages.Success, queryResult.Code);
            Assert.Equal(result, queryResult.Result);
            Assert.False(queryResult.HasError);
            Assert.False(queryResult.Retry);
        }

        [Fact]
        public void ShouldSuccessWithCodeAndResult()
        {
            // Arrange
            string code = "code";
            int result = 1;

            // Act
            var queryResult = new QueryResult<int>(result, code);

            // Assert
            Assert.True(queryResult.IsSuccess);
            Assert.Equal(result, queryResult.Result);
            Assert.Equal(code, queryResult.Code);
            Assert.False(queryResult.HasError);
            Assert.False(queryResult.Retry);
        }

        [Fact]
        public void ShouldSuccessWithResultCodeAndNullMessage()
        {
            // Arrange
            string code = "code";
            string message = null;
            int result = 1;

            // Act
            var queryResult = new QueryResult<int>(result, code,message);

            // Assert
            Assert.True(queryResult.IsSuccess);
            Assert.Equal(result, queryResult.Result);
            Assert.Equal(code, queryResult.Code);
            Assert.Equal(message, queryResult.Message);

            Assert.False(queryResult.HasError);
            Assert.False(queryResult.Retry);
        }

        [Fact]
        public void ShouldSuccessWithResultCodeAndEmptyMessage()
        {
            // Arrange
            string code = "code";
            string message = "";
            int result = 1;

            // Act
            var queryResult = new QueryResult<int>(result, code, message);

            // Assert
            Assert.True(queryResult.IsSuccess);
            Assert.Equal(result, queryResult.Result);
            Assert.Equal(code, queryResult.Code);
            Assert.Equal(message, queryResult.Message);
            Assert.False(queryResult.HasError);
            Assert.False(queryResult.Retry);
        }

        [Fact]
        public void ShouldSuccessWithFailureWithoutRetry()
        {
            // Arrange
            var failireCode = "failure_code";
            var failure = new Failure(failireCode);

            // Act
            var result = new QueryResult<int>(failure);

            // Assert
            Assert.True(result.HasError);
            Assert.False(result.Retry);
            Assert.True(result.Errors.Count == 1
                && result.Errors.Any(x => x.Code == failireCode));

            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void ShouldSuccessWithFailureListWithoutRetry()
        {
            // Arrange
            var failireCode1 = "failure_code_1";
            var failure1 = new Failure(failireCode1);
            var failireCode2 = "failure_code_2";
            var failure2 = new Failure(failireCode2);
            var failures = new List<Failure> { failure1, failure2 };

            // Act
            var result = new QueryResult<int>(failures);

            // Assert
            Assert.True(result.HasError);
            Assert.False(result.Retry);
            Assert.True(result.Errors.Count == 2
                && result.Errors.Any(x => x.Code == failireCode1)
                && result.Errors.Any(x => x.Code == failireCode2));
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void ShouldSuccessWithFailureAndFalseRetry()
        {
            // Arrange
            var failireCode = "failure_code";
            var failure = new Failure(failireCode);
            bool retry = false;

            // Act
            var result = new QueryResult<int>(failure, retry: retry);

            // Assert
            Assert.True(result.HasError);
            Assert.False(result.Retry);
            Assert.False(result.IsSuccess);
        }

        [Fact]
        public void ShouldSuccessWithFailureAndTrueRetry()
        {
            // Arrange
            var failireCode = "failure_code";
            var failure = new Failure(failireCode);
            bool retry = true;

            // Act
            var result = new QueryResult<int>(failure, retry: retry);

            // Assert
            Assert.True(result.HasError);
            Assert.True(result.Retry);
            Assert.False(result.IsSuccess);
        }
    }
}
