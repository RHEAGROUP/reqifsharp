﻿// -------------------------------------------------------------------------------------------------
// <copyright file="AttributeValueBooleanTestFixture.cs" company="RHEA System S.A.">
//
//   Copyright 2017 RHEA System S.A.
//
//   Licensed under the Apache License, Version 2.0 (the "License");
//   you may not use this file except in compliance with the License.
//   You may obtain a copy of the License at
//
//       http://www.apache.org/licenses/LICENSE-2.0
//
//   Unless required by applicable law or agreed to in writing, software
//   distributed under the License is distributed on an "AS IS" BASIS,
//   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//   See the License for the specific language governing permissions and
//   limitations under the License.
//
// </copyright>
// ------------------------------------------------------------------------------------------------

namespace ReqIFSharp.Tests
{
    using System;
    using System.IO;
    using System.Runtime.Serialization;
    using System.Xml;

    using NUnit.Framework;

    using ReqIFSharp;

    /// <summary>
    /// Suite of tests for the <see cref="AttributeValueBoolean"/>
    /// </summary>
    [TestFixture]
    public class AttributeValueBooleanTestFixture
    {
        [Test]
        public void VerifyThatTheAttributeDefinitionCanBeSetOrGet()
        {
            var atrAttributeDefinitionBoolean = new AttributeDefinitionBoolean();

            var attributeValueBoolean = new AttributeValueBoolean();
            attributeValueBoolean.Definition = atrAttributeDefinitionBoolean;

            var attributeValue = (AttributeValue)attributeValueBoolean;

            Assert.AreEqual(atrAttributeDefinitionBoolean, attributeValue.AttributeDefinition);

            attributeValue.AttributeDefinition = atrAttributeDefinitionBoolean;

            Assert.AreEqual(atrAttributeDefinitionBoolean, attributeValue.AttributeDefinition);
        }

        [Test]
        public void VerifytThatExceptionIsRaisedWhenInvalidAttributeDefinitionIsSet()
        {
            var attributeDefinitionString = new AttributeDefinitionString();
            var attributeValueBoolean = new AttributeValueBoolean();
            var attributeValue = (AttributeValue)attributeValueBoolean;

            Assert.Throws<ArgumentException>(() => attributeValue.AttributeDefinition = attributeDefinitionString);
        }

        [Test]
        public void Verify_that_GetSchema_returns_null()
        {
            var attributeValue = new AttributeValueBoolean();
            Assert.That(attributeValue.GetSchema(), Is.Null);
        }

        [Test]
        public void VerifyThatWriteXmlWithoutDefinitionSetThrowsSerializationException()
        {
            using (var fs = new FileStream("test.xml", FileMode.Create))
            {
                using (var writer = XmlWriter.Create(fs, new XmlWriterSettings { Indent = true }))
                {
                    var attributeValueReal = new AttributeValueBoolean();

                    Assert.That(
                        () => attributeValueReal.WriteXml(writer),
                        Throws.Exception.TypeOf<SerializationException>()
                            .With.Message.Contains("The Definition property of an AttributeValueBoolean may not be null"));
                }
            }
        }
        
        [Test]
        public void VerifyConvenienceValueProperty()
        {
            var attributeValueBoolean = new AttributeValueBoolean();
            attributeValueBoolean.ObjectValue = true;

            Assert.That(attributeValueBoolean.TheValue, Is.True);
        }

        [Test]
        public void Verify_that_when_ObjectValue_is_not_boolean_an_exception_is_thrown()
        {
            var attributeValueBoolean = new AttributeValueBoolean();

            Assert.That(
                () => attributeValueBoolean.ObjectValue = "true",
                Throws.Exception.TypeOf<InvalidOperationException>()
                    .With.Message.Contains("Cannot use true as value for this AttributeValueBoolean."));
        }
    }
}
