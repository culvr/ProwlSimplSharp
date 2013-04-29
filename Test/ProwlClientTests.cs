﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ProwlSimplSharp;

namespace ProwlSimplSharp.Test
{
    [TestFixture]
    class WhenSendingRequests
    {
        [Test]
        public void ReturnsNegativeOneWhenThereAreNoKeys()
        {
            ProwlClient client = new ProwlClient();

            int expected = -1;
            int actual = client.Send("app", 0, "app://", "app-subject", "app-message");
            Assert.AreEqual(expected, actual);
        }
    }


    [TestFixture]
    class WhenAddingApiKeysToProwlClient
    {
        const string ValidKey = "98b1735456b516cd305dfd3460da5d8ed34db1d4";

        ProwlClient _client;

        private string LengthMsg(int length)
        {
            return String.Format("Length: {0}", length);
        }


        [SetUp]
        public void InitializeClient()
        {
             _client = new ProwlClient();
        }


        [Test]
        public void KeyLengthMustBe40Characters()
        {
            string too_short = "98b1735456b516cd305dfd3460da5d8ed34db1d";      // 39
            string just_right = "98b1735456b516cd305dfd3460da5d8ed34db1d4";    // 40 <-
            string too_long =   "98b1735456b516cd305dfd3460da5d8ed34db1d4AA";  // 41
            
            
            Assert.AreEqual(_client.AddApiKey(too_short), 0, LengthMsg(too_short.Length));
            Assert.AreEqual(_client.AddApiKey(just_right), 1, LengthMsg(just_right.Length));
            Assert.AreEqual(_client.AddApiKey(too_long), 0,  LengthMsg(too_long.Length));
        }
        


        [Test]
        public void NullKeysReturnsZero()
        {
            Assert.AreEqual(_client.AddApiKey(null), 0);
        }
    }
}