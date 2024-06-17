using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSC.CSharp.Library.ChatGPT.Enums
{
    /// <summary>
    /// ModelValue string enum. For more information about the model, see the official documentation on 
    /// <a href="https://platform.openai.com/docs/models">https://platform.openai.com/docs/models</a>
    /// </summary>
    public class ModelValue
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ModelValue"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        private ModelValue(string value) { Value = value; }

        #region Models

        /// <summary>
        /// The gpt-3.5-turbo model
        /// </summary>
        public static ModelValue GPT35Turbo = new ModelValue("gpt-3.5-turbo");

        /// <summary>
        /// The gpt-3.5-turbo-16k model
        /// </summary>
        public static ModelValue GPT35Turbo16k = new ModelValue("gpt-3.5-turbo-16k");

        /// <summary>
        /// The gpt-4-turbo
        /// </summary>
        public static ModelValue GPT4Turbo = new ModelValue("gpt-4-turbo");

        #endregion

        /// <summary>
        /// Gets the value.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string? Value { get; private set; }
    }
}
