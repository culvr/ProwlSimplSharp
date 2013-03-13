using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Crestron.SimplSharp.Net.Https; // RequestType
using ProwlSimplSharp;

namespace ProwlSimplSharp.Test
{
    [TestFixture]
    public class ProwlRequestTests
    {
        public static Dictionary<string, string> EmptyDictionary()
        {
            return new Dictionary<string, string>();
        }


        public static Dictionary<string, string> PopulatedDictionary()
        {
            return new Dictionary<string, string>() {
                {"foo","bar"},
                {"test","case"}};
        }


        [Test]
        public void WithAnEmptyDictionaryItDoesNotAttachParameters()
        {
            var expected = "https://api.prowlapp.com/publicapi/add";
            var request = new ProwlRequest("add", EmptyDictionary());
            var actual = request.Url.ToString();
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void WithPopulatedDictionary()
        {
            var pattern = "https://api.prowlapp.com/publicapi/add?(?:.+=.+&*)+";
            var request = new ProwlRequest("add", PopulatedDictionary());
            var actual = request.Url.ToString();
            Assert.That(actual, Is.StringMatching(pattern));
        }


        [Test]
        public void HttpGetForNonAddRequests()
        {
            var expected = RequestType.Get;
            var request = new ProwlRequest("other", EmptyDictionary());
            var actual = request.RequestType;
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void HttpPostWhenMethodIsAdd()
        {
            var expected = RequestType.Post;
            var request = new ProwlRequest("add", EmptyDictionary());
            var actual = request.RequestType;
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void RequestsAreHttpsOnly()
        {
            var request = new ProwlRequest("random", EmptyDictionary());
            var protocol = request.Url.Protocol;
            Assert.AreEqual("https", protocol);
        }
    }
}
