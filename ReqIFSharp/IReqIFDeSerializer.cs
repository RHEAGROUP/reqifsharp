﻿// -------------------------------------------------------------------------------------------------
// <copyright file="IReqIFDeSerializer.cs" company="RHEA System S.A.">
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
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using System.Xml.Schema;
    
    /// <summary>
    /// Specifies the <see cref="IReqIFDeSerializer"/> 
    /// </summary>
    public interface IReqIFDeSerializer
    {
        /// <summary>
        /// Deserializes a <see cref="ReqIF"/> XML document.
        /// </summary>
        /// <param name="xmlFilePath">
        /// The Path of the <see cref="ReqIF"/> file to deserialize
        /// </param>
        /// <param name="validate">
        /// a value indicating whether the XML document needs to be validated or not
        /// </param>
        /// <param name="validationEventHandler">
        /// The <see cref="ValidationEventHandler"/> that processes the result of the <see cref="ReqIF"/> validation.
        /// </param>
        /// <returns>
        /// A fully dereferenced <see cref="ReqIF"/> object graph
        /// </returns>
        IEnumerable<ReqIF> Deserialize(string xmlFilePath, bool validate = false, ValidationEventHandler validationEventHandler = null);

        /// <summary>
        /// Deserializes a <see cref="ReqIF"/> XML <see cref="Stream"/>.
        /// </summary>
        /// <param name="stream">
        /// The <see cref="Stream"/> that contains the reqifz file to deserialize
        /// </param>
        /// <param name="validate">
        /// a value indicating whether the XML document needs to be validated or not
        /// </param>
        /// <param name="validationEventHandler">
        /// The <see cref="ValidationEventHandler"/> that processes the result of the <see cref="ReqIF"/> validation.
        /// </param>
        /// <returns>
        /// Fully de-referenced <see cref="IEnumerable{ReqIF}"/> object graphs
        /// </returns>
        IEnumerable<ReqIF> Deserialize(Stream stream, bool validate = false, ValidationEventHandler validationEventHandler = null);
    }
}
