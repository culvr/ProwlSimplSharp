using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ProwlSimplSharp;
using NUnit.Framework;

namespace ProwlSimplSharp.Test
{
    class WithResponseData
    {
        protected IProwlParser _parser;
        protected IProwlResponse _response;

        [SetUp]
        public void Context()
        {
            _parser = new ProwlXmlParser();
            _response = _parser.Parse(ResponseData());
        }

        virtual public string ResponseData()
        {
            throw new NotImplementedException();
        }
    }


    [TestFixture]
    class WhenParsingASuccessResponse : WithResponseData
    {
        override public string ResponseData()
        {
            return Properties.Resources.ProwlSuccessResponseData;
        }

        [Test]
        public void ItReturnsAnInstanceOfProwlSuccess()
        {
            Assert.IsInstanceOf<ProwlSuccess>(_response);
        }


        [Test]
        public void ResponseIsSuccessful()
        {
            Assert.That(_response.IsSuccessful);
        }

        [Test]
        public void ParsesRemainingCalls()
        {
            ProwlSuccess response = (ProwlSuccess)_parser.Parse(ResponseData());
            Assert.AreEqual(1000, response.Remaining);
        }

    }


    [TestFixture]
    class WithErrorResponse : WithResponseData
    {
        override public string ResponseData()
        {
            return Properties.Resources.ProwlErrorResponseData;
        }

        [Test]
        public void ResponseNotSuccessful()
        {
            Assert.IsFalse(_response.IsSuccessful);
        }

        [Test]
        public void ResponseAnInstanceOfProwlError()
        {;
            Assert.IsInstanceOf<ProwlError>(_response);
        }

        [Test]
        public void ParsesErrorMessage()
        {
            ProwlError response = (ProwlError)_parser.Parse(ResponseData());
            Assert.AreEqual("Bad Request", response.Message);
        }

        [Test]
        public void ContainsResponseCode()
        {
            Assert.AreEqual(_response.Code, 401);
        }
    }


    [TestFixture]
    class WithInvalidData : WithResponseData
    {
        override public string ResponseData()
        {
            return "";
        }

        [Test]
        public void ReturnsAnInstanceOfProwlError()
        {
            Assert.IsInstanceOf<ProwlError>(_response);
        }
    }
}
