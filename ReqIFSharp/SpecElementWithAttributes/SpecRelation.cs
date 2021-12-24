﻿// -------------------------------------------------------------------------------------------------
// <copyright file="SpecRelation.cs" company="RHEA System S.A.">
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

namespace ReqIFSharp
{
    using System;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Xml;

    /// <summary>
    /// Defines relations (links) between two <see cref="SpecObject"/> instances.
    /// </summary>
    public class SpecRelation : SpecElementWithAttributes
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpecRelation"/> class.
        /// </summary>
        public SpecRelation()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpecRelation"/> class.
        /// </summary>
        /// <param name="reqIfContent">
        /// The container <see cref="reqIfContent"/>
        /// </param>
        internal SpecRelation(ReqIFContent reqIfContent)
            : base(reqIfContent)
        {
            this.ReqIFContent.SpecRelations.Add(this);
        }

        /// <summary>
        /// Gets or sets the Source object of the relationship.
        /// </summary>
        public SpecObject Source { get; set; }

        /// <summary>
        /// Gets or sets the Target object of the relationship.
        /// </summary>
        public SpecObject Target { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="SpecRelationType"/> of the relationship
        /// </summary>
        public SpecRelationType Type { get; set; }

        /// <summary>
        /// Reads the concrete <see cref="SpecElementWithAttributes"/> elements
        /// </summary>
        /// <param name="reader">The current <see cref="XmlReader"/></param>
        /// <remarks>
        /// In this case, read the source and target
        /// </remarks>
        protected override void ReadObjectSpecificElements(XmlReader reader)
        {
            if (reader.MoveToContent() == XmlNodeType.Element)
            {
                switch (reader.LocalName)
                {
                    case "SOURCE":
                        if (reader.ReadToDescendant("SPEC-OBJECT-REF"))
                        {
                            var reference = reader.ReadElementContentAsString();
                            var specObject = this.ReqIFContent.SpecObjects.SingleOrDefault(x => x.Identifier == reference);
                            this.Source = specObject
                                          ?? new SpecObject
                                          {
                                              Identifier = reference,
                                              Description = "This spec-object was not found in the source file."
                                          };
                        }
                        break;
                    case "TARGET":
                        if (reader.ReadToDescendant("SPEC-OBJECT-REF"))
                        {
                            var reference = reader.ReadElementContentAsString();
                            var specObject = this.ReqIFContent.SpecObjects.SingleOrDefault(x => x.Identifier == reference);
                            this.Target = specObject
                                          ?? new SpecObject
                                          {
                                              Identifier = reference,
                                              Description = "This spec-object was not found in the source file."
                                          };
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Asynchronously reads the concrete <see cref="SpecElementWithAttributes"/> elements
        /// </summary>
        /// <param name="reader">The current <see cref="XmlReader"/></param>
        /// <remarks>
        /// In this case, read the source and target
        /// </remarks>
        /// <param name="token">
        /// A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        protected override async Task ReadObjectSpecificElementsAsync(XmlReader reader, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }

            if (await reader.MoveToContentAsync() == XmlNodeType.Element)
            {
                switch (reader.LocalName)
                {
                    case "SOURCE":
                        if (reader.ReadToDescendant("SPEC-OBJECT-REF"))
                        {
                            var reference = await reader.ReadElementContentAsStringAsync();
                            var specObject = this.ReqIFContent.SpecObjects.SingleOrDefault(x => x.Identifier == reference);
                            this.Source = specObject
                                          ?? new SpecObject
                                          {
                                              Identifier = reference,
                                              Description = "This spec-object was not found in the source file."
                                          };
                        }
                        break;
                    case "TARGET":
                        if (reader.ReadToDescendant("SPEC-OBJECT-REF"))
                        {
                            var reference = await reader.ReadElementContentAsStringAsync();
                            var specObject = this.ReqIFContent.SpecObjects.SingleOrDefault(x => x.Identifier == reference);
                            this.Target = specObject
                                          ?? new SpecObject
                                          {
                                              Identifier = reference,
                                              Description = "This spec-object was not found in the source file."
                                          };
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Gets the <see cref="SpecType"/>class
        /// </summary>
        /// <returns>
        /// an instance of <see cref="SpecType"/>
        /// </returns>
        protected override SpecType GetSpecType()
        {
            return this.Type;
        }

        /// <summary>
        /// Sets the <see cref="SpecType"/> 
        /// </summary>
        /// <param name="specType">
        /// The <see cref="SpecType"/> to set.
        /// </param>
        protected override void SetSpecType(SpecType specType)
        {
            if (specType.GetType() != typeof(SpecRelationType))
            {
                throw new ArgumentException("specType must of type SpecRelationType");
            }

            this.Type = (SpecRelationType)specType;
        }

        /// <summary>
        /// Reads the <see cref="SpecType"/> which is specific to the <see cref="SpecRelation"/> class.
        /// </summary>
        /// <param name="reader">
        /// an instance of <see cref="XmlReader"/>
        /// </param>
        protected override void ReadSpecType(XmlReader reader)
        {
            if (reader.ReadToDescendant("SPEC-RELATION-TYPE-REF"))
            {
                var reference = reader.ReadElementContentAsString();
                var specType = this.ReqIFContent.SpecTypes.SingleOrDefault(x => x.Identifier == reference);
                this.Type = (SpecRelationType)specType;
            }
        }

        /// <summary>
        /// Asynchronously reads the <see cref="SpecType"/> which is specific to the <see cref="SpecRelation"/> class.
        /// </summary>
        /// <param name="reader">
        /// an instance of <see cref="XmlReader"/>
        /// </param>
        /// <param name="token">
        /// A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        protected override async Task ReadSpecTypeAsync(XmlReader reader, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                token.ThrowIfCancellationRequested();
            }

            if (reader.ReadToDescendant("SPEC-RELATION-TYPE-REF"))
            {
                var reference = await reader.ReadElementContentAsStringAsync();
                var specType = this.ReqIFContent.SpecTypes.SingleOrDefault(x => x.Identifier == reference);
                this.Type = (SpecRelationType)specType;
            }
        }

        /// <summary>
        /// Reads the <see cref="SpecHierarchy"/> which is specific to the <see cref="Specification"/> class.
        /// </summary>
        /// <param name="reader">
        /// an instance of <see cref="XmlReader"/>
        /// </param>
        protected override void ReadHierarchy(XmlReader reader)
        {
            throw new InvalidOperationException("SpecRelation does not have a hierarchy");
        }

        /// <summary>
        /// Asynchronously reads the <see cref="SpecHierarchy"/> which is specific to the <see cref="Specification"/> class.
        /// </summary>
        /// <param name="reader">
        /// an instance of <see cref="XmlReader"/>
        /// </param>
        /// <param name="token">
        /// A cancellation token that can be used by other objects or threads to receive notice of cancellation.
        /// </param>
        protected override Task ReadHierarchyAsync(XmlReader reader, CancellationToken token)
        {
            throw new InvalidOperationException("SpecRelation does not have a hierarchy");
        }

        /// <summary>
        /// Converts a <see cref="SpecRelation"/> object into its XML representation.
        /// </summary>
        /// <param name="writer">
        /// an instance of <see cref="XmlWriter"/>
        /// </param>
        /// <exception cref="SerializationException">
        /// The Type, Source and Target properties may not be null
        /// </exception>
        public override void WriteXml(XmlWriter writer)
        {
            if (this.Type == null)
            {
                throw new SerializationException($"The Type of SpecRelation {this.Identifier}:{this.LongName} may not be null");
            }

            if (this.Source == null)
            {
                throw new SerializationException($"The Source of SpecRelation {this.Identifier}:{this.LongName} may not be null");
            }

            if (this.Target == null)
            {
                throw new SerializationException($"The Target of SpecRelation {this.Identifier}:{this.LongName} may not be null");
            }

            base.WriteXml(writer);

            writer.WriteStartElement("TYPE");
            writer.WriteElementString("SPEC-RELATION-TYPE-REF", this.Type.Identifier);
            writer.WriteEndElement();

            writer.WriteStartElement("TARGET");
            writer.WriteElementString("SPEC-OBJECT-REF", this.Target.Identifier);
            writer.WriteEndElement();

            writer.WriteStartElement("SOURCE");
            writer.WriteElementString("SPEC-OBJECT-REF", this.Source.Identifier);
            writer.WriteEndElement();
        }

        /// <summary>
        /// Asynchronously converts a <see cref="SpecRelation"/> object into its XML representation.
        /// </summary>
        /// <param name="writer">
        /// an instance of <see cref="XmlWriter"/>
        /// </param>
        /// <exception cref="SerializationException">
        /// The Type, Source and Target properties may not be null
        /// </exception>
        public override async Task WriteXmlAsync(XmlWriter writer)
        {
            if (this.Type == null)
            {
                throw new SerializationException($"The Type of SpecRelation {this.Identifier}:{this.LongName} may not be null");
            }

            if (this.Source == null)
            {
                throw new SerializationException($"The Source of SpecRelation {this.Identifier}:{this.LongName} may not be null");
            }

            if (this.Target == null)
            {
                throw new SerializationException($"The Target of SpecRelation {this.Identifier}:{this.LongName} may not be null");
            }

            await base.WriteXmlAsync(writer);

            await writer.WriteStartElementAsync(null, "TYPE", null);
            await writer.WriteElementStringAsync(null, "SPEC-RELATION-TYPE-REF",null,  this.Type.Identifier);
            await writer.WriteEndElementAsync();

            await writer.WriteStartElementAsync(null, "TARGET", null);
            await writer.WriteElementStringAsync(null, "SPEC-OBJECT-REF", null, this.Target.Identifier);
            await writer.WriteEndElementAsync();

            await writer.WriteStartElementAsync(null,"SOURCE", null);
            await writer.WriteElementStringAsync(null,"SPEC-OBJECT-REF", null,this.Source.Identifier);
            await writer.WriteEndElementAsync();
        }
    }
}
