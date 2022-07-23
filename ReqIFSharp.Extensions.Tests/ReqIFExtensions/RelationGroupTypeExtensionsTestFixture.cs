﻿//  -------------------------------------------------------------------------------------------------
//  <copyright file="RelationGroupTypeExtensionsTestFixture.cs" company="RHEA System S.A.">
// 
//    Copyright 2017-2022 RHEA System S.A.
// 
//    Licensed under the Apache License, Version 2.0 (the "License");
//    you may not use this file except in compliance with the License.
//    You may obtain a copy of the License at
// 
//        http://www.apache.org/licenses/LICENSE-2.0
// 
//    Unless required by applicable law or agreed to in writing, software
//    distributed under the License is distributed on an "AS IS" BASIS,
//    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//    See the License for the specific language governing permissions and
//    limitations under the License.
// 
//  </copyright>
//  -------------------------------------------------------------------------------------------------

namespace ReqIFSharp.Extensions.Tests.ReqIFExtensions
{
    using System.Linq;

    using NUnit.Framework;

    using ReqIFSharp.Extensions.ReqIFExtensions;
    using ReqIFSharp.Extensions.Tests.TestData;

    /// <summary>
    /// Suite of tests for the <see cref="RelationGroupTypeExtensions"/> class.
    /// </summary>
    [TestFixture]
    public class RelationGroupTypeExtensionsTestFixture
    {
        private ReqIF reqIf;

        [SetUp]
        public void SetUp()
        {
            var reqIfTestDataCreator = new ReqIFTestDataCreator();

            this.reqIf = reqIfTestDataCreator.Create();
        }

        [Test]
        public void Verify_that_QueryReferencingRelationGroups_returns_expected_results()
        {
            var relationGroupType = (RelationGroupType)this.reqIf.CoreContent.SpecTypes.Single(x => x.Identifier == "relationgrouptype");

            var specObjects = relationGroupType.QueryReferencingRelationGroups();

            Assert.That(specObjects.Count(), Is.EqualTo(1));
        }
    }
}
